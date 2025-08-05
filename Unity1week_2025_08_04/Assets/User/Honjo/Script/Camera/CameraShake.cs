using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class CameraShake : MonoBehaviour
    {
        public float shakeDuration = 0.2f;   // �h��鎞��
        public float shakeMagnitude = 0.1f;  // �h��鋭��
        public AnimationCurve shakeCurve;    // �h��̌����J�[�u�i�C�Ӂj

        private Vector3 originalPos;
        private Coroutine shakeCoroutine;

        void Awake()
        {
            originalPos = transform.localPosition;
        }

        public void Shake()
        {
            Debug.Log("CameraChake.Shake()");
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                float strength = shakeMagnitude;

                if (shakeCurve != null)
                    strength *= shakeCurve.Evaluate(elapsed / shakeDuration);

                Vector3 randomPoint = originalPos + Random.insideUnitSphere * strength;
                transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, originalPos.z);

                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}