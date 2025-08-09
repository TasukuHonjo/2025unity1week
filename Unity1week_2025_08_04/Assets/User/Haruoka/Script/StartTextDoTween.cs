using UnityEngine;
using DG.Tweening; // DoTweenの名前空間

public class StartTextDoTween : MonoBehaviour
{
    [Header("対象UI")]
    public RectTransform target; // アニメーションさせたいUI（例: STARTテキスト）

    [Header("SE設定")]
    public AudioSource audioSource;
    public AudioClip startSE;

    [Header("アニメーション設定")]
    public float rotateTime = 0.5f; // 回転時間
    public float bounceScale = 1.2f; // バウンス時の最大スケール
    public float bounceTime = 0.2f;  // バウンスアップの時間

    void Start()
    {
        PlayStartAnimation();
    }

    public void PlayStartAnimation()
    {
        // 初期化
        target.localScale = Vector3.one;
        target.localRotation = Quaternion.identity;

        // 回転 → バウンス
        target
            .DORotate(new Vector3(0, 720, 0), rotateTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // SE再生
                if (audioSource != null && startSE != null)
                {
                    audioSource.PlayOneShot(startSE);
                }
                target.DOScale(Vector3.one * bounceScale, bounceTime)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        target.DOScale(Vector3.one, bounceTime).SetEase(Ease.OutBounce);
                    });
            });
    }
}
