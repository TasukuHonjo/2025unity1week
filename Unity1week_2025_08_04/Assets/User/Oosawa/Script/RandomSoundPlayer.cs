using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class RandomSoundPlayer : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] audioClips;

        private bool isPlay = true;

        void OnTriggerEnter(Collider other)
        {
            // プレイヤーと衝突したか判定（タグ使用）
            if (other.CompareTag("Player") && isPlay)
            {
                isPlay = false; // 一度だけ再生するためフラグをオフ
                PlayRandomSound();
            }
        }

        public void PlayRandomSound()
        {
            if (audioClips.Length == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}


