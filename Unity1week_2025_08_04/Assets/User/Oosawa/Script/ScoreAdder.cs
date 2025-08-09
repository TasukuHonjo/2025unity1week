using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Oosawa.ScoreManager;

namespace Oosawa
{
    public class ScoreAdder : MonoBehaviour
    {
        [Header("NPC�̃X�R�A�̒l")]
        public int salaryman = 10;        // �T�����[�}��
        public int officelady = 100;      // OL
        public int grandmother = 1000;    // ���΂������
        public int schoolgirl = 10000;    // ���q�w��
        public int schoolboy = 100000;    // �j�q�w��

        private bool isScoreAdded = false; // �X�R�A�����Z���ꂽ���ǂ����̃t���O

        private void OnTriggerEnter(Collider other)
        {
            // �v���C���[�ƏՓ˂���������i�^�O�g�p�j
            if (other.CompareTag("Player") && isScoreAdded == false)
            {
                isScoreAdded = true; // �X�R�A�����Z���ꂽ�t���O�𗧂Ă�

                // �Փ˂����I�u�W�F�N�g�̃^�O�ɉ����ăX�R�A�����Z
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