using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class NPCReaction : MonoBehaviour
    {
        [Header("画像設定")]
        [SerializeField] private Sprite defaultSprite; // 初期状態のスプライト
        [SerializeField] private Sprite hitSprite;     // プレイヤーが接触したときに表示するスプライト

        [Header("挙動設定")]
        [SerializeField] private float rotateDuration = 0.3f;    // 回転アニメーションの時間
        [SerializeField] private float destroyDelay = 1.0f;      // 削除までの時間
        [SerializeField] private float launchForce = 5f;         // 飛ばすときの力
        [SerializeField] private float stopAfterSeconds = 0.3f;  // 固定するまでの時間

        private SpriteRenderer spriteRenderer; // スプライト描画用
        private Rigidbody rb;                  // 物理挙動用
        private Collider col;                  // 当たり判定用
        private bool alreadyHit = false;       // 二重反応防止フラグ

        private bool followCameraMovement = false; // カメラの移動量をNPCに加えるかどうか

        private bool followCamera = false;       // カメラ追従するかどうか

        private Vector3 initialCameraOffset;     // カメラの初期移動量（差分）
        private Vector3 initialSelfPosition;     // 自分の位置を記録

        private void Awake()
        {
            // コンポーネント取得
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            // 初期スプライト設定
            if (defaultSprite != null)
                spriteRenderer.sprite = defaultSprite;

            // Rigidbody初期化（静的に）
            if (rb != null)
                rb.isKinematic = true;

            // トリガー判定有効化（Colliderはぶつからず中に入るだけ）
            if (col != null)
                col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // すでに処理されている or プレイヤー以外なら無視
            if (alreadyHit) return;

            if (other.CompareTag("Player"))
            {
                alreadyHit = true;

                // 当たり判定を無効化（多重反応を防ぐ）
                if (col != null)
                    col.enabled = false;

                // カメラ移動に追従開始
                followCameraMovement = true;

                // コルーチンで演出スタート
                StartCoroutine(HandleHit());
            }
        }

        private void Update()
        {
            // カメラが動いた分だけ位置を更新して追従
            if (followCamera)
            {
                Vector3 cameraDelta = CameraMotionTracker.CameraOffsetSinceStart - initialCameraOffset;
                transform.position = initialSelfPosition + cameraDelta;
            }
        }

        private System.Collections.IEnumerator HandleHit()
        {
            // ランダムに左上 or 右上へ飛ばす
            Vector3 direction = (Random.value < 0.5f) ? new Vector3(-1f, 1f, 0f) : new Vector3(1f, 1f, 0f);
            direction.Normalize();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(direction * launchForce, ForceMode.Impulse);
            }

            // 飛ばしたあと固定まで少し待機
            yield return new WaitForSeconds(stopAfterSeconds);

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }

            // 回転アニメーション（くるっと）
            float elapsed = 0f;
            Quaternion startRot = transform.rotation;
            Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

            while (elapsed < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = endRot;

            // スプライト差し替え
            if (hitSprite != null)
                spriteRenderer.sprite = hitSprite;

            // タグに応じたスコア加算
            ScoreManager.Instance?.AddScoreByTag(gameObject.tag);

            // カメラ追従を終了
            followCameraMovement = false;

            // 一定時間後に削除
            Destroy(gameObject, destroyDelay);
        }
    }

}
