using Oosawa;
using unityroom.Api;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace Honjo
{
    /*
     * �����L���O�֔��f�����鏈��
     * �X�R�A�c�C�[�g�֐�
     */
    public class ResultManager : MonoBehaviour
    {
        ScoreManager scoreManager;
        int totalScore = ScoreManager.totalScore; // �N���X���o�R�ŃA�N�Z�X
        int totalPeople = ScoreManager.totalPeople;
        private static int highScore = 0;

        void Start()
        {   
            //�q�G�����L�[����X�R�A�}�l�[�W���[�擾
            scoreManager = GameObject.FindObjectOfType<ScoreManager>();
            RankingToHighScore();
        }

        private void RankingToHighScore()
        {
            //�X�R�A���r���A���������ꍇ�̓n�C�X�R�A��h��ւ���
            if (totalScore >= highScore)
            {
                highScore = totalScore;
                //�����L���O�ɔ��f�����鏈��
                // C#�X�N���v�g�̖`���� `using unityroom.Api;` ��ǉ����Ă��������B

                // �{�[�hNo1�ɃX�R�A123.45f�𑗐M����B
                UnityroomApiClient.Instance.SendScore(1, highScore, ScoreboardWriteMode.Always);
            }
        }

        public void Tweet()
        {
            string text = $"���́y{totalPeople}�z�l�ِ̈��E�֓]�������܂����B \n#unity1week #�]�g��";
            string url = "https://unityroom.com/games/tentora"; // �����̃Q�[���y�[�W���`�����N
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

