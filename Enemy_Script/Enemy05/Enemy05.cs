using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy05 : MonoBehaviour,IMove
{
    public float _radiusChase;
    private int _layerP;
    private int _maxHP;
    private bool _isMove;
    private bool _isfall;
    private GameObject _player;
    private Rigidbody2D _rb;
    private Vector2 tr;
    private Vector2 _distanceCopy;
    private Animator _anim;
    private SpriteRenderer _sr;
    private EnemyBase _enemyBase;
    public void Move(float _moveSpeed){
        
        
        if(_isMove){ //向き確定後
            if(_maxHP!=_enemyBase._hp){//飛行後、攻撃を受けたら落ちる
                _rb.velocity=new Vector2(0f,-6f);
                _isfall=true;
                _anim.enabled=false;
                return;
            }
            _rb.velocity = new Vector2(_distanceCopy.x * _moveSpeed,_moveSpeed);
            return;
        }
        if(!IsPlayerWithinArea()) return; //エリア内かどうかチェック
        Vector2 trplayer=_player.transform.position;
        Vector2 distanceEnemyToPlayer=(trplayer-tr).normalized;
        _isMove=true;
        if(distanceEnemyToPlayer.x>0.02){
            _sr.flipX=true;
            distanceEnemyToPlayer.x=1f;
            _distanceCopy=distanceEnemyToPlayer;

        }else if(distanceEnemyToPlayer.x<-0.02){
            _sr.flipX=false;
            distanceEnemyToPlayer.x=-1f;
            _distanceCopy=distanceEnemyToPlayer;
        }
        
        _rb.velocity = new Vector2(distanceEnemyToPlayer.x * _moveSpeed,_moveSpeed);

    }
    // Start is called before the first frame update
    void Awake()
    {
        _isMove=false;
        _isfall=false;
        _distanceCopy=Vector2.zero;
        _player=GameObject.FindGameObjectWithTag("Player");
        _rb=GetComponent<Rigidbody2D>();
        _anim=GetComponent<Animator>();
        _sr=GetComponent<SpriteRenderer>();
        _enemyBase=GetComponent<EnemyBase>();
        _layerP=LayerMask.GetMask("Player");
        _maxHP=_enemyBase._hp;
    }

    // Update is called once per frame
    void Update()
    {
        tr=transform.position;
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(!_isfall) return;
        if(collision.gameObject.tag=="Floor"){
            Destroy(gameObject);
        }
    }
    private bool IsPlayerWithinArea(){

        Collider2D chasePlayerArea = Physics2D.OverlapCircle(transform.position, _radiusChase, _layerP);
        
        return chasePlayerArea != null;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,_radiusChase);
    }
}
