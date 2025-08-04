using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class Bumper : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bumper")) // B‚É"TargetB"ƒ^ƒO‚ð‚Â‚¯‚é
            {
                Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
                float randomY = Random.Range(0f, 360f);
                Vector3 currentEuler = transform.eulerAngles;
                transform.eulerAngles = new Vector3(currentEuler.x, randomY, currentEuler.z);
                rigidbody.AddForce(transform.forward * 150, ForceMode.Impulse);
            }
        }
    }
}

