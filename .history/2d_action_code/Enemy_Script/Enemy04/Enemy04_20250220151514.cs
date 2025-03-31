using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04 : MonoBehaviour,IMove
{
    public float _moveArea;
    public float _waterShotVx;
    public float _waterShotVy;
    public float _waterShotInterval;
    public GameObject _waterShotPrefab;
    private float _time;
    private int _waterShotDirection;
    private bool _isFirstMove=true;
    private GameObject _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _tr;
    
    public void Move(float _moveSpeed){
        if(_isFirstMove){
            _rb.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rb.velocity.y);
            _isFirstMove=false;
            return;
        }
        if(transform.position.x<=_tr.x-_moveArea){
            _rb.velocity = new Vector2(Vector2.right.x * _moveSpeed, _rb.velocity.y);

        }else if(transform.position.x>=_tr.x+_moveArea){
            _rb.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rb.velocity.y); 
        }

        Vector2 trplayer=_player.transform.position;
        Vector2 trDistance=(trplayer-new Vector2(transform.position.x,transform.position.y)).normalized;
        if(trDistance.x>0){//プレイヤーが右側
            _sr.flipX=true;
            _waterShotDirection=1;
        }else if(trDistance.x<-0){
            _sr.flipX=false;
            _waterShotDirection=-1;
        }
    }
    private void Attack() {
        
         GameObject _waterShot=Instantiate(_waterShotPrefab,transform.position, Quaternion.identity,transform);
         _waterShot.GetComponent<Rigidbody2D>().AddForce(new Vector2(_waterShotDirection*_waterShotVx,_waterShotVy),ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
    void Awake()
    {
        _player=GameObject.FindGameObjectWithTag("Player");
        _rb=GetComponent<Rigidbody2D>();
        _sr=GetComponent<SpriteRenderer>();
        _tr=transform.position;
        _time=0f;
        _waterShotDirection=-1;
    }

    // Update is called once per frame
    void Update()
    {
        _time+=Time.deltaTime;
        if(_time>=_waterShotInterval){
            _time=0f;
            Attack();
        }
    }
}
