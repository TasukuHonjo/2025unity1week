using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Honjo
{
    public class Motor : MonoBehaviour
    {
        Rigidbody rb = null;

        [SerializeField]float maxSpeed = 10;//���x���
        [SerializeField] float moveTime = 5;//���b������
        float time = 0;

        [SerializeField] bool startFg = false;

        [SerializeField] float power = 5;//�������p���[
        Transform cameraTransform = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            time = 0;
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            //���x���
            if (rb.velocity.magnitude > maxSpeed)
            {
                // ���݂̑��x������ۂ����܂܏���X�s�[�h�ɐ���
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            if (!startFg)
            {
                if (cameraTransform == null) return;

                // �J���������� �̃x�N�g�����擾
                Vector3 toCamera = cameraTransform.position - transform.position;

                // ���Ε����������i�w��������j
                Vector3 lookDirection = -toCamera.normalized;

                // Y���͂��̂܂܂Ő�����]�����Ɍ���i�C�Ӂj
                lookDirection.y = 0f;

                if (lookDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                if(!startFg)startFg = true;
            }
        }

        private void FixedUpdate()
        {
            if (!startFg) return;
                time += Time.fixedDeltaTime;
            if (time > moveTime) 
            {
                time = 0;
                startFg = false;
                return;
            }

            rb.AddForce(transform.forward * power, ForceMode.Force);
        }
    }
}

