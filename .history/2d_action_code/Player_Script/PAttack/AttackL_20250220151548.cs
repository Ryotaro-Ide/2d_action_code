using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackL : AttackBase,IAttack
{
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    [SerializeField,Header("強化攻撃力")]
    private int _energyAttackPoint;

    [SerializeField,Header("消費エナジー")]
    private int _energyConsume;
    private Collider2D _collider;
    
    protected override void Awake(){
        base.Awake();
        _collider=GetComponent<Collider2D>();
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
    }

   
    public void Attack(){
        
        StartCoroutine(IAttackL());
    }
    IEnumerator IAttackL(){
        _collider.enabled=true;
        yield return null;
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
