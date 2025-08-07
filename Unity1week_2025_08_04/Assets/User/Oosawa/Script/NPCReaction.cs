using Honjo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{

    public class NPCReaction : MonoBehaviour
    {
        [Header("�X�v���C�g�ݒ�")]
        public Sprite beforeSprite; // �ՓˑO�̌�����
        public Sprite afterSprite;  // �Փˌ�̌�����

        [SerializeField] private float rotateDuration = 0.3f;    // ��]�A�j���[�V�����̎���

        [Header("������ы���")]
        public float blowForce = 30f;        // ���������̐�����ї�
        public float upwardForce = 2f;       // ������̒��˗�
        public float stopDelay = 0.5f;       // �͂��~�߂�܂ł̎���
        public float destroyDelay = 1.5f;    // ������܂ł̎���

        [Header("��]����")]
        public float rotationSpeed = 400f; // Y����]���x�i�x/�b�j

        // �������
        private SpriteRenderer spriteRenderer;
        private Rigidbody rb;
        private Transform player;

        void Awake()
        {
            // �K�v�ȃR���|�[�l���g�擾
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();

            // �����X�v���C�g�ݒ�
            spriteRenderer.sprite = beforeSprite;

            // �v���C���[��Transform�擾�i�^�O�Ō����j
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // NPC��Y����360�x��]���A180�x�ŃX�v���C�g��؂�ւ��鏈��
        private IEnumerator RotateAndSwitchSprite()
        {
            // ��]�A�j���[�V�����i������Ɓj
            float elapsed = 0f;
            Quaternion startRot = transform.rotation;
            Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

            spriteRenderer.sprite = afterSprite;

            while (elapsed < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        // �v���C���[�ƏՓ˂����ۂ̏���
        void OnTriggerEnter(Collider other)
        {
            // �v���C���[�ƏՓ˂���������i�^�O�g�p�j
            if (other.CompareTag("Player"))
            {
                // �v���C���[�̕������擾
                Vector3 dirToPlayer = (transform.position - other.transform.position).normalized;
                Vector3 playerForward = other.transform.forward;
                Vector3 playerRight = other.transform.right;

                // ������ѕ����F�v���C���[�̉E�O�܂��͍��O��������Ƀ����_������
                bool goLeft = Random.value < 0.5f;

                // ���O: forward - right / �E�O: forward + right
                Vector3 baseDirection = (goLeft) ?
                    (playerForward - playerRight).normalized :
                    (playerForward + playerRight).normalized;

                // ������уx�N�g���Ƀ����_���Ȃ�炬��������i�ɂ�񂱂��ۂ��j
                Vector3 randomOffset = new Vector3(
                    0f,
                    Random.Range(-0.5f, 0.5f), // ����������炮
                    0f
                );

                Vector3 finalDirection = (baseDirection + randomOffset).normalized;

                // ���˂�悤�ɗ͂�������i�ɂ�񂱓I�����j
                Vector3 force = finalDirection * blowForce + Vector3.up * upwardForce;
                rb.velocity = Vector3.zero; // ���O�̑��x�����Z�b�g
                rb.AddForce(force, ForceMode.Impulse);

                // �X�v���C�g��؂�ւ���
                StartCoroutine(RotateAndSwitchSprite());

                // �͂��~�߂鏈�����������X�P�W���[��
                StartCoroutine(StopMovementAfterDelay());
                Destroy(gameObject, destroyDelay);
            }
        }

        // ��莞�Ԍ�ɐ�����т��~����i�͂��[���ɂ���j
        private IEnumerator StopMovementAfterDelay()
        {
            yield return new WaitForSeconds(stopDelay);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // ���S�ɒ�~������
        }
    }
}
