using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Haruoka; // �� ChangeScene.cs ���g������

public class TitleClickToStartWithFade : MonoBehaviour
{
    [Header("�t�F�[�h�p�̍��摜")]
    public Image fadeImage;

    [Header("�t�F�[�h���x�i�b�j")]
    public float fadeDuration = 1.0f;

    private bool isTransitioning = false;

    void Start()
    {
        if (fadeImage != null)
        {
            // �ŏ��Ɋ��S�ɍ� �� �t�F�[�h�C��
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

            // �t�F�[�h�A�E�g �� �Q�[���V�[����
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

        // �t�F�[�h�A�E�g������ɃQ�[���V�[����
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
