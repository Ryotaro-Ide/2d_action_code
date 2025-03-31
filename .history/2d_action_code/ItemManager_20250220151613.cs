using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField,Header("回復量")]
    private int _healAmount;
    PlayerBase _player;
    HP_Player _hpPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _player=FindObjectOfType<PlayerBase>();
        _hpPlayer=FindObjectOfType<HP_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider){
        
        if(collider.gameObject.tag=="Player"){
            int actualHealAmount=0;
            for(int i=1; i<=_healAmount; i++){
                if(_player._hp+i<=_player._maxHP){
                    actualHealAmount++;
                }
            }
            _hpPlayer.HealHP(actualHealAmount);
            Destroy(gameObject);
        }
    }
   
}
