using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Oosawa
{
    public class ScoreManager : MonoBehaviour
    {
        public GameObject score_object = null; // Text�I�u�W�F�N�g
        public static ScoreManager instance; // �V���O���g���C���X�^���X
        private int totalScore = 0; // ���X�R�A

        void Awake()
        {
            // �V���O���g���̃C���X�^���X��ݒ�
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // �V�[���J�ڎ��ɃI�u�W�F�N�g��j�����Ȃ�
            }
            else
            {
                Destroy(gameObject); // ���ɑ��݂���ꍇ�͐V�����C���X�^���X��j��
            }
        }

        void Update()
        {
            // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
            TextMeshProUGUI score_text = score_object.GetComponent<TextMeshProUGUI>();
            // �e�L�X�g�̕\�������ւ���
            score_text.text = "Score: " + totalScore.ToString("N0"); // ���l���J���}��؂�ŕ\��
        }

        // �X�R�A���Z
        public void AddScore(int amount)
        {
            totalScore += amount;
            Debug.Log("���݂̃X�R�A: " + totalScore);
        }
    }
}
