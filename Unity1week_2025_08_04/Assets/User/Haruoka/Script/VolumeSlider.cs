using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SEVolumeSlider : MonoBehaviour
{
    public Slider seSlider;
    public AudioMixer audioMixer;
    public string exposedParam = "SEVolume";

    [Header("SE Feedback")]
    public AudioSource sePreviewSource;
    public AudioClip feedbackSE;  // 短めのクリック音など

    private float lastStep = -1f;

    private void Start()
    {
        // 初期化
        float savedVolume = PlayerPrefs.GetFloat(exposedParam, 0.5f);
        seSlider.value = savedVolume;
        ApplyVolume(savedVolume);

        seSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // 値を10ステップで区切って音を鳴らす（視覚的・聴覚的フィードバック）
        float step = Mathf.Round(value * 10f) / 10f;

        ApplyVolume(value);

        if (step != lastStep)
        {
            lastStep = step;

            // 音を確実に再生（PlayOneShot推奨）
            if (sePreviewSource != null && feedbackSE != null)
            {
                sePreviewSource.Stop(); // 保険
                sePreviewSource.PlayOneShot(feedbackSE);
            }
        }
    }

    private void ApplyVolume(float value)
    {
        float volumeDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(exposedParam, volumeDb);
        PlayerPrefs.SetFloat(exposedParam, value);
    }
}
