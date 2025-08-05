using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Honjo
{
    public class SystemMethod : MonoBehaviour
    {

        public static SystemMethod instance; // �C���X�^���X�̒�`

        CameraShake driveCameraChaker = null;
        private void Awake()
        {
            // �V���O���g���̎���
            if (instance == null)
            {
                // ���g���C���X�^���X�Ƃ���
                instance = this;
            }
            else
            {
                // �C���X�^���X���������݂��Ȃ��悤�ɁA���ɑ��݂��Ă����玩�g����������
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
            yield return new WaitForSecondsRealtime(duration); // Realtime�Ȃ̂ŉe���󂯂Ȃ�
            Time.timeScale = 1f;
        }

        public void Shake()
        {
            driveCameraChaker.Shake();
        }
    }
}

