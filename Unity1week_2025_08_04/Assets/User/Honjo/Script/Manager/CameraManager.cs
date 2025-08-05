using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera topViewCamera = null;
        [SerializeField] private Camera driveCamera = null;
        public Camera currentCamera = null;

        private void Update()
        {
            if (topViewCamera.depth > driveCamera.depth)
            {
                currentCamera = topViewCamera;
            }
            else
            {
                currentCamera = driveCamera;
            }
        }
    }

}

