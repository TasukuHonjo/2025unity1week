using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Honjo
{
    public class Motor : MonoBehaviour
    {
        Rigidbody rb = null;

        [SerializeField]float maxSpeed = 10;//速度上限
        [SerializeField] float moveTime = 5;//何秒動くか
        float time = 0;

        [SerializeField] bool startFg = false;

        [SerializeField] float power = 5;//動かすパワー
        Transform cameraTransform = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            time = 0;
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            //速度上限
            if (rb.velocity.magnitude > maxSpeed)
            {
                // 現在の速度方向を保ったまま上限スピードに制限
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            if (!startFg)
            {
                if (cameraTransform == null) return;

                // カメラ→自分 のベクトルを取得
                Vector3 toCamera = cameraTransform.position - transform.position;

                // 反対方向を向く（背を向ける）
                Vector3 lookDirection = -toCamera.normalized;

                // Y軸はそのままで水平回転だけに限定（任意）
                lookDirection.y = 0f;

                if (lookDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                if(!startFg)startFg = true;
            }
        }

        private void FixedUpdate()
        {
            if (!startFg) return;
                time += Time.fixedDeltaTime;
            if (time > moveTime) 
            {
                time = 0;
                startFg = false;
                return;
            }
            // Debug用RayをSceneビューに表示
            Ray ray = new Ray(transform.position,transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            rb.AddForce(transform.forward * power, ForceMode.Force);
        }


        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bumper")) // Bに"TargetB"タグをつける
            {
                float randomY = Random.Range(0f, 360f);
                Vector3 currentEuler = transform.eulerAngles;
                transform.eulerAngles = new Vector3(currentEuler.x, randomY, currentEuler.z);
                rb.AddForce(transform.forward * 50, ForceMode.Impulse);
            }
        }
    }
}

