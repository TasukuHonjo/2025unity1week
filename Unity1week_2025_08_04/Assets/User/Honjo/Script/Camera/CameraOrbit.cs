using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class CameraOrbit : MonoBehaviour
    {
        public Transform target;       // オブジェクトA
        public float rotationSpeed = 5f;
        public float followSpeed = 10f;  // 追従のスムーズさ（大きいほど早い）

        private Vector3 offset;

        void Start()
        {
            if (target == null)
            {
                Debug.LogError("ターゲットが設定されていません");
                enabled = false;
                return;
            }

            Vector3 diff = transform.position - target.position;
            offset = new Vector3(diff.x, 0, diff.z);
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float angle = mouseX * rotationSpeed;

                offset = Quaternion.Euler(0, angle, 0) * offset;
            }

            Vector3 desiredPosition = target.position + offset;
            desiredPosition.y = transform.position.y;  // Yは今のカメラの高さを維持

            // スムーズに追従（Lerp）
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            //transform.LookAt(target.position);
        }
    }
}

