using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Haruoka;

public class CameraMoveByClick : MonoBehaviour
{
    [Header("漫画の各コマ位置（最初の位置も含めて順番に）")]
    public Transform[] comicFrames;

    [Header("各コマに対応する効果音（数を合わせる）")]
    public AudioClip[] frameSEs;

    [Header("最後のシーン遷移時の効果音（任意）")]
    public AudioClip finalSE;

    [Header("SE再生用AudioSource")]
    public AudioSource audioSource;

    [Header("フェード用パネル")]
    public Image fadePanel;
    public float fadeDuration = 1.0f;

    [Header("カメラの移動速度")]
    public float moveSpeed = 1f;

    private int currentFrameIndex = 1;
    private bool isMoving = false;
    private bool reachedFinalFrame = false;
    private bool isFading = false;

    void Start()
    {
        if (comicFrames.Length > 0)
        {
            transform.position = comicFrames[0].position;
        }

        if (fadePanel != null)
        {
            SetAlpha(fadePanel, 1f); // 最初は黒
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        if (isMoving || isFading) return;

        if (reachedFinalFrame && Input.GetMouseButtonDown(0))
        {
            PlaySE(finalSE);
            StartCoroutine(FadeOutAndLoadTitle());
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentFrameIndex < comicFrames.Length)
            {
                PlaySE(GetFrameSE(currentFrameIndex));
                StartCoroutine(MoveToFrame(comicFrames[currentFrameIndex]));
                currentFrameIndex++;

                if (currentFrameIndex >= comicFrames.Length)
                {
                    reachedFinalFrame = true;
                }
            }
        }
    }

    IEnumerator MoveToFrame(Transform target)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = target.position;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            float easedT = t * t * (3f - 2f * t); // easeInOut
            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            SetAlpha(fadePanel, t);
            yield return null;
        }
        SetAlpha(fadePanel, 0f);
        isFading = false;
    }

    IEnumerator FadeOutAndLoadTitle()
    {
        isFading = true;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            SetAlpha(fadePanel, t);
            yield return null;
        }

        SetAlpha(fadePanel, 1f);
        yield return new WaitForSeconds(0.2f);
        ChangeScene.Load_TitleScene();
    }

    void SetAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color c = img.color;
            c.a = Mathf.Clamp01(alpha);
            img.color = c;
        }
    }

    void PlaySE(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    AudioClip GetFrameSE(int index)
    {
        if (frameSEs != null && index < frameSEs.Length)
        {
            return frameSEs[index];
        }
        return null;
    }
}
