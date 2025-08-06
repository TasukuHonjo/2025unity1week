using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class CameraMotionTracker : MonoBehaviour
    {
        // �J�����̏����ʒu����̈ړ��ʁi�ݐρj
        public static Vector3 CameraOffsetSinceStart { get; private set; }

        private Vector3 initialPosition;

        private void Start()
        {
            // �����J�����ʒu���L�^
            initialPosition = transform.position;
        }

        private void LateUpdate()
        {
            // ���݈ʒu�Ƃ̍����𖈃t���[���X�V
            CameraOffsetSinceStart = transform.position - initialPosition;
        }
    }

}