using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class NPCReaction : MonoBehaviour
    {
        [Header("�摜�ݒ�")]
        [SerializeField] private Sprite defaultSprite; // ������Ԃ̃X�v���C�g
        [SerializeField] private Sprite hitSprite;     // �v���C���[���ڐG�����Ƃ��ɕ\������X�v���C�g

        [Header("�����ݒ�")]
        [SerializeField] private float rotateDuration = 0.3f;    // ��]�A�j���[�V�����̎���
        [SerializeField] private float destroyDelay = 1.0f;      // �폜�܂ł̎���
        [SerializeField] private float launchForce = 5f;         // ��΂��Ƃ��̗�
        [SerializeField] private float stopAfterSeconds = 0.3f;  // �Œ肷��܂ł̎���

        private SpriteRenderer spriteRenderer; // �X�v���C�g�`��p
        private Rigidbody rb;                  // ���������p
        private Collider col;                  // �����蔻��p
        private bool alreadyHit = false;       // ��d�����h�~�t���O

        private bool followCameraMovement = false; // �J�����̈ړ��ʂ�NPC�ɉ����邩�ǂ���

        private bool followCamera = false;       // �J�����Ǐ]���邩�ǂ���

        private Vector3 initialCameraOffset;     // �J�����̏����ړ��ʁi�����j
        private Vector3 initialSelfPosition;     // �����̈ʒu���L�^

        private void Awake()
        {
            // �R���|�[�l���g�擾
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            // �����X�v���C�g�ݒ�
            if (defaultSprite != null)
                spriteRenderer.sprite = defaultSprite;

            // Rigidbody�������i�ÓI�Ɂj
            if (rb != null)
                rb.isKinematic = true;

            // �g���K�[����L�����iCollider�͂Ԃ��炸���ɓ��邾���j
            if (col != null)
                col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // ���łɏ�������Ă��� or �v���C���[�ȊO�Ȃ疳��
            if (alreadyHit) return;

            if (other.CompareTag("Player"))
            {
                alreadyHit = true;

                // �����蔻��𖳌����i���d������h���j
                if (col != null)
                    col.enabled = false;

                // �J�����ړ��ɒǏ]�J�n
                followCameraMovement = true;

                // �R���[�`���ŉ��o�X�^�[�g
                StartCoroutine(HandleHit());
            }
        }

        private void Update()
        {
            // �J�������������������ʒu���X�V���ĒǏ]
            if (followCamera)
            {
                Vector3 cameraDelta = CameraMotionTracker.CameraOffsetSinceStart - initialCameraOffset;
                transform.position = initialSelfPosition + cameraDelta;
            }
        }

        private System.Collections.IEnumerator HandleHit()
        {
            // �����_���ɍ��� or �E��֔�΂�
            Vector3 direction = (Random.value < 0.5f) ? new Vector3(-1f, 1f, 0f) : new Vector3(1f, 1f, 0f);
            direction.Normalize();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(direction * launchForce, ForceMode.Impulse);
            }

            // ��΂������ƌŒ�܂ŏ����ҋ@
            yield return new WaitForSeconds(stopAfterSeconds);

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }

            // ��]�A�j���[�V�����i������Ɓj
            float elapsed = 0f;
            Quaternion startRot = transform.rotation;
            Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

            while (elapsed < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = endRot;

            // �X�v���C�g�����ւ�
            if (hitSprite != null)
                spriteRenderer.sprite = hitSprite;

            // �^�O�ɉ������X�R�A���Z
            ScoreManager.Instance?.AddScoreByTag(gameObject.tag);

            // �J�����Ǐ]���I��
            followCameraMovement = false;

            // ��莞�Ԍ�ɍ폜
            Destroy(gameObject, destroyDelay);
        }
    }

}
