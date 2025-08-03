using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class CameraOrbit : MonoBehaviour
    {
        public Transform target;   // オブジェクトA（中心に回る対象）
        public float rotationSpeed = 5f;  // 回転速度調整

        private Vector3 offset;    // カメラとターゲットの初期距離

        void Start()
        {
            if (target == null)
            {
                Debug.LogError("ターゲットが設定されていません");
                enabled = false;
                return;
            }
            // ターゲットからの初期オフセットをXZのみで取得（Yは固定）
            Vector3 diff = transform.position - target.position;
            offset = new Vector3(diff.x, this.transform.position.y, diff.z);
            transform.LookAt(target.position);
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))  // 右クリック押している間
            {
                float mouseX = Input.GetAxis("Mouse X");  // マウスの水平移動量

                // 回転角度を計算（マウス移動に応じて）
                float angle = mouseX * rotationSpeed;

                // offsetをY軸（上方向）周りに回転させる
                offset = Quaternion.Euler(0, angle, 0) * offset;

                // 新しいカメラ位置を計算（ターゲットの位置にoffsetを足す）
                Vector3 newPos = target.position + offset;

                // Y座標はカメラの現在の高さを維持
                newPos.y = transform.position.y;

                // カメラの位置を更新
                transform.position = newPos;

                // 常にターゲットを見る
                transform.LookAt(target.position);
            }
        }
    }
}

