using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRD : AttackBase,IAttack
{
    
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    [SerializeField,Header("消費エナジー")]
    private int _energyConsume;
    [SerializeField,Header("速度")]
    private float _v;
    [SerializeField,Header("硬直時間")]
    private float _time;
    [SerializeField,Header("無敵時間")]
    private float _invicibleTime;
    private Collider2D _collider;

        
    

    protected override void Awake(){
        base.Awake();
        
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
        _sr=transform.parent.gameObject.GetComponent<SpriteRenderer>();
        _rb=transform.parent.gameObject.GetComponent<Rigidbody2D>();
        _collider=GetComponent<Collider2D>();

    }

   
    public void Attack(){
        
        AttackEnergyConsume(_energyConsume);
        StartCoroutine(IAttackRD());
    }
    IEnumerator IAttackRD(){
        _collider.enabled=true;
        _rb.velocity=Vector2.zero;
        float direction = transform.localScale.x > 0 ? 1 : -1;
        yield return new WaitForSeconds(_time);
        StartCoroutine(_player.InvicibleTime());
        _rb.velocity=new Vector2(_v*direction,0f);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="Enemy"){
            int ap=_attackPoint;
            EnemyBase enemy=collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(ap,_player.gameObject);
        }
    }
     
}
