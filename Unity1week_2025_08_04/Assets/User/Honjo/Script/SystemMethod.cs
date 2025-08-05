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

        public void HitStop(float duration, float slowTimeScale = 0f)
        {
            StartCoroutine(pHitStop(duration,slowTimeScale));
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

