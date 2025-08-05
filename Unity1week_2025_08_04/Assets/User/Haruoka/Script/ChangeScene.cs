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

        // TitleScene読み込み
        public static void Load_TitleScene()
        {
            SceneManager.LoadScene("Title");
            Debug.Log("SceneName:Title");
        }

        public static void Load_ProlorueScene()
        {
            SceneManager.LoadScene("Prologue");
            Debug.Log("SceneName:Prologue");
        }

        public static void Load_GameScene()
        {
            SceneManager.LoadScene("Game");
            Debug.Log("SceneName:Game");
        }

        public static void Load_ResultScene()
        {
            SceneManager.LoadScene("Result");
            Debug.Log("SceneName:Result");
        }

        //ゲームを終了する
        public void GameEnd()
        {
#if UNITY_EDITOR//UnityEditorでの実行なら
            //再生モードを解除する
            UnityEditor.EditorApplication.isPlaying = false;
#else//UnityEditorでの実行ではなければ（→ビルド後）なら
     //アプリケーションを終了する
     Application.Quit();
#endif
        }
    }}
