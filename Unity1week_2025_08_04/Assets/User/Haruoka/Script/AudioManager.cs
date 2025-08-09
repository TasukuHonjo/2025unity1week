using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Volume Keys")]
    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SE_VOLUME_KEY = "SEVolume";

    private void Awake()
    {
        // シングルトン設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 保存された音量を取得（デフォルトは50% = 0.5f）
        float savedBGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.5f);
        float savedSEVolume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, 0.5f);

        SetBGMVolume(savedBGMVolume);
        SetSEVolume(savedSEVolume);
    }

    // BGM音量設定（0.0001〜1.0）
    public void SetBGMVolume(float volume)
    {
        // -10dBのオフセットを追加して全体的に音量を下げる
        float volumeDb = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20 - 10f;
        audioMixer.SetFloat("BGMVolume", volumeDb);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
    }

    // SE音量設定（0.0001〜1.0）→ SEグループ全体にかかる
    public void SetSEVolume(float volume)
    {
        // -10dBのオフセットを追加して全体的に音量を下げる
        float volumeDb = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20 - 10f;
        audioMixer.SetFloat("SEVolume", volumeDb);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, volume);
    }
}