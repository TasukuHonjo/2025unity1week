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

        [SerializeField]float rotationPerClick = 30f; // 1クリックで回転する角度（例：30度）
        float smoothSpeed = 5f;       // 補間スピード

        float targetYRotation = 0f;   // 目標のY角度

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            targetYRotation = transform.eulerAngles.y;
        }

        private void Update()
        {
            // 左クリックで左旋回
            if (Input.GetMouseButtonDown(1))
            {
                ++click;
                targetYRotation += rotationPerClick;
            }

            // 右クリックで右旋回
            if (Input.GetMouseButtonDown(0))
            {
                --click;
                targetYRotation -= rotationPerClick;
            }

            // 現在の角度を取得して補間
            Quaternion current = transform.rotation;
            Quaternion target = Quaternion.Euler(0f, targetYRotation, 0f);
            transform.rotation = Quaternion.Lerp(current, target, Time.deltaTime * smoothSpeed);

            // 遅延変数（もし使ってるなら維持）
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

