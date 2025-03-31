using UnityEngine;

public class SignalRoad : MonoBehaviour
{
    public enum RoadType { White, Black }
    public RoadType roadType;
    public Signal.SignalState currentSignalState;
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform spawnPoint; // 敵の出現位置（Inspectorで設定）
    [SerializeField] private Vector2 rayDirection = Vector2.right; // レイの方向
    [SerializeField] private float rayLength = 2f; // レイの長さ
    [SerializeField] private LayerMask playerLayer; // プレイヤーのレイヤー
    private void Awake()
    {
        
    }
    private void Update() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayLength, playerLayer);
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

    
    
    // Scene ビューでレイを可視化
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rayDirection * rayLength);
    }
}
