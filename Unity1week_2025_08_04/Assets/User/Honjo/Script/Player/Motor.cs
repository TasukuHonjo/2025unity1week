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

        [SerializeField] Vector2 vec2 = new Vector2();//�ړ��������
        [SerializeField] float power = 5;//�������p���[

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

