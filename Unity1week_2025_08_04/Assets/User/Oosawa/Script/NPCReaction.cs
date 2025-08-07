using Honjo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{

    public class NPCReaction : MonoBehaviour
    {
        [Header("スプライト設定")]
        public Sprite beforeSprite; // 衝突前の見た目
        public Sprite afterSprite;  // 衝突後の見た目

        [SerializeField] private float rotateDuration = 0.3f;    // 回転アニメーションの時間

        [Header("吹き飛び挙動")]
        public float blowForce = 30f;        // 水平方向の吹き飛び力
        public float upwardForce = 2f;       // 上方向の跳ね力
        public float stopDelay = 0.5f;       // 力を止めるまでの時間
        public float destroyDelay = 1.5f;    // 消えるまでの時間

        [Header("回転挙動")]
        public float rotationSpeed = 400f; // Y軸回転速度（度/秒）

        // 内部状態
        private SpriteRenderer spriteRenderer;
        private Rigidbody rb;
        private Transform player;

        void Awake()
        {
            // 必要なコンポーネント取得
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();

            // 初期スプライト設定
            spriteRenderer.sprite = beforeSprite;

            // プレイヤーのTransform取得（タグで検索）
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // NPCをY軸で360度回転し、180度でスプライトを切り替える処理
        private IEnumerator RotateAndSwitchSprite()
        {
            // 回転アニメーション（くるっと）
            float elapsed = 0f;
            Quaternion startRot = transform.rotation;
            Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

            spriteRenderer.sprite = afterSprite;

            while (elapsed < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        // プレイヤーと衝突した際の処理
        void OnTriggerEnter(Collider other)
        {
            // プレイヤーと衝突したか判定（タグ使用）
            if (other.CompareTag("Player"))
            {
                // プレイヤーの方向を取得
                Vector3 dirToPlayer = (transform.position - other.transform.position).normalized;
                Vector3 playerForward = other.transform.forward;
                Vector3 playerRight = other.transform.right;

                // 吹き飛び方向：プレイヤーの右前または左前方向を基準にランダム決定
                bool goLeft = Random.value < 0.5f;

                // 左前: forward - right / 右前: forward + right
                Vector3 baseDirection = (goLeft) ?
                    (playerForward - playerRight).normalized :
                    (playerForward + playerRight).normalized;

                // 吹き飛びベクトルにランダムなゆらぎを加える（にゃんこっぽく）
                Vector3 randomOffset = new Vector3(
                    0f,
                    Random.Range(-0.5f, 0.5f), // 高さだけゆらぐ
                    0f
                );

                Vector3 finalDirection = (baseDirection + randomOffset).normalized;

                // 跳ねるように力を加える（にゃんこ的挙動）
                Vector3 force = finalDirection * blowForce + Vector3.up * upwardForce;
                rb.velocity = Vector3.zero; // 直前の速度をリセット
                rb.AddForce(force, ForceMode.Impulse);

                // スプライトを切り替える
                StartCoroutine(RotateAndSwitchSprite());

                // 力を止める処理＆消去をスケジュール
                StartCoroutine(StopMovementAfterDelay());
                Destroy(gameObject, destroyDelay);
            }
        }

        // 一定時間後に吹き飛びを停止する（力をゼロにする）
        private IEnumerator StopMovementAfterDelay()
        {
            yield return new WaitForSeconds(stopDelay);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // 完全に停止させる
        }
    }
}
