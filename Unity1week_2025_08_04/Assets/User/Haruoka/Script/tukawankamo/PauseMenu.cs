using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haruoka
{
    public class PauseMenu : MonoBehaviour
    {
        static bool createFlag = false;
        [SerializeField] GameObject backGround;
        [SerializeField] GameObject text;
        [SerializeField] GameObject returnButton;
        [SerializeField] GameObject RetryButton;
        [SerializeField] GameObject titleButton;

        void Start()
        {
            backGround.SetActive(false);
            text.SetActive(false);
            returnButton.SetActive(false);
            RetryButton.SetActive(false);
            titleButton.SetActive(false);
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                createFlag = true;
            }

            if(createFlag)
            {
                backGround.SetActive(true);
                text.SetActive(true);
                returnButton.SetActive(true);
                RetryButton.SetActive(true);
                titleButton.SetActive(true);
                Debug.Log("�|�[�Y���:�쐬");
            }
            else
            {
                backGround.SetActive(false);
                text.SetActive(false);
                returnButton.SetActive(false);
                RetryButton.SetActive(false);
                titleButton.SetActive(false);
            }
        }

        public static void Delete_PauseMenu()
        {
            createFlag = false;
            Debug.Log("�|�[�Y���:�폜");
        }
        public void Retry()
        {
            Delete_PauseMenu();
            ChangeScene.Load_GameScene();
        }
    }

}
