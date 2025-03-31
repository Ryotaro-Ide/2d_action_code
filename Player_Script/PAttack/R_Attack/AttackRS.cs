using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRS : AttackBase,IAttack
{
    public GameObject _rainGunPrefab;
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    [SerializeField,Header("消費エナジー")]
    private int _energyConsume;
    [SerializeField,Header("gunの速度")]
    private float _gunVelocity;
    [SerializeField,Header("gun持続時間")]
    private float _gunTime;
    protected override void Awake(){
        base.Awake();
        
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
        _sr=transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

   
    public void Attack(){
        AttackEnergyConsume(_energyConsume);
        StartCoroutine(IAttackRS());
    }
    IEnumerator IAttackRS(){
       
        GameObject _rainGun=Instantiate(_rainGunPrefab,transform.position,Quaternion.identity);
        int direction= _sr.flipX ? -1 : 1;
        _rainGun.GetComponent<Rigidbody2D>().velocity=new Vector2(direction*_gunVelocity,0f);
        yield return new WaitForSeconds(_gunTime);
        Destroy(_rainGun);
    }
    
     
}
