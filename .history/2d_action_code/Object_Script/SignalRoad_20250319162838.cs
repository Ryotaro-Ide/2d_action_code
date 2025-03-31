using UnityEngine;

public class SignalRoad : MonoBehaviour
{
    public enum RoadType { White, Black }
    public RoadType roadType;
    public Signal.SignalState currentSignalState;
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform spawnPoint; // 敵の出現位置（Inspectorで設定）
    [Header("BoxCast の設定")]
    [SerializeField] private Vector2 _boxOffset = Vector2.zero; // ボックスの発射位置
    [SerializeField] private Vector2 _boxSize = new Vector2(1f, 1f); // ボックスのサイズ
    [SerializeField] private LayerMask _playerLayer; // プレイヤーのレイヤー
    private void Awake()
    {
        
    }
    private void Update() {
        Vector2 boxStart = (Vector2)transform.position + _boxOffset; // ボックスの発射位置
        RaycastHit2D hit = Physics2D.BoxCast(boxStart, _boxSize, 0f, Vector2.zero, 0f, _playerLayer);
        if (hit.collider != null)
        {
            
            OnPlayerDetected(hit.collider.gameObject);
        }
    }
    private void OnPlayerDetected(GameObject player)
    {
        if (roadType == RoadType.Black && currentSignalState == Signal.SignalState.Blue)
            {
                Debug.Log("プレイヤーにダメージ！");
                player.GetComponent<PlayerBase>().DamageByOther(1,gameObject);
            }
            else if (currentSignalState == Signal.SignalState.Red)
            {
                Debug.Log(" 敵出現！");
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
    }

    
    public void UpdateRoadState(Signal.SignalState newState)
    {
        
        currentSignalState = newState;
    }

    
    private void OnDrawGizmos()
    {
        Vector2 boxStart = (Vector2)transform.position + _boxOffset;
        Vector2 boxEnd = boxStart + (Vector2.zero.normalized * 0f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxStart, _boxSize); // 開始位置のボックス
        Gizmos.DrawWireCube(boxEnd, _boxSize);   // 終了位置のボックス
        Gizmos.DrawLine(boxStart, boxEnd);      // 発射方向のライン
    }
}
