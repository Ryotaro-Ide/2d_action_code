using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PGuard : MonoBehaviour
{    
    
    
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
            if(_player.IsDelayGuard) return;
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
            _player.IsDamageEase=true;
            
    }
    private void GuardEnd(){
        if(_player.IsDelayGuard){ //遅延中なら
                
                StartCoroutine(DelayGuardEnd());
                return;
            }
            _sr.color=new Color(1f,1f,1f);
            _player.IsDamageEase=false;
    }
    private IEnumerator DelayGuardStart(){
        _player.IsDelayGuard=true;
        
        _sr.color=new Color(0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.2f);
        _player.IsDamageEase=true;
        
        _sr.color=new Color(0f,0f,1f);
    }
    private IEnumerator DelayGuardEnd(){
        _player.IsDamageEase=false;
        _sr.color=new Color(0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.2f);
        _player.IsDelayGuard=false;
        _sr.color=new Color(1f,1f,1f);
    }
    
    private void Update() {
        
    }
    
    
    private bool CanNotGuard(){
        return _player.IsJump||_aB.IsAttack||_player.IsBarrier;
    }
    
}
