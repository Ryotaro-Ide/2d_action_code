using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn _pR = other.GetComponent<PlayerRespawn>();
            if (_pR != null)
            {
                _pR.SetCheckpoint(transform.position); // セーブポイントを更新
            }
        }
    }
}
