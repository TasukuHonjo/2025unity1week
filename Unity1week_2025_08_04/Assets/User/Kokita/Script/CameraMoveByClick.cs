using UnityEngine;
using System.Collections;

public class CameraMoveByClick : MonoBehaviour
{
    [Header("漫画の各コマ位置（最初の位置も含めて順番に）")]
    public Transform[] comicFrames;

    [Header("カメラの移動速度")]
    public float moveSpeed = 1f; // 値を下げるとゆっくりになる

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

            // イージング関数（ease in-out）
            float easedT = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }
}
