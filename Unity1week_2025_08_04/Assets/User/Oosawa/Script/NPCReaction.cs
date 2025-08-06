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

        [Header("�J�����i���w��Ȃ�MainCamera�j")]
        public Transform cameraTransform;
        public float forwardOffset = 15f;       // �J�����̑O�����ɉ����[�g���o����

        private bool followCamera = false;      // �J�����ɒǏ]���邩�ǂ���
        public float knockbackForce = 10f;

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

            // �J���������ݒ�Ȃ�MainCamera���g�p
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // ���łɏ�������Ă��� or �v���C���[�ȊO�Ȃ疳��
            if (alreadyHit) return;

            // �J�����ɒǏ]����t���O��L����
            followCamera = true; 

            if (other.CompareTag("Player"))
            {
                alreadyHit = true;

                // �����蔻��𖳌����i���d������h���j
                if (col != null)
                    col.enabled = false;

                // �R���[�`���ŉ��o�X�^�[�g
                StartCoroutine(HandleHit());
            }
        }

        private void FixedUpdate()
        {
            if (followCamera)
            {
                // �J�����̎q�ɐݒ�i���[�J���ʒu���g�������̂� false�j
                transform.SetParent(cameraTransform, false);

                // �J�����̑O������ forwardOffset �������ړ��i���[�J����ԁj
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forwardOffset);


                followCamera = false; // ��x�����Ǐ]���邽�߃t���O�����Z�b�g
            }
        }

        private System.Collections.IEnumerator HandleHit()
        {
            // �����_���ɍ���܂��͉E��֔�΂�
            Vector3 direction = (Random.value < 0.5f) ? new Vector3(-1f, 1f, 0f) : new Vector3(1f, 1f, 0f);
            direction.Normalize(); // �O�̂��ߐ��K��

            if (rb != null)
            {
                rb.isKinematic = false; // ��������悤��
                rb.velocity = Vector3.zero; // �O�̂��ߏ�����
                rb.AddForce(direction * launchForce, ForceMode.Impulse); // �͂�������
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

            // ��莞�Ԍ�ɍ폜
            Destroy(gameObject, destroyDelay);

            // �J�����Ǐ]�t���O�𖳌���
            followCamera = false;
        }
    }
}
