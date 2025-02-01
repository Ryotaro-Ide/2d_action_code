using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//止まっている、範囲内なら追跡、横移動のみ
public class Enemy03 : MonoBehaviour,IMove
{
    public float _radiusChase;
    public float _diveTime;
    public float _chaseTime;
    public float _attackTime;
    public Sprite _enemySp;
    public Sprite _shadowSp;
    private int _layerP;
    private float stateTimer = 0f;
    private bool _isDive;
    private enum State { Idle, Diving, Chasing, Attacking, Cooldown }
    private State currentState = State.Idle;

    private GameObject _player;
    private Rigidbody2D _rb;
    private Collider2D _eCollider;
    private Collider2D[] _pColliders;
    private Vector2 tr;
    private Animator _anim;
    private SpriteRenderer _sr;
    public void Move(float _moveSpeed){
        stateTimer -= Time.deltaTime;
        switch (currentState)
        {
            case State.Idle:
                IsPlayerWithinArea();
                break;
            case State.Diving:
                GroundDive();
                break;
            case State.Chasing:
                ChasePlayer(_moveSpeed);
                break;
            case State.Attacking:
                AttackPlayer();
                break; 
            case State.Cooldown:
                CooldownEnemy();
                break;

        }
    }
    private void IsPlayerWithinArea(){

        Collider2D chasePlayerArea = Physics2D.OverlapCircle(transform.position, _radiusChase, _layerP);
        if(chasePlayerArea!=null){
            TransitionToState(State.Diving,_diveTime);
        }
    }
    private void GroundDive(){
        if (stateTimer <= 0f)
        {
            InvicibleMode(true);
            _sr.sprite=_shadowSp;
            TransitionToState(State.Chasing,_chaseTime);
        }

    }
    private void ChasePlayer(float _chaseSpeed)
    {
        if (_player != null)
        {
            Vector2 targetPosition = new Vector2(_player.transform.position.x, transform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _chaseSpeed * Time.deltaTime);
        }

        if (/*Mathf.Abs(_player.transform.position.x - transform.position.x) <= 0.1f || */stateTimer <= 0f)
        {
            TransitionToState(State.Attacking, _attackTime);
        }
    }
    private void AttackPlayer()
    {
        if(stateTimer<=0f){
        InvicibleMode(false);
        _sr.sprite=_enemySp;
        Debug.Log("Attack!");

        TransitionToState(State.Cooldown, 1f); // 攻撃後1秒隙
        }
    }
    private void CooldownEnemy()
    {
        if (stateTimer <= 0f)
        {
            TransitionToState(State.Idle, 0f);
        }
    }
    private void Awake(){
        _player=GameObject.FindGameObjectWithTag("Player");
        _isDive=false;
        _eCollider=GetComponent<Collider2D>();
        _pColliders = _player.GetComponentsInChildren<Collider2D>();
        _rb=GetComponent<Rigidbody2D>();
        _anim=GetComponent<Animator>();
        _sr=GetComponent<SpriteRenderer>();
        _layerP=LayerMask.GetMask("Player");

    }
    private void Update(){
        tr=transform.position;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,_radiusChase);
    }
    private void TransitionToState(State newState, float timer)
    {
        currentState = newState;
        stateTimer = timer;
        Debug.Log($"State changed to: {newState}");
    }
    private void InvicibleMode(bool isDive){
        foreach (var collider in _pColliders){
            Physics2D.IgnoreCollision(_eCollider, collider, isDive);
    }
    }
}
