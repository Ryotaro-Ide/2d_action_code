using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int _hp;
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    private Rigidbody2D _rigid; 
    private HP_Player _hpPlayer;
    private PlayerBase _player;
    private KnockBackC _kB;
    private IMove _iMove;
    public bool _isKnockedBack=false;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigid=GetComponent<Rigidbody2D>();
        _hpPlayer=FindObjectOfType<HP_Player>();
        _player=FindObjectOfType<PlayerBase>();
        _kB=FindObjectOfType<KnockBackC>();
        _iMove=GetComponent<IMove>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!_isKnockedBack&&_player!=null){
        _iMove.Move(_moveSpeed);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.tag=="Player"){
            _hpPlayer.DamageHP(_attackPoint, gameObject,collision.gameObject,true);
        
        
    }
    }
    
    public void TakeDamage(int attackPoint,GameObject player){ //攻撃を受けた
        _hp-=attackPoint;
        if(_hp>0){ //ノックバック、アニメーション停止、点滅など
            StartCoroutine(_kB.KnockBack(gameObject,player,_isKnockedBack,newbool=>_isKnockedBack=newbool));
            StartCoroutine(_kB.HitBlink(gameObject,3)); //対象、点滅回数
        }else{ //死亡アニメーション
            Destroy(gameObject);
        }
    }
    public void KnockBackGuardToEnemy(GameObject player){ //ガードを受けた
        StartCoroutine(_kB.KnockBackGuardToEnemy(gameObject,player,_isKnockedBack,newbool=>_isKnockedBack=newbool));
    }
}
public interface IMove{
    void Move(float _moveSpeed);
}
