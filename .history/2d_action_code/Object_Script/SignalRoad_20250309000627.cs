using UnityEngine;

public class SignalRoad : MonoBehaviour
{
    public enum RoadType { White, Black }
    public RoadType roadType;
    public Signal.SignalState currentSignalState;
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform spawnPoint; // 敵の出現位置（Inspectorで設定）

    private void Awake()
    {
        currentSignalState = Signal.SignalState.Blue; // 初期状態は赤（変更可）
    }

    public void UpdateRoadState(Signal.SignalState newState)
    {
        Debug.Log(newState);
        currentSignalState = newState;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (roadType == RoadType.Black && currentSignalState == Signal.SignalState.Blue)
            {
                Debug.Log("プレイヤーにダメージ！");
                collision.gameObject.GetComponent<PlayerBase>().DamageByOther(1,gameObject);
            }
            else if (currentSignalState == Signal.SignalState.Red)
            {
                Debug.Log(" 敵出現！");
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
