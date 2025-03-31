using UnityEngine;

public class SignalRoad : MonoBehaviour
{
    public enum RoadType { White, Black }
    public RoadType roadType;
    public Signal.SignalState currentSignalState;
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform spawnPoint; // 敵の出現位置（Inspectorで設定）

    private void Start()
    {
        currentSignalState = Signal.SignalState.Blue; // 初期状態は赤（変更可）
    }

    public void UpdateRoadState(Signal.SignalState newState)
    {
        currentSignalState = newState;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (roadType == RoadType.Black && currentSignalState == Signal.SignalState.Blue)
            {
                Debug.Log("黒い道路（青信号）→ プレイヤーにダメージ！");
                other.GetComponent<PlayerBase>().Damage(1,gameObject);
            }
            else if (currentSignalState == Signal.SignalState.Red)
            {
                Debug.Log("白い道路（赤信号）→ 敵出現！");
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    
    }
}
