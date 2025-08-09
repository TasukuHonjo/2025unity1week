using Oosawa;
using unityroom.Api;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace Honjo
{
    /*
     * ランキングへ反映させる処理
     * スコアツイート関数
     */
    public class ResultManager : MonoBehaviour
    {
        ScoreManager scoreManager;
        int totalScore = ScoreManager.totalScore; // クラス名経由でアクセス
        int totalPeople = ScoreManager.totalPeople;
        private static int highScore = 0;

        void Start()
        {   
            //ヒエラルキーからスコアマネージャー取得
            scoreManager = GameObject.FindObjectOfType<ScoreManager>();
            RankingToHighScore();
        }

        private void RankingToHighScore()
        {
            //スコアを比較し、高かった場合はハイスコアを塗り替える
            if (totalScore >= highScore)
            {
                highScore = totalScore;
                //ランキングに反映させる処理
                // C#スクリプトの冒頭に `using unityroom.Api;` を追加してください。

                // ボードNo1にスコア123.45fを送信する。
                UnityroomApiClient.Instance.SendScore(1, highScore, ScoreboardWriteMode.Always);
            }
        }

        public void Tweet()
        {
            string text = $"私は【{totalPeople}】人の異世界へ転生させました。 \n#unity1week #転トラ";
            string url = "https://unityroom.com/games/tentora"; // 自分のゲームページや宣伝リンク
            string tweetUrl = $"https://twitter.com/intent/tweet?text={WebUtility.UrlEncode(text)}&url={WebUtility.UrlEncode(url)}";

            Application.OpenURL(tweetUrl);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Tweet();
            }
        }
    }
}

