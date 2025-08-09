using System;
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
        Fade f;

        [SerializeField]float fadeTime = 1;
        float defTime = 1;
        bool ones = false;

        private void Start()
        {
            motor = GameObject.Find("TruckPlayer").GetComponent<Motor>();
            currentSceneName = SceneManager.GetActiveScene().name;
            f = GameObject.Find("FadePanel").GetComponent<Fade>();
            defTime = fadeTime;
        }

        private void Update()
        {
            if (!motor) { return; }

            //運転が終了して次のシーンへ行く
            if (motor.driveEndFg == true)
            {
                if(ones == false)
                {
                    ones = true;
                    f.GameObjectsActive(false);
                }


                fadeTime -= Time.deltaTime;
                if(fadeTime < 0) 
                { 
                    fadeTime = 0;

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
                float normalized = fadeTime / defTime;
                normalized = Mathf.Clamp01(normalized); // 念のため 0?1 に制限
                f.SetFadeAlpha(1-normalized);

                
            }
        }

        public void ChangeScene(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

