using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PGuard : MonoBehaviour
{    
    
    private bool _isDelayGuard=false;
    private bool _isDamageEase=false;
    private SpriteRenderer _sr;
    private PlayerBase _player;
    private AttackBase _aB;
    private void Start() {
        _sr=GetComponent<SpriteRenderer>();
        _player=GetComponent<PlayerBase>();
        _aB=GetComponent<AttackBase>();
    }
    public void _OnGuard(InputAction.CallbackContext context){
        if(CanNotGuard()) return;
        
        if(context.performed){
            if(_isDelayGuard) return;
            Guard();
            _player.IsGuard=true;
            
        }else if(context.canceled){
            GuardEnd();
            _player.IsGuard=false;
        }
    }

    public void Guard(){ 
        
            if(!_player.IsUmbrellaOpened){
                StartCoroutine(DelayGuardStart());
                return;
                  //遅延処理
            }
            _sr.color=new Color(0f,0f,1f);
            _isDamageEase=true;
            
    }
    private void GuardEnd(){
        if(_isDelayGuard){ //遅延中なら
                
                StartCoroutine(DelayGuardEnd());
                return;
            }
            _sr.color=new Color(1f,1f,1f);
            _isDamageEase=false;
    }
    private IEnumerator DelayGuardStart(){
        _isDelayGuard=true;
        
        _sr.color=new Color(0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.2f);
        _isDamageEase=true;
        
        _sr.color=new Color(0f,0f,1f);
    }
    private IEnumerator DelayGuardEnd(){
        _isDamageEase=false;
        _sr.color=new Color(0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.2f);
        _isDelayGuard=false;
        _sr.color=new Color(1f,1f,1f);
    }
    
    private void Update() {
        
    }
    public bool IsDamageEase{
        get=>_isDamageEase;
    }
    
    private bool CanNotGuard(){
        return _player.IsJump||_aB.IsAttack||_player.IsBarrier;
    }
    
}
