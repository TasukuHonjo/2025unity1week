using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class Human : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SystemMethod.instance.HitStop(0.15f);
                SystemMethod.instance.Shake();
            }
            
        }
    }
}

