using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float breakTime = 2f; // 乗ってから壊れるまでの時間
    [SerializeField] private float respawnTime = 3f; // 壊れてから復活するまでの時間

    private SpriteRenderer _sr;
    private Collider2D _collider;
    private Color originalColor;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        originalColor = _sr.color; // 元の色を記録
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakBlock());
        }
    }

    private IEnumerator BreakBlock()
    {
        // **色を緑に変更**
        _sr.color = Color.green;

        // **指定時間後にブロックを壊す**
        yield return new WaitForSeconds(breakTime);
        _collider.enabled = false; // 衝突判定を無効化
        _sr.enabled = false; // 見た目を消す

        // **さらに指定時間後に復活**
        yield return new WaitForSeconds(respawnTime);
        _collider.enabled = true; // 衝突判定を復活
        _sr.enabled = true; // 見た目を復活
        _sr.color = originalColor; // 色を元に戻す
    }
}
