using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField,Header("振動する時間")]
    private float _shakeTime;
    [SerializeField,Header("振動の大きさ")]
    private float _shakeMagnitude;
    private Vector3 _trPos;
   
    private float _shakeCount;
    //private int _currentPlayerHP;
    // Start is called before the first frame update
    void Start()
    {
        _trPos=transform.position;
        
       // _currentPlayerHP=_player.GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    // _ShakeCheck();
    } 
   /* private void _ShakeCheck(){
        if(_currentPlayerHP!=_player.GetHP()){
            _currentPlayerHP=_player.GetHP();
            _shakeCount=0.0f;
            StartCoroutine(_Shake());
        }
    }
    IEnumerator _Shake(){
        Vector3 initPos=transform.position;
        while(_shakeCount<_shakeTime){
            float x=initPos.x+Random.Range(-_shakeMagnitude,_shakeMagnitude);
            float y=initPos.y+Random.Range(-_shakeMagnitude,_shakeMagnitude);
            transform.position=new Vector3(x,y,initPos.z);
            _shakeCount+=Time.deltaTime;
            yield return null;
        }
        transform.position=initPos;
    }*/
    
} 
