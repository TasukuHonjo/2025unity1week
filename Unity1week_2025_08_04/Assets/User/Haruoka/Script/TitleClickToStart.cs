using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Haruoka; // ChangeScene.csを使うため

public class TitleClickToStartWithFade : MonoBehaviour
{
    [Header("フェード用の黒画像")]
    public Image fadeImage;

    [Header("フェード速度（秒）")]
    public float fadeDuration = 1.0f;

    [Header("効果音")]
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private AudioClip clickSoundClip;

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

        // SEAudioSourceの設定確認
        if (seAudioSource != null)
        {
            Debug.Log($"SE AudioSource設定確認 - Volume: {seAudioSource.volume}, Enabled: {seAudioSource.enabled}");
        }
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Debug.Log("タイトル画面でクリックされました");

            // クリック音を再生
            PlayClickSound();

            // フェードアウト → ゲームシーンへ
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    private void PlayClickSound()
    {
        if (seAudioSource != null && clickSoundClip != null)
        {
            Debug.Log("クリック音を再生中...");
            seAudioSource.PlayOneShot(clickSoundClip);
        }
        else
        {
            Debug.LogWarning("AudioSourceまたはAudioClipが設定されていません");
            if (seAudioSource == null) Debug.LogWarning("seAudioSource is null");
            if (clickSoundClip == null) Debug.LogWarning("clickSoundClip is null");
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