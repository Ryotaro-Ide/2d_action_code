using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06 : MonoBehaviour
{
    public Collider2D leftWallCollider;  // 左の壁判定コライダー
    public Collider2D rightWallCollider; // 右の壁判定コライダー
    private int _xDir;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    public float moveSpeed = 2f;   // 横移動の速度
    public float waveHeight = 1f;  // 上下の振れ幅
    public float waveSpeed = 2f;   // 上下の速度

    private float startY; // 初期Y座標
    private float time;   // 時間経過管理

    private void Awake() {
        _xDir = -1;
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y; // 初期のY座標を記録
    }

    private void Update() {
        // 時間経過を更新
        time += Time.deltaTime;

        // **蛇行移動**
        float waveOffset = Mathf.Sin(time * waveSpeed) * waveHeight; // 上下移動を計算
        float newY = startY + waveOffset;

        // **移動を適用**
        _rb.velocity = new Vector2(_xDir * moveSpeed, 0); // X方向に移動
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // Y方向にスムーズ移動

        // **向きを変える**
        _sr.flipX = _xDir == 1 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Floor") {
            if (leftWallCollider.IsTouching(other)) {
                _xDir = 1;
            } else if (rightWallCollider.IsTouching(other)) {
                _xDir = -1;
            }
        }
    }
}
