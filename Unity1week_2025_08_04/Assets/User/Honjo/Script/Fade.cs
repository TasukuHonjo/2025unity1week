using Haruoka;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Honjo
{
    public class Fade : MonoBehaviour
    {
        Motor motor;
        [SerializeField]float fadeTime = 0.5f;
        Image image;

        [SerializeField]List<GameObject> gameObjects = new List<GameObject>();
        void Start()
        {
            image = GetComponent<Image>();
            motor = GameObject.Find("TruckPlayer").GetComponent<Motor>();
            StartCoroutine(FadeOut());
            
        }


        void Update()
        {

        }

        public void FadeInF()
        {
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Clamp01(t / fadeTime);
                SetFadeAlpha(alpha);
                yield return null;
            }
            SetFadeAlpha(1f);
            GameObjectsActive(false);

        }

        public void SetFadeAlpha(float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }

        IEnumerator FadeOut()
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Clamp01(t / fadeTime);
                SetFadeAlpha(1-alpha);
                yield return null;
            }
            SetFadeAlpha(0f);
            GameObjectsActive(true);
            motor.OpeningPerformanceFg();
        }

        public void GameObjectsActive(bool _active)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.SetActive(_active);
            }
        }
    }

    
}
