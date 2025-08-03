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

            // デバッグ可視化
            Debug.DrawRay(transform.position, GetCameraCenterDirectionOnXZ() * 5f, Color.green);
        }

        Vector3 GetCameraCenterDirectionOnXZ()
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // 画面中央のRay
            Plane ground = new Plane(Vector3.up, transform.position); // Y=オブジェクトの高さの平面

            if (ground.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance); // 画面中央がXZ平面と交差する点
                Vector3 direction = hitPoint - transform.position;
                direction.y = 0; // XZ方向だけに制限
                return direction.normalized;
            }

            return Vector3.zero;
        }
    }
}

