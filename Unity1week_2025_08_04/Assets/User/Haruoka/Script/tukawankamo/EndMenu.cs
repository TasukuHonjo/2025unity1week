using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haruoka
{
    public class EndMenu : MonoBehaviour
    {
        static bool createFlag = false;

        [SerializeField] GameObject bg;
        [SerializeField] GameObject text;
        [SerializeField] GameObject endButton;
        [SerializeField] GameObject returnButton;

        void Start()
        {
            bg.SetActive(false);
            text.SetActive(false);
            endButton.SetActive(false);
            returnButton.SetActive(false);
        }

        void Update()
        {
            if (createFlag)
            {
                bg.SetActive(true);
                text.SetActive(true);
                endButton.SetActive(true);
                returnButton.SetActive(true);
            }
            else
            {
                bg.SetActive(false);
                text.SetActive(false);
                endButton.SetActive(false);
                returnButton.SetActive(false);
            }
        }

        public void Create_EndMenu()
        {
            createFlag = true;
            Debug.Log("�Q�[���I�����:�쐬");
        }

        public void Delete_EndMenu()
        {
            createFlag = false;
            Debug.Log("�Q�[���I�����:�폜");
        }
    }
}