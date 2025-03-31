using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackR : AttackBase,IAttack
{
    public GameObject _rainWavePrefab;
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    [SerializeField,Header("消費エナジー")]
    private int _energyConsume;
    [SerializeField,Header("waveの速度")]
    private float _waveVelocity;
    [SerializeField,Header("wave持続時間")]
    private float _waveTime;
    protected override void Awake(){
        base.Awake();
        
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
        _sr=transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

   
    public void Attack(){
        AttackEnergyConsume(_energyConsume);
        StartCoroutine(IAttackR());
    }
    IEnumerator IAttackR(){
       
        GameObject _rainWave=Instantiate(_rainWavePrefab,transform.position,Quaternion.identity);
        int direction= _sr.flipX ? -1 : 1;
        _rainWave.GetComponent<Rigidbody2D>().velocity=new Vector2(direction*_waveVelocity,0f);
        yield return new WaitForSeconds(_waveTime);
        Destroy(_rainWave);
    }
    
     
}
