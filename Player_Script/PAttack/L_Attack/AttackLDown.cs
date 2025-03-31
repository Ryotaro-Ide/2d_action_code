using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLDown : AttackBase,IAttack
{
    public float _rotationSpeed;
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
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
    }
   public void Attack(){
        
        StartCoroutine(IAttackLDown());
    }
    IEnumerator IAttackLDown(){
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
            enemy.TakeDamage(_attackPoint,_player.gameObject);
        }
    }
}

