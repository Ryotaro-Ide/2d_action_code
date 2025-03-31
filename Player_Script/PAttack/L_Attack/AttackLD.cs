using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLD : AttackBase,IAttack
{
   
    public float _attackDashSpeed;
    public float _invicibleTime;
    public float _stopTime;
    private Collider2D _collider;
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    [SerializeField,Header("強化攻撃力")]
    private int _energyAttackPoint;
    [SerializeField,Header("消費エナジー")]
    private int _energyConsume;
    protected override void Awake(){
        base.Awake();
        _collider=GetComponent<Collider2D>();
        _rb=transform.parent.gameObject.GetComponent<Rigidbody2D>();
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
    }

   
    public void Attack(){
        
        StartCoroutine(IAttackLD());
    }
    IEnumerator IAttackLD(){ 
        _collider.enabled=true;
        float direction = transform.localScale.x > 0 ? 1 : -1;
        yield return new WaitForSeconds(_stopTime);
        StartCoroutine(_player.InvicibleTime());
        _rb.AddForce(Vector2.right * _attackDashSpeed*direction, ForceMode2D.Impulse);
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="Enemy"){
            int ap=_attackPoint;
            if(IsEnergyUse(_energyConsume)){
                ap=_energyAttackPoint;
                AttackEnergyConsume(_energyConsume);
            } 
            Debug.Log("damage="+ap);
            EnemyBase enemy=collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(ap,_player.gameObject);
        }
    }
}
