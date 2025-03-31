using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpoint; // 最後に通過したセーブポイント
    private bool hasCheckpoint = false; // セーブポイントが設定されたか

    private void Start()
    {
        lastCheckpoint = transform.position; // 最初の位置をデフォルトのリスポーン地点に
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        hasCheckpoint = true;
    }

    public void Respawn()
    {
        if (hasCheckpoint)
        {
            transform.position = lastCheckpoint; // セーブポイントに移動
        }
        else
        {
            Debug.LogWarning("セーブポイントが設定されていません。");
        }
    }
}
