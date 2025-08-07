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
            // �v���C���[�ƏՓ˂���������i�^�O�g�p�j
            if (other.CompareTag("Player") && isPlay)
            {
                isPlay = false; // ��x�����Đ����邽�߃t���O���I�t
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


