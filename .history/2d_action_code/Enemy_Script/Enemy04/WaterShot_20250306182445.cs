using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShot : MonoBehaviour
{
    private HP_Player _hpPlayer;
    public int _attackPoint;
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject==transform.parent.gameObject){
            return;
        }else if(collider.gameObject.tag=="Player"){
            _hpPlayer.DamageHP(_attackPoint, transform.parent.gameObject,collider.gameObject,false);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _hpPlayer=FindObjectOfType<HP_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
