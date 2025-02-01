using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//歩く敵、追跡なし
public class Enemy01 : MonoBehaviour,IMove
{
    private Rigidbody2D _rb;
    public void Move(float _moveSpeed){
        _rb.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rb.velocity.y);
    }
    private void Awake() {
        _rb=GetComponent<Rigidbody2D>();
    }
    private void Update() {
        
    }

}
