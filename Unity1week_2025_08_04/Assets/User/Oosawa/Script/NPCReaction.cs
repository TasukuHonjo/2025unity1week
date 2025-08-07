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

        //[Header("�v���C���[�֘A")]
        //public float triggerDistance = 5f; // �v���C���[�����m���鋗��

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

        void Update()
        {
            //// �܂��������Ă��Ȃ��ꍇ�̂݁A�v���C���[�Ƃ̋����𑪒�
            //if (!isTriggered && player != null)
            //{
            //    float distance = Vector3.Distance(transform.position, player.position);

            //    // �v���C���[���w�苗���ɓ��������]���J�n
            //    if (distance <= triggerDistance)
            //    {

            //    }
            //}
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



            //float rotated = 0f;

            //while (rotated < 360f)
            //{
            //    float step = rotationSpeed * Time.deltaTime;
            //    transform.Rotate(Vector3.up, step);
            //    rotated += step;

            //    // 180�x��]�����^�C�~���O�ŃX�v���C�g�ύX
            //    if (!hasSwitchedSprite && rotated >= 180f)
            //    {
            //        spriteRenderer.sprite = afterSprite;
            //        hasSwitchedSprite = true;
            //    }

            //    yield return null;
            //}
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



    //public class NPCReaction : MonoBehaviour
    //{
    //    [Header("�摜�ݒ�")]
    //    [SerializeField] private Sprite defaultSprite; // ������Ԃ̃X�v���C�g
    //    [SerializeField] private Sprite hitSprite;     // �v���C���[���ڐG�����Ƃ��ɕ\������X�v���C�g

    //    [Header("�����ݒ�")]
    //    [SerializeField] private float rotateDuration = 0.3f;    // ��]�A�j���[�V�����̎���
    //    [SerializeField] private float destroyDelay = 1.0f;      // �폜�܂ł̎���
    //    [SerializeField] private float launchForce = 5f;         // ��΂��Ƃ��̗�
    //    [SerializeField] private float stopAfterSeconds = 0.3f;  // �Œ肷��܂ł̎���

    //    private SpriteRenderer spriteRenderer; // �X�v���C�g�`��p
    //    private Rigidbody rb;                  // ���������p
    //    private Collider col;                  // �����蔻��p
    //    private bool alreadyHit = false;       // ��d�����h�~�t���O

    //    [Header("�J�����i���w��Ȃ�MainCamera�j")]
    //    public Transform cameraTransform;
    //    public float forwardOffset = 15f;       // �J�����̑O�����ɉ����[�g���o����

    //    private bool followCamera = false;      // �J�����ɒǏ]���邩�ǂ���
    //    public float knockbackForce = 10f;

    //    private void Awake()
    //    {
    //        // �R���|�[�l���g�擾
    //        spriteRenderer = GetComponent<SpriteRenderer>();
    //        rb = GetComponent<Rigidbody>();
    //        col = GetComponent<Collider>();

    //        // �����X�v���C�g�ݒ�
    //        if (defaultSprite != null)
    //            spriteRenderer.sprite = defaultSprite;

    //        // Rigidbody�������i�ÓI�Ɂj
    //        if (rb != null)
    //            rb.isKinematic = true;

    //        // �g���K�[����L�����iCollider�͂Ԃ��炸���ɓ��邾���j
    //        if (col != null)
    //            col.isTrigger = true;

    //        // �J���������ݒ�Ȃ�MainCamera���g�p
    //        if (cameraTransform == null)
    //        {
    //            cameraTransform = Camera.main.transform;
    //        }
    //    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //        // ���łɏ�������Ă��� or �v���C���[�ȊO�Ȃ疳��
    //        if (alreadyHit) return;

    //        // �J�����ɒǏ]����t���O��L����
    //        followCamera = true; 

    //        if (other.CompareTag("Player"))
    //        {
    //            alreadyHit = true;

    //            // �����蔻��𖳌����i���d������h���j
    //            if (col != null)
    //                col.enabled = false;

    //            // �R���[�`���ŉ��o�X�^�[�g
    //            StartCoroutine(HandleHit());
    //        }
    //    }

    //    private void FixedUpdate()
    //    {
    //        if (followCamera)
    //        {
    //            // �J�����̎q�ɐݒ�i���[�J���ʒu���g�������̂� false�j
    //            transform.SetParent(cameraTransform, false);

    //            // �J�����̑O������ forwardOffset �������ړ��i���[�J����ԁj
    //            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forwardOffset);
    //        }
    //    }

    //    private System.Collections.IEnumerator HandleHit()
    //    {
    //        // �����_���ɍ���܂��͉E��֔�΂�
    //        Vector3 direction = (Random.value < 0.5f) ? new Vector3(-10f, 1f, 0f) : new Vector3(10f, 1f, 0f);
    //        direction.Normalize(); // �O�̂��ߐ��K��

    //        if (rb != null)
    //        {
    //            rb.isKinematic = false; // ��������悤��
    //            rb.velocity = Vector3.zero; // �O�̂��ߏ�����
    //            rb.AddForce(direction * launchForce, ForceMode.Impulse); // �͂�������
    //        }

    //        // ��΂������ƌŒ�܂ŏ����ҋ@
    //        yield return new WaitForSeconds(stopAfterSeconds);

    //        if (rb != null)
    //        {
    //            rb.velocity = Vector3.zero;
    //            rb.isKinematic = true;
    //        }

    //        // ��]�A�j���[�V�����i������Ɓj
    //        float elapsed = 0f;
    //        Quaternion startRot = transform.rotation;
    //        Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

    //        while (elapsed < rotateDuration)
    //        {
    //            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
    //            elapsed += Time.deltaTime;
    //            yield return null;
    //        }

    //        transform.rotation = endRot;

    //        // �X�v���C�g�����ւ�
    //        if (hitSprite != null)
    //            spriteRenderer.sprite = hitSprite;

    //        // �^�O�ɉ������X�R�A���Z
    //        ScoreManager.Instance?.AddScoreByTag(gameObject.tag);

    //        // ��莞�Ԍ�ɍ폜
    //        Destroy(gameObject, destroyDelay);

    //        // �J�����Ǐ]�t���O�𖳌���
    //        followCamera = false;
    //    }
    //}
}
