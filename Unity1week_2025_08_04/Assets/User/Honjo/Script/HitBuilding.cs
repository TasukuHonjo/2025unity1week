using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Honjo
{
    [RequireComponent(typeof(AudioSource))]
    public class HitBuilding : MonoBehaviour
    {
        [SerializeField] AudioClip[] se_hit = new AudioClip[4];
        AudioSource m_as;
        private void Awake()
        {
            m_as = GetComponent<AudioSource>();
            m_as.playOnAwake = false;
            m_as.loop = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                int r = Random.Range(0, se_hit.Length);
                m_as.clip = se_hit[r];
                m_as.Play();
            }
        }
    }
}

