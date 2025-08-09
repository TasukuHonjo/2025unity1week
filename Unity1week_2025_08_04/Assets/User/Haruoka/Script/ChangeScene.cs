using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haruoka
{
    public class ChangeScene : MonoBehaviour
    {
        public static void Load_NameScene(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
            Debug.Log("SceneName:" + _sceneName);
        }

        // TitleScene�ǂݍ���
        public static void Load_TitleScene()
        {
            SceneManager.LoadScene("Title");
            Debug.Log("SceneName:Title");
        }

        public static void Load_PrologueScene()
        {
            SceneManager.LoadScene("Prologue");
            Debug.Log("SceneName:Prologue");
        }

        public static void Load_GameScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;

            // �V�[�����𒴂��Ȃ��悤�Ƀ`�F�b�N
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.Log("���̃V�[��������܂���I");
            }
        }

        public static void Load_ResultScene()
        {
            SceneManager.LoadScene("Result");
            Debug.Log("SceneName:Result");
        }

        //�Q�[�����I������
        public void GameEnd()
        {
#if UNITY_EDITOR//UnityEditor�ł̎��s�Ȃ�
            //�Đ����[�h����������
            UnityEditor.EditorApplication.isPlaying = false;
#else//UnityEditor�ł̎��s�ł͂Ȃ���΁i���r���h��j�Ȃ�
     //�A�v���P�[�V�������I������
     Application.Quit();
#endif
        }
    }}
