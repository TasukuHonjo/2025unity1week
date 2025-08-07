using UnityEngine;
using System.Collections;
using Haruoka; // �� ChangeScene �N���X���g�����߂ɕK�v�I

public class CameraMoveByClick : MonoBehaviour
{
    [Header("����̊e�R�}�ʒu�i�ŏ��̈ʒu���܂߂ď��ԂɁj")]
    public Transform[] comicFrames;

    [Header("�J�����̈ړ����x")]
    public float moveSpeed = 1f;

    private int currentFrameIndex = 1;
    private bool isMoving = false;
    private bool reachedFinalFrame = false;

    void Start()
    {
        if (comicFrames.Length > 0)
        {
            transform.position = comicFrames[0].position;
        }
    }

    void Update()
    {
        if (isMoving) return;

        // �Ō�̃t���[���܂œ��B���āA����ɃN���b�N���ꂽ��^�C�g����
        if (reachedFinalFrame && Input.GetMouseButtonDown(0))
        {
            ChangeScene.Load_TitleScene();
            return;
        }

        // �ʏ�̃R�}����
        if (Input.GetMouseButtonDown(0))
        {
            if (currentFrameIndex < comicFrames.Length)
            {
                StartCoroutine(MoveToFrame(comicFrames[currentFrameIndex]));
                currentFrameIndex++;

                // �Ō�̈ړ��ł��邩�m�F
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
            float easedT = t * t * (3f - 2f * t); // �C�[�W���O
            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }
}
