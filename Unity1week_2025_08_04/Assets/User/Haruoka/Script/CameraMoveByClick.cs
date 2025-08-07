using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Haruoka;

public class CameraMoveByClick : MonoBehaviour
{
    [Header("����̊e�R�}�ʒu�i�ŏ��̈ʒu���܂߂ď��ԂɁj")]
    public Transform[] comicFrames;

    [Header("�e�R�}�ɑΉ�������ʉ��i�������킹��j")]
    public AudioClip[] frameSEs;

    [Header("�Ō�̃V�[���J�ڎ��̌��ʉ��i�C�Ӂj")]
    public AudioClip finalSE;

    [Header("SE�Đ��pAudioSource")]
    public AudioSource audioSource;

    [Header("�t�F�[�h�p�p�l��")]
    public Image fadePanel;
    public float fadeDuration = 1.0f;

    [Header("�J�����̈ړ����x")]
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
            SetAlpha(fadePanel, 1f); // �ŏ��͍�
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
