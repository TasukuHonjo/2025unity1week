using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance; // シングルトン

        private int totalScore = 0; // 累計スコア

        // タグに対応するスコア表
        [SerializeField]
        private Dictionary<string, int> tagScoreTable = new Dictionary<string, int>()
    {
        { "Salaryman", 10 },
        { "Officelady", 100 },
        { "Schoolgirl", 1000 },
        { "Grandmother", 10000 }
    };

        private void Awake()
        {
            // シングルトンの初期化
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移しても破棄されないようにする
        }

        // タグに応じてスコアを加算する関数
        public void AddScoreByTag(string tag)
        {
            if (tagScoreTable.ContainsKey(tag))
            {
                totalScore += tagScoreTable[tag];
                Debug.Log($"Score Added! Current Score: {totalScore}");
            }
            else
            {
                Debug.LogWarning($"Tag '{tag}' not found in score table.");
            }
        }

        // 現在のスコアを取得する関数（外部参照用）
        public int GetTotalScore() => totalScore;
    }
}
