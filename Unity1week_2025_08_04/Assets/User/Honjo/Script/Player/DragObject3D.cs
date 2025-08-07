using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Honjo
{
    public class DragObject3D : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer; // �n�ʗp���C���[
        private Camera mainCamera;
        private bool isDragging = false;
        [SerializeField] private GameObject dragObj;
        [SerializeField] private float maxDistance = 15;

        private Vector3 defPos = Vector3.zero;

        Vector3 latePos, currentPos;
        float value = 0f;
        [SerializeField] float maxValue = 100;
        [SerializeField] Image timer;

        void Start()
        {
            mainCamera = Camera.main;
            defPos = dragObj.transform.position;
            latePos = dragObj.transform.position;
        }

        void Update()
        {
            // �}�E�X�{�^������������h���b�O�J�n
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }

            // �}�E�X�{�^���𗣂�����h���b�O�I��
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                dragObj.transform.position = defPos;
                this.enabled = false;
            }

            if (isDragging)
            {
                currentPos = dragObj.transform.position;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundLayer))
                {
                    // �n�ʂ̃q�b�g�|�C���g�ɃI�u�W�F�N�g���ړ�
                    Vector3 targetPos = hit.point;
                    targetPos.y = dragObj.transform.position.y; // �I�u�W�F�N�g�̍����͕ς��Ȃ��i�n�ʂɖ��܂�Ȃ��悤�Ɂj
                    dragObj.transform.position = targetPos;
                }

                if (Mathf.Abs(currentPos.x - latePos.x) > 1)
                {
                    value += Mathf.Abs(currentPos.x - latePos.x);
                }
                if(Mathf.Abs(currentPos.z - latePos.z) > 1)
                {
                    value += Mathf.Abs(currentPos.z - latePos.z);
                }

                if (value > maxValue) { value = maxValue; }

                float normalized = value / maxValue;
                normalized = Mathf.Clamp01(normalized); // �O�̂��� 0?1 �ɐ���
                timer.fillAmount = normalized;

                latePos = currentPos;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }

}
