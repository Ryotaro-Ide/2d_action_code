using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy08 : MonoBehaviour
{
    public float _radiusChase;
    private GameObject _player;
    private Rigidbody2D _rb;
    private Vector2 tr;
    private Animator _anim;
    private SpriteRenderer _sr;
    private EnemyBase _eB;
    private bool _isChasing = false; // **攻撃を受けたらtrueにする**

    public void Move(float _moveSpeed)
    {
        if (!_isChasing) return; // **攻撃を受けていなければ動かない**

        Vector2 trplayer = _player.transform.position;
        Vector2 trDistance = (trplayer - tr).normalized;

        if (trDistance.x >= -0.02f && trDistance.x <= 0.02f)
        {
            _anim.SetBool("isMove", false);
            _rb.velocity = Vector2.zero;
            return;
        }
        else if (trDistance.x > 0.02f)
        {
            _sr.flipX = true;
            trDistance.x = 1f;
        }
        else if (trDistance.x < -0.02f)
        {
            _sr.flipX = false;
            trDistance.x = -1f;
        }

        _anim.SetBool("isMove", true);
        _rb.velocity = new Vector2(trDistance.x * _moveSpeed, _rb.velocity.y);
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        tr = transform.position;
    }

    // **攻撃を受けたときに呼ばれるメソッド**
    public void TakeDamage()
    {
        _isChasing = true; // **攻撃を受けたら追跡開始**
    }

}
