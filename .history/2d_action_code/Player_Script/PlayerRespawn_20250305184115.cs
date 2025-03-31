using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpoint; // 最後に通過したセーブポイント
    private bool hasCheckpoint = false; // セーブポイントが設定されたか
    private SpriteRenderer _sr;
    private Collider2D _collider;
    private PlayerBase _pB;
    private Rigidbody2D _rb;
    private void Start()
    {
        lastCheckpoint = transform.position; // 最初の位置をデフォルトのリスポーン地点に
        _sr=GetComponent<SpriteRenderer>();
        _collider=GetComponent<Collider2D>();
        _pB=GetComponent<PlayerBase>();
        _rb=GetComponent<Rigidbody2D>();
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        hasCheckpoint = true;
    }
public void Respawn()
    {
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        // **1. プレイヤーを消す**
        _sr.enabled = false;
        _collider.enabled = false;

        // **2. 1秒待機 → 画面暗転**
        yield return new WaitForSeconds(1f);
        ScreenFade.Instance.FadeOut(); // 画面暗転開始

        // **3. さらに1秒後に復活**
        yield return new WaitForSeconds(1f);

        if (hasCheckpoint)
        {
            transform.position = lastCheckpoint; // セーブポイント位置に戻す
        }
        _pB._hp=_pB._maxHP; // HPを回復

        // **4. プレイヤーを再表示**
        _sr.enabled = true;
        _collider.enabled = true;

        // **5. 画面を明るくする**
        ScreenFade.Instance.FadeIn();

    }
    
        
    
}
