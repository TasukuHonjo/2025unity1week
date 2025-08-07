using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Honjo
{
    public class SystemMethod : MonoBehaviour
    {

        public static SystemMethod instance; // インスタンスの定義

        [SerializeField] float hitStopCoolTime = 3f;
        bool hitStopFg = false;
        bool hitStopActionFg = false;
        float m_duration, m_slowTimeScale = 0f;
        float time = 0;

        CameraShake driveCameraChaker = null;
        private void Awake()
        {
            // シングルトンの呪文
            if (instance == null)
            {
                // 自身をインスタンスとする
                instance = this;
            }
            else
            {
                // インスタンスが複数存在しないように、既に存在していたら自身を消去する
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            driveCameraChaker = GameObject.Find("DriveCamera").GetComponent<CameraShake>();
        }

        private void Update()
        {
            //ヒットストップが実行されたら
            if (hitStopActionFg)
            {
                time += Time.deltaTime;
                if(time > hitStopCoolTime)
                {
                    time = 0;
                    hitStopActionFg = false;
                }
            }
            if (hitStopActionFg) { return; }
            if (hitStopFg)
            {
                hitStopActionFg = true;
                hitStopFg = false;
                StartCoroutine(pHitStop(m_duration, m_slowTimeScale));
            }
        }

        public void HitStop(float duration, float slowTimeScale = 0f)
        {
            m_duration = duration;
            m_slowTimeScale = slowTimeScale;
            hitStopFg = true;
            //StartCoroutine(pHitStop(duration,slowTimeScale));
        }

        IEnumerator pHitStop(float duration, float slowTimeScale = 0f)
        {
            Time.timeScale = slowTimeScale;
            yield return new WaitForSecondsRealtime(duration); // Realtimeなので影響受けない
            Time.timeScale = 1f;
        }

        public void Shake()
        {
            driveCameraChaker.Shake();
        }
    }
}

