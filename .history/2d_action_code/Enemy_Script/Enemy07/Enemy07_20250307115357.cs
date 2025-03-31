using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy07 : MonoBehaviour,IMove
{
    [SerializeField,Header("左コライダー")] Collider2D leftWallCollider;  // 左の壁判定コライダー
    [SerializeField,Header("右コライダー")] Collider2D rightWallCollider; // 右の壁判定コライダー
    [SerializeField,Header("上コライダー")] Collider2D upWallCollider;  // 上の壁判定コライダー
    [SerializeField,Header("下コライダー")] Collider2D downWallCollider; // 下の壁判定コライダー
    private int _xDir;
    private int _yDir;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    public void Move(float _moveSpeed){
        _rb.velocity = new Vector2(_xDir* _moveSpeed,_yDir*_rb.velocity.y);
        _sr.flipX=_xDir==1?true:false;
    }
    private void Awake() {
        _xDir=-1;
        _yDir=-1;
        _sr=GetComponent<SpriteRenderer>();
        _rb=GetComponent<Rigidbody2D>();
    }
    private void Update() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (leftWallCollider.IsTouching(other))
            {
                _xDir = 1; // 左の壁に当たったら右へ
            }
            else if (rightWallCollider.IsTouching(other))
            {
                _xDir = -1; // 右の壁に当たったら左へ
            }
            else if (upWallCollider.IsTouching(other))
            {
                _yDir = -1; // 上の壁に当たったら下へ
            }
            else if (downWallCollider.IsTouching(other))
            {
                _yDir = 1; // 下の壁に当たったら上へ
            }
        }
    }
}
