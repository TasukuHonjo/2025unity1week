using UnityEngine;
using System.Collections;

public class CameraMoveByClick : MonoBehaviour
{
    [Header("����̊e�R�}�ʒu�i�ŏ��̈ʒu���܂߂ď��ԂɁj")]
    public Transform[] comicFrames;

    [Header("�J�����̈ړ����x")]
    public float moveSpeed = 1f; // �l��������Ƃ������ɂȂ�

    private int currentFrameIndex = 1;
    private bool isMoving = false;

    void Start()
    {
        if (comicFrames.Length > 0)
        {
            transform.position = comicFrames[0].position;
        }
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            if (currentFrameIndex < comicFrames.Length)
            {
                StartCoroutine(MoveToFrame(comicFrames[currentFrameIndex]));
                currentFrameIndex++;
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

            // �C�[�W���O�֐��iease in-out�j
            float easedT = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }
}
