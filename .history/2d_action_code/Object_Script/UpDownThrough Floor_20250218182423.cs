using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownThroughFloor : MonoBehaviour
{
    public LayerMask _playerLayer;
    public float sizex;
    public float sizey;
    public float y;
    private GameObject _player;
    private PlayerBase _pb;
    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _pb=FindObjectOfType<PlayerBase>();
        _collider=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ThroughFloor();
    }

    private bool CheckPlayer()
{
    // ボックスの幅（x軸方向）と高さ（y軸方向）
    Vector2 boxSize = new Vector2(sizex, sizey); // x軸方向を2fに広げる
    Vector2 boxOrigin = new Vector2(transform.position.x, transform.position.y + y);

    // BoxCastの実行
    RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.up, 0f, _playerLayer);

    // デバッグ表示
    DrawBoxCastDebug(boxOrigin, boxSize, hit ? Color.green : Color.red);

    if (hit.collider != null)
    {
        _player = hit.collider.gameObject;
        Debug.Log("check");
        return true;
    }

    return false;
}
    private void ThroughFloor(){
        if(CheckPlayer()&&_pb.IsSquat){
            Debug.Log("true");
            _collider.isTrigger=true;
        }else{
            _collider.isTrigger=false;
        }
    }
    private void DrawBoxCastDebug(Vector2 origin, Vector2 size, Color color)
{
    // 四隅の座標を計算
    Vector2 halfSize = size / 2f;
    Vector2 topLeft = origin + new Vector2(-halfSize.x, halfSize.y);
    Vector2 topRight = origin + new Vector2(halfSize.x, halfSize.y);
    Vector2 bottomLeft = origin + new Vector2(-halfSize.x, -halfSize.y);
    Vector2 bottomRight = origin + new Vector2(halfSize.x, -halfSize.y);

    // ボックスの四辺を描画
    Debug.DrawLine(topLeft, topRight, color);
    Debug.DrawLine(topRight, bottomRight, color);
    Debug.DrawLine(bottomRight, bottomLeft, color);
    Debug.DrawLine(bottomLeft, topLeft, color);
}
}
