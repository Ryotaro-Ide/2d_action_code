using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    [SerializeField,Header("攻撃力")]
    private int ap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="Enemy"){
            EnemyBase enemy=collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(ap,gameObject);
        }
    }
}
