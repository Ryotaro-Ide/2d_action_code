using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//止まっている、範囲内なら追跡、横移動のみ
public class Enemy02 : MonoBehaviour,IMove
{
    public float _radiusChase;
    private int _layerP;
    private GameObject _player;
    private Rigidbody2D _rb;
    private Vector2 tr;
    private Animator _anim;
    private SpriteRenderer _sr;
    public void Move(float _moveSpeed){
        if(!IsPlayerWithinArea()) return; //エリア内かどうかチェック
        
        Vector2 trplayer=_player.transform.position;
        Vector2 trDistance=(trplayer-tr).normalized;
        if(trDistance.x>=-0.02&&trDistance.x<=0.02){
            _anim.SetBool("isMove",false);
            
            _rb.velocity=Vector2.zero; 
            return;
        }else if(trDistance.x>0.02){
            _sr.flipX=true;
            trDistance.x=1f;
        }else if(trDistance.x<-0.02){
            _sr.flipX=false;
            trDistance.x=-1f;
        }
        
        _anim.SetBool("isMove",true);
        _rb.velocity = new Vector2(trDistance.x * _moveSpeed, _rb.velocity.y);
    }
    private void Awake(){
        _player=GameObject.FindGameObjectWithTag("Player");
        
        _rb=GetComponent<Rigidbody2D>();
        _anim=GetComponent<Animator>();
        _sr=GetComponent<SpriteRenderer>();
        _layerP=LayerMask.GetMask("Player");

    }
    private void Update(){
        tr=transform.position;
    }
    private bool IsPlayerWithinArea(){

        Collider2D chasePlayerArea = Physics2D.OverlapCircle(transform.position, _radiusChase, _layerP);
        
        return chasePlayerArea != null;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,_radiusChase);
    }
}
