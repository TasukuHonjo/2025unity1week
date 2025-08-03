using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class CameraOrbit : MonoBehaviour
    {
        public Transform target;   // �I�u�W�F�N�gA�i���S�ɉ��Ώہj
        public float rotationSpeed = 5f;  // ��]���x����

        private Vector3 offset;    // �J�����ƃ^�[�Q�b�g�̏�������

        void Start()
        {
            if (target == null)
            {
                Debug.LogError("�^�[�Q�b�g���ݒ肳��Ă��܂���");
                enabled = false;
                return;
            }
            // �^�[�Q�b�g����̏����I�t�Z�b�g��XZ�݂̂Ŏ擾�iY�͌Œ�j
            Vector3 diff = transform.position - target.position;
            offset = new Vector3(diff.x, this.transform.position.y, diff.z);
            transform.LookAt(target.position);
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))  // �E�N���b�N�����Ă����
            {
                float mouseX = Input.GetAxis("Mouse X");  // �}�E�X�̐����ړ���

                // ��]�p�x���v�Z�i�}�E�X�ړ��ɉ����āj
                float angle = mouseX * rotationSpeed;

                // offset��Y���i������j����ɉ�]������
                offset = Quaternion.Euler(0, angle, 0) * offset;

                // �V�����J�����ʒu���v�Z�i�^�[�Q�b�g�̈ʒu��offset�𑫂��j
                Vector3 newPos = target.position + offset;

                // Y���W�̓J�����̌��݂̍������ێ�
                newPos.y = transform.position.y;

                // �J�����̈ʒu���X�V
                transform.position = newPos;

                // ��Ƀ^�[�Q�b�g������
                transform.LookAt(target.position);
            }
        }
    }
}

