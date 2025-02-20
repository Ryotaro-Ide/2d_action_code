using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//歩く敵、追跡なし
public class Enemy01 : MonoBehaviour,IMove
{
    public Collider2D leftWallCollider;  // 左の壁判定コライダー
    public Collider2D rightWallCollider; // 右の壁判定コライダー
    private int _xDir;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    public void Move(float _moveSpeed){
        _rb.velocity = new Vector2(_xDir* _moveSpeed, _rb.velocity.y);
        _sr.flipX=_xDir==1?true:false;
    }
    private void Awake() {
        _xDir=-1;
        _sr=GetComponent<SpriteRenderer>();
        _rb=GetComponent<Rigidbody2D>();
    }
    private void Update() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Floor"){
            if (leftWallCollider.IsTouching(other)){
                _xDir=1;
            }else if(rightWallCollider.IsTouching(other)){
                _xDir=-1;
            }
    }
    }
}
