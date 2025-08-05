using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haruoka
{
    public class PrologueSystem : MonoBehaviour
    {
        int currentIndex = 0;   // �w�i�摜�J�E���g
        int endIndex = 0;       // �I���ԍ�

        [Header("�}�E�X�E���͎���")]
        [SerializeField] float holdTime = 2.0f;
        float currentTime = 0.0f;

        [Header("�w�i�摜�ɕK�v�ȃA�^�b�`�������")]
        [SerializeField] GameObject objBg;  // �w�i�摜�I�u�W�F�N�g
        SpriteRenderer bgSprite;
        [SerializeField] List<Sprite> backGroundSprites;

        [Header("�e�N�X�`���؂�ւ��o�^�ԍ�")]
        [Tooltip("�w�i�̔ԍ�������")]
        [SerializeField] List<int> changeTextureNum;

        void Start()
        {
            bgSprite = objBg.GetComponent<SpriteRenderer>();
            bgSprite.sprite = backGroundSprites[changeTextureNum[currentIndex]];
            AdjustBackgroundScale(bgSprite.sprite);
            Debug.Log("TextureName:" + backGroundSprites[changeTextureNum[currentIndex]]);

            endIndex = changeTextureNum.Count - 1;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // �Ō�̔ԍ��Ȃ�
                if (currentIndex == endIndex)
                {
                    // �J��
                    ChangeScene.Load_TitleScene();
                    return;
                }

                // �w�i�摜�̍X�V
                currentIndex++;
                bgSprite.sprite = backGroundSprites[changeTextureNum[currentIndex]];
                AdjustBackgroundScale(bgSprite.sprite);
                Debug.Log("TextureName:" + backGroundSprites[changeTextureNum[currentIndex]]);
            }
            // �E�N���b�N��������
            if (Input.GetMouseButton(1))
            {
                currentTime += Time.deltaTime;
                Debug.Log(currentTime);

                if (currentTime > holdTime)
                {
                    // �J��
                    ChangeScene.Load_TitleScene();
                }
            }
            else
            {
                // ���Z�b�g
                currentTime = 0.0f;
            }
        }

        void AdjustBackgroundScale(Sprite sprite)
        {
            if (sprite == null) return;

            float screenHeight = Camera.main.orthographicSize * 2f;
            float screenWidth = screenHeight * Camera.main.aspect;

            float spriteWidth = sprite.bounds.size.x;
            float spriteHeight = sprite.bounds.size.y;

            Vector3 scale = objBg.transform.localScale;
            scale.x = screenWidth / spriteWidth;
            scale.y = screenHeight / spriteHeight;
            objBg.transform.localScale = scale;
        }
    }

}