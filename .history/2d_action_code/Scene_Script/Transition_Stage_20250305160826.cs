using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
{
    [SerializeField] private string nextSceneName; // 遷移するシーンの名前

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーが入ったら
        {
            SceneManager.LoadScene(nextSceneName); // 指定したシーンへ遷移
        }
    }
}
