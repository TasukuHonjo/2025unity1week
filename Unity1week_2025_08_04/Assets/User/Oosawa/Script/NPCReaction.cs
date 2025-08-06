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

        [Header("カメラ（未指定ならMainCamera）")]
        public Transform cameraTransform;
        public float forwardOffset = 15f;       // カメラの前方向に何メートル出すか

        private bool followCamera = false;      // カメラに追従するかどうか
        public float knockbackForce = 10f;

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

            // カメラが未設定ならMainCameraを使用
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // すでに処理されている or プレイヤー以外なら無視
            if (alreadyHit) return;

            // カメラに追従するフラグを有効化
            followCamera = true; 

            if (other.CompareTag("Player"))
            {
                alreadyHit = true;

                // 当たり判定を無効化（多重反応を防ぐ）
                if (col != null)
                    col.enabled = false;

                // コルーチンで演出スタート
                StartCoroutine(HandleHit());
            }
        }

        private void FixedUpdate()
        {
            if (followCamera)
            {
                // カメラの子に設定（ローカル位置を使いたいので false）
                transform.SetParent(cameraTransform, false);

                // カメラの前方向に forwardOffset 分だけ移動（ローカル空間）
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forwardOffset);


                followCamera = false; // 一度だけ追従するためフラグをリセット
            }
        }

        private System.Collections.IEnumerator HandleHit()
        {
            // ランダムに左上または右上へ飛ばす
            Vector3 direction = (Random.value < 0.5f) ? new Vector3(-1f, 1f, 0f) : new Vector3(1f, 1f, 0f);
            direction.Normalize(); // 念のため正規化

            if (rb != null)
            {
                rb.isKinematic = false; // 動かせるように
                rb.velocity = Vector3.zero; // 念のため初期化
                rb.AddForce(direction * launchForce, ForceMode.Impulse); // 力を加える
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

            // 一定時間後に削除
            Destroy(gameObject, destroyDelay);

            // カメラ追従フラグを無効化
            followCamera = false;
        }
    }
}
