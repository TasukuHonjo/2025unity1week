using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Oosawa.ScoreManager;

namespace Oosawa
{
    public class ScoreAdder : MonoBehaviour
    {
        [Header("NPCのスコアの値")]
        public int salaryman = 10;        // サラリーマン
        public int officelady = 100;      // OL
        public int grandmother = 1000;    // おばあちゃん
        public int schoolgirl = 10000;    // 女子学生
        public int schoolboy = 100000;    // 男子学生

        private bool isScoreAdded = false; // スコアが加算されたかどうかのフラグ

        private void OnTriggerEnter(Collider other)
        {
            // プレイヤーと衝突したか判定（タグ使用）
            if (other.CompareTag("Player") && isScoreAdded == false)
            {
                isScoreAdded = true; // スコアが加算されたフラグを立てる

                // 衝突したオブジェクトのタグに応じてスコアを加算
                switch (gameObject.tag)
                {
                    case "Salaryman":
                        ScoreManager.instance.AddScore(salaryman, PeopleTag.Salaryman);
                        break;
                    case "Office lady":
                        ScoreManager.instance.AddScore(officelady, PeopleTag.Officelady);
                        break;
                    case "Grandmother":
                        ScoreManager.instance.AddScore(grandmother, PeopleTag.Grandmother);
                        break;
                    case "Schoolgirl":
                        ScoreManager.instance.AddScore(schoolgirl, PeopleTag.Schoolgirl);
                        break;
                    case "Schoolboy":
                        ScoreManager.instance.AddScore(schoolboy, PeopleTag.Schoolboy);
                        break;
                    default:
                        Debug.LogWarning("Unknown tag: " + gameObject.tag);
                        break;
                }
            }
        }
    }
}