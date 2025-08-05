using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Honjo
{
    public class Motor : MonoBehaviour
    {
        Rigidbody rb = null;

        [SerializeField] float maxSpeed = 10;//���x���
        [SerializeField] float moveTime = 5;//���b������
        float time = 0;

        [SerializeField] bool startFg = false;

        [SerializeField] float power = 5;//�������p���[

        [SerializeField]Camera topViewCamera = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            time = 0;
        }

        private void Update()
        {
            //���x���
            if (rb.velocity.magnitude > maxSpeed)
            {
                // ���݂̑��x������ۂ����܂܏���X�s�[�h�ɐ���
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }


            if (Input.GetMouseButtonDown(0))
            {
                DriveFg();
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
            // Debug�pRay��Scene�r���[�ɕ\��
            Ray ray = new Ray(transform.position,transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            rb.AddForce(transform.forward * power, ForceMode.Force);
        }

        public void DriveFg()
        {
            if (!startFg) startFg = true;
            topViewCamera.depth = -2;
        }
    }
}

