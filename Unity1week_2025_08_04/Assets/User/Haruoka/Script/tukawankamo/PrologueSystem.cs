using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haruoka
{
    public class PrologueSystem : MonoBehaviour
    {
        int currentIndex = 0;   // 背景画像カウント
        int endIndex = 0;       // 終了番号

        [Header("マウス右入力時間")]
        [SerializeField] float holdTime = 2.0f;
        float currentTime = 0.0f;

        [Header("背景画像に必要なアタッチするもの")]
        [SerializeField] GameObject objBg;  // 背景画像オブジェクト
        SpriteRenderer bgSprite;
        [SerializeField] List<Sprite> backGroundSprites;

        [Header("テクスチャ切り替え登録番号")]
        [Tooltip("背景の番号を入れる")]
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
                // 最後の番号なら
                if (currentIndex == endIndex)
                {
                    // 遷移
                    ChangeScene.Load_TitleScene();
                    return;
                }

                // 背景画像の更新
                currentIndex++;
                bgSprite.sprite = backGroundSprites[changeTextureNum[currentIndex]];
                AdjustBackgroundScale(bgSprite.sprite);
                Debug.Log("TextureName:" + backGroundSprites[changeTextureNum[currentIndex]]);
            }
            // 右クリック長押しで
            if (Input.GetMouseButton(1))
            {
                currentTime += Time.deltaTime;
                Debug.Log(currentTime);

                if (currentTime > holdTime)
                {
                    // 遷移
                    ChangeScene.Load_TitleScene();
                }
            }
            else
            {
                // リセット
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