using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class ShotPlayer : MonoBehaviour
    {
        public Camera mainCamera;
        public float forcePower = 10f;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 direction = GetCameraCenterDirectionOnXZ();
                rb.AddForce(direction * forcePower, ForceMode.Impulse);
            }

            // �f�o�b�O����
            Debug.DrawRay(transform.position, GetCameraCenterDirectionOnXZ() * 5f, Color.green);
        }

        Vector3 GetCameraCenterDirectionOnXZ()
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ��ʒ�����Ray
            Plane ground = new Plane(Vector3.up, transform.position); // Y=�I�u�W�F�N�g�̍����̕���

            if (ground.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance); // ��ʒ�����XZ���ʂƌ�������_
                Vector3 direction = hitPoint - transform.position;
                direction.y = 0; // XZ���������ɐ���
                return direction.normalized;
            }

            return Vector3.zero;
        }
    }
}

