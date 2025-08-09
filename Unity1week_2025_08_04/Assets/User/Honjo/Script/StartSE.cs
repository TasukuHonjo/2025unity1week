using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class StartSE : MonoBehaviour
    {
        AudioSource m_as;
        [SerializeField]float fadeTime = 3.0f;
        float time = 0;
        private void Awake()
        {
            m_as = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!m_as) { return; }
            time += Time.deltaTime;

            if (fadeTime < time)
            {
                this.gameObject.SetActive(false);
            }

            float normalized = (fadeTime - time) / fadeTime;
            normalized = Mathf.Clamp01(normalized); // �O�̂��� 0?1 �ɐ���
            m_as.volume = normalized;
        }
    }
}

