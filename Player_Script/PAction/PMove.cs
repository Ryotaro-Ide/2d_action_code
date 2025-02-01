using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PMove : MonoBehaviour
{
    private Vector2 _inputDirection; 
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private AttackBase _aB;
    private PGuard _guard;
    private bool _isWalk=false;
    private void Start() {

        _sr=GetComponent<SpriteRenderer>();
        _rb=GetComponent<Rigidbody2D>();
        _aB=GetComponent<AttackBase>();
        _guard=GetComponent<PGuard>();
    }
    public void Move(Func<float> SpeedControl)
    {
        if(CanNotMove()) return;
            
            _rb.velocity = new Vector2(_inputDirection.x * SpeedControl(), _rb.velocity.y);
            LookMoveDirec();
        
    }

     public void LookMoveDirec()
    {
        
        if (_inputDirection.x > 0.0f && _sr.flipX == true)
        {
            _sr.flipX = false;
        }
        else if (_inputDirection.x < 0.0f && _sr.flipX == false)
        {
            _sr.flipX = true;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _isWalk=true;
        _inputDirection = context.ReadValue<Vector2>();
        _rb.bodyType=RigidbodyType2D.Dynamic;
        if(context.canceled){
            _isWalk=false;
        }
    }
    private bool CanNotMove(){
        PlayerBase _player=GetComponent<PlayerBase>();
        return _player._isKnockedBack||_aB.IsAttack;
    }
    public bool IsWalk{
        get=>_isWalk;}
    // Update is called once per frame
    void Update()
    {
        
    }
}
