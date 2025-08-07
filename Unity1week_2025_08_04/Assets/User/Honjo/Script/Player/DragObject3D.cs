using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class DragObject3D : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer; // 地面用レイヤー
        private Camera mainCamera;
        private bool isDragging = false;
        [SerializeField] private GameObject dragObj;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            // マウスボタンを押したらドラッグ開始
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }

            // マウスボタンを離したらドラッグ終了
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
                {
                    // 地面のヒットポイントにオブジェクトを移動
                    Vector3 targetPos = hit.point;
                    targetPos.y = dragObj.transform.position.y; // オブジェクトの高さは変えない（地面に埋まらないように）
                    dragObj.transform.position = targetPos;
                }
            }
        }
    }

}
