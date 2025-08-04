using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class BillBoard : MonoBehaviour
    {
        void Update()
        {
            Vector3 p = Camera.main.transform.position;
            p.y = transform.position.y;
            transform.LookAt(p);
        }
    }
}

