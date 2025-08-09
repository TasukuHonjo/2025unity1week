using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Honjo
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]Motor motor;
        private string currentSceneName;
        private string nextSceneName;

        private void Start()
        {
            motor = GameObject.Find("TruckPlayer").GetComponent<Motor>();
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        private void Update()
        {
            if (!motor) { return; }

            //運転が終了して次のシーンへ行く
            if (motor.driveEndFg == true)
            {
                switch (currentSceneName)
                {
                    case "Stage1":
                        nextSceneName = "Stage2";
                        break;
                    case "Stage2":
                        nextSceneName = "Stage3";
                        break;
                    case "Stage3":
                        nextSceneName = "Result";
                        break;
                    default:
                        Debug.Log("イレギュラーなシーン");
                        nextSceneName = "Result";
                        break;
                }


                //シーン切り替え
                ChangeScene(nextSceneName);
            }
        }

        public void ChangeScene(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

