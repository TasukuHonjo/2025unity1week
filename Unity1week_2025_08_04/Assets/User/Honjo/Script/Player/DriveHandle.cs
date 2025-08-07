using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class DriveHandle : MonoBehaviour
    {
        Rigidbody rb;
        float click = 0;
        float lateClick = 0;

        [SerializeField]float rotationPerClick = 30f; // 1�N���b�N�ŉ�]����p�x�i��F30�x�j
        float smoothSpeed = 5f;       // ��ԃX�s�[�h

        float targetYRotation = 0f;   // �ڕW��Y�p�x

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            targetYRotation = transform.eulerAngles.y;
        }

        private void Update()
        {
            // ���N���b�N�ō�����
            if (Input.GetMouseButtonDown(1))
            {
                ++click;
                targetYRotation += rotationPerClick;
            }

            // �E�N���b�N�ŉE����
            if (Input.GetMouseButtonDown(0))
            {
                --click;
                targetYRotation -= rotationPerClick;
            }

            // ���݂̊p�x���擾���ĕ��
            Quaternion current = transform.rotation;
            Quaternion target = Quaternion.Euler(0f, targetYRotation, 0f);
            transform.rotation = Quaternion.Lerp(current, target, Time.deltaTime * smoothSpeed);

            // �x���ϐ��i�����g���Ă�Ȃ�ێ��j
            if (lateClick > click)
            {
                lateClick += Time.deltaTime;
            }
            if (lateClick < click)
            {
                lateClick -= Time.deltaTime;
            }
        }

        public void SetTargetYRotation(float rotationY)
        {
            targetYRotation = rotationY;
        }
    }
}

