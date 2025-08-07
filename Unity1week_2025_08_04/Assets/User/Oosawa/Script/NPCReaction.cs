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

        //[Header("プレイヤー関連")]
        //public float triggerDistance = 5f; // プレイヤーを検知する距離

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

        void Update()
        {
            //// まだ反応していない場合のみ、プレイヤーとの距離を測定
            //if (!isTriggered && player != null)
            //{
            //    float distance = Vector3.Distance(transform.position, player.position);

            //    // プレイヤーが指定距離に入ったら回転を開始
            //    if (distance <= triggerDistance)
            //    {

            //    }
            //}
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



            //float rotated = 0f;

            //while (rotated < 360f)
            //{
            //    float step = rotationSpeed * Time.deltaTime;
            //    transform.Rotate(Vector3.up, step);
            //    rotated += step;

            //    // 180度回転したタイミングでスプライト変更
            //    if (!hasSwitchedSprite && rotated >= 180f)
            //    {
            //        spriteRenderer.sprite = afterSprite;
            //        hasSwitchedSprite = true;
            //    }

            //    yield return null;
            //}
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



    //public class NPCReaction : MonoBehaviour
    //{
    //    [Header("画像設定")]
    //    [SerializeField] private Sprite defaultSprite; // 初期状態のスプライト
    //    [SerializeField] private Sprite hitSprite;     // プレイヤーが接触したときに表示するスプライト

    //    [Header("挙動設定")]
    //    [SerializeField] private float rotateDuration = 0.3f;    // 回転アニメーションの時間
    //    [SerializeField] private float destroyDelay = 1.0f;      // 削除までの時間
    //    [SerializeField] private float launchForce = 5f;         // 飛ばすときの力
    //    [SerializeField] private float stopAfterSeconds = 0.3f;  // 固定するまでの時間

    //    private SpriteRenderer spriteRenderer; // スプライト描画用
    //    private Rigidbody rb;                  // 物理挙動用
    //    private Collider col;                  // 当たり判定用
    //    private bool alreadyHit = false;       // 二重反応防止フラグ

    //    [Header("カメラ（未指定ならMainCamera）")]
    //    public Transform cameraTransform;
    //    public float forwardOffset = 15f;       // カメラの前方向に何メートル出すか

    //    private bool followCamera = false;      // カメラに追従するかどうか
    //    public float knockbackForce = 10f;

    //    private void Awake()
    //    {
    //        // コンポーネント取得
    //        spriteRenderer = GetComponent<SpriteRenderer>();
    //        rb = GetComponent<Rigidbody>();
    //        col = GetComponent<Collider>();

    //        // 初期スプライト設定
    //        if (defaultSprite != null)
    //            spriteRenderer.sprite = defaultSprite;

    //        // Rigidbody初期化（静的に）
    //        if (rb != null)
    //            rb.isKinematic = true;

    //        // トリガー判定有効化（Colliderはぶつからず中に入るだけ）
    //        if (col != null)
    //            col.isTrigger = true;

    //        // カメラが未設定ならMainCameraを使用
    //        if (cameraTransform == null)
    //        {
    //            cameraTransform = Camera.main.transform;
    //        }
    //    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //        // すでに処理されている or プレイヤー以外なら無視
    //        if (alreadyHit) return;

    //        // カメラに追従するフラグを有効化
    //        followCamera = true; 

    //        if (other.CompareTag("Player"))
    //        {
    //            alreadyHit = true;

    //            // 当たり判定を無効化（多重反応を防ぐ）
    //            if (col != null)
    //                col.enabled = false;

    //            // コルーチンで演出スタート
    //            StartCoroutine(HandleHit());
    //        }
    //    }

    //    private void FixedUpdate()
    //    {
    //        if (followCamera)
    //        {
    //            // カメラの子に設定（ローカル位置を使いたいので false）
    //            transform.SetParent(cameraTransform, false);

    //            // カメラの前方向に forwardOffset 分だけ移動（ローカル空間）
    //            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forwardOffset);
    //        }
    //    }

    //    private System.Collections.IEnumerator HandleHit()
    //    {
    //        // ランダムに左上または右上へ飛ばす
    //        Vector3 direction = (Random.value < 0.5f) ? new Vector3(-10f, 1f, 0f) : new Vector3(10f, 1f, 0f);
    //        direction.Normalize(); // 念のため正規化

    //        if (rb != null)
    //        {
    //            rb.isKinematic = false; // 動かせるように
    //            rb.velocity = Vector3.zero; // 念のため初期化
    //            rb.AddForce(direction * launchForce, ForceMode.Impulse); // 力を加える
    //        }

    //        // 飛ばしたあと固定まで少し待機
    //        yield return new WaitForSeconds(stopAfterSeconds);

    //        if (rb != null)
    //        {
    //            rb.velocity = Vector3.zero;
    //            rb.isKinematic = true;
    //        }

    //        // 回転アニメーション（くるっと）
    //        float elapsed = 0f;
    //        Quaternion startRot = transform.rotation;
    //        Quaternion endRot = Quaternion.Euler(0, 180, 0) * startRot;

    //        while (elapsed < rotateDuration)
    //        {
    //            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotateDuration);
    //            elapsed += Time.deltaTime;
    //            yield return null;
    //        }

    //        transform.rotation = endRot;

    //        // スプライト差し替え
    //        if (hitSprite != null)
    //            spriteRenderer.sprite = hitSprite;

    //        // タグに応じたスコア加算
    //        ScoreManager.Instance?.AddScoreByTag(gameObject.tag);

    //        // 一定時間後に削除
    //        Destroy(gameObject, destroyDelay);

    //        // カメラ追従フラグを無効化
    //        followCamera = false;
    //    }
    //}
}
