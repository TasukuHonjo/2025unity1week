using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oosawa
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance; // �V���O���g��

        private int totalScore = 0; // �݌v�X�R�A

        // �^�O�ɑΉ�����X�R�A�\
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
            // �V���O���g���̏�����
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��j������Ȃ��悤�ɂ���
        }

        // �^�O�ɉ����ăX�R�A�����Z����֐�
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

        // ���݂̃X�R�A���擾����֐��i�O���Q�Ɨp�j
        public int GetTotalScore() => totalScore;
    }
}
