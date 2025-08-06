using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class CameraMotionTracker : MonoBehaviour
    {
        // カメラの初期位置からの移動量（累積）
        public static Vector3 CameraOffsetSinceStart { get; private set; }

        private Vector3 initialPosition;

        private void Start()
        {
            // 初期カメラ位置を記録
            initialPosition = transform.position;
        }

        private void LateUpdate()
        {
            // 現在位置との差分を毎フレーム更新
            CameraOffsetSinceStart = transform.position - initialPosition;
        }
    }

}