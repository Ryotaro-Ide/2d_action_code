using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLAir : AttackBase,IAttack
{
    public int _attackPoint=5;
    public float _rotationSpeed;
    private Collider2D _collider;

    protected override void Awake(){
        base.Awake();
        _collider=GetComponent<Collider2D>();
        _player=transform.parent.gameObject.GetComponent<PlayerBase>();
    }
   public void Attack(){
        
        StartCoroutine(IAttackLAir());
    }
    IEnumerator IAttackLAir(){
        _collider.enabled=true;
        
        yield return null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="Enemy"){
            EnemyBase enemy=collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(_attackPoint,_player.gameObject);
        }
    }
}
