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

        [SerializeField] Vector2 vec2 = new Vector2();//移動する方向
        [SerializeField] float power = 5;//動かすパワー

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

            rb.AddForce(transform.forward * power, ForceMode.Force);
        }
    }
}

