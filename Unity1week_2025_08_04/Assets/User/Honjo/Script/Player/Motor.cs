using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Honjo
{
    public class Motor : MonoBehaviour
    {
        Rigidbody rb = null;

        [SerializeField] float maxSpeed = 10;//速度上限
        [SerializeField] float moveTime = 5;//何秒動くか
        float time = 0;

        [SerializeField] bool startFg = false;

        [SerializeField] float power = 5;//動かすパワー

        [SerializeField]Camera topViewCamera = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            time = 0;
        }

        private void Update()
        {
            //速度上限
            if (rb.velocity.magnitude > maxSpeed)
            {
                // 現在の速度方向を保ったまま上限スピードに制限
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }


            if (Input.GetMouseButtonDown(0))
            {
                DriveFg();
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

        public void DriveFg()
        {
            if (!startFg) startFg = true;
            topViewCamera.depth = -2;
        }
    }
}

