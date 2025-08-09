using UnityEngine;
using DG.Tweening; // DoTween�̖��O���

public class StartTextDoTween : MonoBehaviour
{
    [Header("�Ώ�UI")]
    public RectTransform target; // �A�j���[�V������������UI�i��: START�e�L�X�g�j

    [Header("SE�ݒ�")]
    public AudioSource audioSource;
    public AudioClip startSE;

    [Header("�A�j���[�V�����ݒ�")]
    public float rotateTime = 0.5f; // ��]����
    public float bounceScale = 1.2f; // �o�E���X���̍ő�X�P�[��
    public float bounceTime = 0.2f;  // �o�E���X�A�b�v�̎���

    void Start()
    {
        PlayStartAnimation();
    }

    public void PlayStartAnimation()
    {
        // ������
        target.localScale = Vector3.one;
        target.localRotation = Quaternion.identity;

        // ��] �� �o�E���X
        target
            .DORotate(new Vector3(0, 720, 0), rotateTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // SE�Đ�
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
