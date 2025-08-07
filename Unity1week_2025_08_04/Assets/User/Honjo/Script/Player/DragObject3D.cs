using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class DragObject3D : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer; // �n�ʗp���C���[
        private Camera mainCamera;
        private bool isDragging = false;
        [SerializeField] private GameObject dragObj;

        void Start()
        {
            mainCamera = Camera.main;
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
            }

            if (isDragging)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
                {
                    // �n�ʂ̃q�b�g�|�C���g�ɃI�u�W�F�N�g���ړ�
                    Vector3 targetPos = hit.point;
                    targetPos.y = dragObj.transform.position.y; // �I�u�W�F�N�g�̍����͕ς��Ȃ��i�n�ʂɖ��܂�Ȃ��悤�Ɂj
                    dragObj.transform.position = targetPos;
                }
            }
        }
    }

}
