using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Oosawa
{
    public class ScoreManager : MonoBehaviour
    {
        public GameObject score_object = null; // Textオブジェクト
        public static ScoreManager instance; // シングルトンインスタンス
        public static int totalScore = 0; // 総スコア
        public static int totalPeople = 0;
        TextMeshProUGUI score_text;
        void Awake()
        {
            // シングルトンのインスタンスを設定
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject); // シーン遷移時にオブジェクトを破棄しない
            }
            else
            {
                Destroy(gameObject); // 既に存在する場合は新しいインスタンスを破棄
            }
        }

        void Start()
        {
            // オブジェクトからTextコンポーネントを取得
            score_text = score_object.GetComponent<TextMeshProUGUI>();
            //はじめのステージだったらスコア初期化する
            if(SceneManager.GetActiveScene().name == "Stage1")
            {
                totalScore = 0;
                totalPeople = 0;
            }

        }

        void Update()
        {
            // テキストの表示を入れ替える

            if(score_text) score_text.text = "Score: " + totalScore.ToString("N0"); // 数値をカンマ区切りで表示

#if UNITY_EDITOR
            Debug.Log("スコア："+totalScore + "\n合計人数："+totalPeople);
#endif
        }

        // スコア加算
        public void AddScore(int amount)
        {
            totalScore += amount;
            ++totalPeople;
            Debug.Log("現在のスコア: " + totalScore);
        }
    }
}