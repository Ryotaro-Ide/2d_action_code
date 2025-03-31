using UnityEngine;

public class SignalRoad : MonoBehaviour
{
    public Signal.SignalState currentSignalState;
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform spawnPoint; // 敵の出現位置（Inspectorで設定）

    private void Start()
    {
        currentSignalState = Signal.SignalState.Red; // 初期状態は赤（変更可）
    }

    public void UpdateRoadState(Signal.SignalState newState)
    {
        currentSignalState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentSignalState == Signal.SignalState.Blue)
            {
                Debug.Log("プレイヤーにダメージ！");
                other.GetComponent<PlayerBase>().Damage(1);
            }
            else if (currentSignalState == Signal.SignalState.Red)
            {
                Debug.Log("敵が出現！");
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
