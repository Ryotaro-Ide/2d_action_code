using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PBarrier : MonoBehaviour
{
    private float _currentTime;
    public float _span;
    public GameObject _barrier;
    private PlayerBase _player;
    private Energy_Player _eP;
    private AttackBase _aB;
    // Start is called before the first frame update
    void  Awake() {
        _currentTime=0f;
        _player=GetComponent<PlayerBase>();
        _eP=FindObjectOfType<Energy_Player>();
        _aB=GetComponent<AttackBase>();
        _barrier.SetActive(false);
    }
    private void Update() {
        if(_player.IsBarrierExpand){
            _currentTime+=Time.deltaTime;
            Debug.Log(_currentTime);
            if(_currentTime>=_span&&_player._energy>0){
                
                _eP.ConsumeEnergy(1);
                _currentTime=0f;
            }
        }
        if(_player._energy<=0){
            BarrierEnd();
        }
    }
    
    public void OnBarrier(InputAction.CallbackContext context){
        if(CanNotBarrier()) return;
        if(context.performed){
            BarrierStart();
        }else if(context.canceled){
            BarrierEnd();
        }
    }
    void BarrierStart(){
        _player.IsBarrier=true;
        _player.IsBarrierExpand=true;
        _barrier.SetActive(true);
      
    }
    void BarrierEnd(){
        _player.IsBarrier=false;
        _player.IsBarrierExpand=false;
        _barrier.SetActive(false);
    }
    private bool CanNotBarrier(){
        return _player.IsJump||_aB.IsAttack;
    }
}
