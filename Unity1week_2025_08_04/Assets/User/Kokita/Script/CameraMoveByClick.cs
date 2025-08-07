using UnityEngine;
using System.Collections;
using Haruoka; // ← ChangeScene クラスを使うために必要！

public class CameraMoveByClick : MonoBehaviour
{
    [Header("漫画の各コマ位置（最初の位置も含めて順番に）")]
    public Transform[] comicFrames;

    [Header("カメラの移動速度")]
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

        // 最後のフレームまで到達して、さらにクリックされたらタイトルへ
        if (reachedFinalFrame && Input.GetMouseButtonDown(0))
        {
            ChangeScene.Load_TitleScene();
            return;
        }

        // 通常のコマ送り
        if (Input.GetMouseButtonDown(0))
        {
            if (currentFrameIndex < comicFrames.Length)
            {
                StartCoroutine(MoveToFrame(comicFrames[currentFrameIndex]));
                currentFrameIndex++;

                // 最後の移動であるか確認
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
            float easedT = t * t * (3f - 2f * t); // イージング
            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }
}
