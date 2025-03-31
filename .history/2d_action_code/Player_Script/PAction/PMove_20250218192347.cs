using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PMove : MonoBehaviour
{
    public float _climbSpeed;
    public LayerMask _ladderLayer;
    private GameObject _ladderObj;
    private Vector2 _inputDirection; 
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private AttackBase _aB;
    private PGuard _guard;
    private PlayerBase _pb;
    private bool _isWalk=false;
    private void Start() {

        _sr=GetComponent<SpriteRenderer>();
        _rb=GetComponent<Rigidbody2D>();
        _aB=GetComponent<AttackBase>();
        _guard=GetComponent<PGuard>();
        _pb=GetComponent<PlayerBase>();
    }
    public void Move(Func<float> SpeedControl)
    {
        
        if(CanNotMove()) return;
        
            _rb.velocity = new Vector2(_inputDirection.x * SpeedControl(), _rb.velocity.y);
            LookMoveDirec();
            if (Mathf.Abs(_inputDirection.x) > 0.1f && _pb.IsLadderMove){
                _pb.IsLadderMove=false;
            }
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
    public void LadderMove(){
        if(_inputDirection.y!=0f){
            _pb.IsLadderMove=true;
            _pb.IsJump=false;
            this.transform.position=new Vector2(_ladderObj.transform.position.x,this.transform.position.y);
            _rb.velocity=new Vector2(_rb.velocity.x,_inputDirection.y*_climbSpeed);
        }else if(_inputDirection.y==0&&_pb.IsLadderMove){
            _rb.velocity=Vector2.zero;
        }
        
        
       
    }
    private bool CheckLadder()
    {
        // Raycastをプレイヤー中心から真下へ撃つ
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, _ladderLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.up * 0.5f, Color.green);
            _ladderObj=hit.collider.gameObject;
            return true;
        }

        Debug.DrawRay(transform.position, Vector2.up * 0.5f, Color.red);
        
        return false;
    }
    private bool CanNotMove(){
        
        return _pb._isKnockedBack||(_aB.IsAttack&&!_pb.IsJump);
    }
    public void _OnLookUp(InputAction.CallbackContext context){

        if(context.performed){
            _pb.IsLookUp=true;
            
        }else if(context.canceled){
            _pb.IsLookUp=false;
        }    
    }
    
    public void _OnSquat(InputAction.CallbackContext context)
    {
        if (context.performed&&!_pb.IsJump) // しゃがみキーが押されたとき
        {
            _pb.IsSquat=true;
            gameObject.GetComponent<Animator>().SetBool("isSquat",true);
        }
        else if (context.canceled) // しゃがみキーが離されたとき
        {
            _pb.IsSquat=false;
            gameObject.GetComponent<Animator>().SetBool("isSquat",false);

        }
    }
    public bool IsWalk{
        get=>_isWalk;}
    // Update is called once per frame
    void Update()
    {
        if(CheckLadder()){//ハシゴに触れてるかチェック
            LadderMove();
        }else{
            _pb.IsLadderMove=false;
        }
    }
}
