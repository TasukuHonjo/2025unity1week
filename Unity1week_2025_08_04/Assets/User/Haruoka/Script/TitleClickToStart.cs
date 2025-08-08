using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Haruoka; // ← ChangeScene.cs を使うため

public class TitleClickToStartWithFade : MonoBehaviour
{
    [Header("フェード用の黒画像")]
    public Image fadeImage;

    [Header("フェード速度（秒）")]
    public float fadeDuration = 1.0f;

    private bool isTransitioning = false;

    void Start()
    {
        if (fadeImage != null)
        {
            // 最初に完全に黒 → フェードイン
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            // フェードアウト → ゲームシーンへ
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(t / fadeDuration);
            SetFadeAlpha(alpha);
            yield return null;
        }
        SetFadeAlpha(0f);
    }

    IEnumerator FadeOutAndChangeScene()
    {
        isTransitioning = true;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            SetFadeAlpha(alpha);
            yield return null;
        }
        SetFadeAlpha(1f);

        // フェードアウト完了後にゲームシーンへ
        ChangeScene.Load_GameScene();
    }

    void SetFadeAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}
