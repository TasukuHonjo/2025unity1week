using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class BillBoard : MonoBehaviour
    {
        CameraManager cameraManager;

        private void Start()
        {
            cameraManager = GameObject.Find("Managers").GetComponent<CameraManager>();
        }

        void Update()
        {
            Vector3 p = cameraManager.currentCamera.transform.position;
            p.y = transform.position.y;
            transform.LookAt(p);
        }
    }
}

