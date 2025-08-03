using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class LookAt : MonoBehaviour
    {
        public GameObject targetObj;
        void Start()
        {
            if(targetObj == null)
            {
                Debug.LogError(this.transform.name + ".LookAt�R���|�[�l���g��TargetObj��Null�ł�");
            }
        }

        void Update()
        {
            if (targetObj != null)
            {
                transform.LookAt(targetObj.transform);
            }
        }
    }
}
