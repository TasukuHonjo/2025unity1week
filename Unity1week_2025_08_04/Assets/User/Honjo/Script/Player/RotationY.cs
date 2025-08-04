using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class RotationY : MonoBehaviour
    {
        public float rotationSpeed = 5f;

        void Update()
        {
            if (Input.GetMouseButton(1)) // 右クリック中
            {
                float mouseX = Input.GetAxis("Mouse X");
                transform.Rotate(0f, mouseX * rotationSpeed, 0f, Space.World);
            }
        }
    }
}

