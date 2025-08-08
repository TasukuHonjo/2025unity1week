using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haruoka
{
    public class TitleMenu : MonoBehaviour
    {
        static bool createFlag = false;
        [SerializeField] GameObject backGround;
        [SerializeField] GameObject text;
        [SerializeField] GameObject titleButton;
        [SerializeField] GameObject returnButton;

        void Start()
        {
            backGround.SetActive(false);
            text.SetActive(false);
            titleButton.SetActive(false);
            returnButton.SetActive(false);
        }
        
        void Update()
        {
            if(createFlag)
            {
                backGround.SetActive(true);
                text.SetActive(true);
                titleButton.SetActive(true);
                returnButton.SetActive(true);
            }
            else
            {
                backGround.SetActive(false);
                text.SetActive(false);
                titleButton.SetActive(false);
                returnButton.SetActive(false);
            }
        }

        public void Create_TitleMenu()
        {
            createFlag = true;
            Debug.Log("タイトル遷移確認画面:作成");
        }

        public void Delete_TitleMenu()
        {
            createFlag = false;
            Debug.Log("タイトル遷移確認画面:削除");
        }
        public void ToTitle()
        {
            Delete_TitleMenu();
            PauseMenu.Delete_PauseMenu();
            SceneManager.LoadScene("Title");
        }
    }
}