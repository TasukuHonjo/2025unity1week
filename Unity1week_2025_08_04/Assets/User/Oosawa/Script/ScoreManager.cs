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
        public GameObject score_object = null; // Text�I�u�W�F�N�g
        public static ScoreManager instance; // �V���O���g���C���X�^���X
        public static int totalScore = 0; // ���X�R�A
        public static int totalPeople = 0;
        TextMeshProUGUI score_text;
        void Awake()
        {
            // �V���O���g���̃C���X�^���X��ݒ�
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject); // �V�[���J�ڎ��ɃI�u�W�F�N�g��j�����Ȃ�
            }
            else
            {
                Destroy(gameObject); // ���ɑ��݂���ꍇ�͐V�����C���X�^���X��j��
            }
        }

        void Start()
        {
            // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
            score_text = score_object.GetComponent<TextMeshProUGUI>();
            //�͂��߂̃X�e�[�W��������X�R�A����������
            if(SceneManager.GetActiveScene().name == "Stage1")
            {
                totalScore = 0;
                totalPeople = 0;
            }

        }

        void Update()
        {
            // �e�L�X�g�̕\�������ւ���

            if(score_text) score_text.text = "Score: " + totalScore.ToString("N0"); // ���l���J���}��؂�ŕ\��

#if UNITY_EDITOR
            Debug.Log("�X�R�A�F"+totalScore + "\n���v�l���F"+totalPeople);
#endif
        }

        // �X�R�A���Z
        public void AddScore(int amount)
        {
            totalScore += amount;
            ++totalPeople;
            Debug.Log("���݂̃X�R�A: " + totalScore);
        }
    }
}