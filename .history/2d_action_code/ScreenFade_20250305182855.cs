using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance { get; private set; }
    private Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeImage = GetComponentInChildren<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // 最初は透明
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f)); // 完全に暗くする
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f)); // 明るくする
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float duration = 1f;
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}

