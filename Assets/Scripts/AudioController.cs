using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public float volume = 0.5f; // 要调整的音量值，在Inspector面板中设置

    public Slider volumeSlider; // Slider组件，用于控制音量

    void Awake()
    {
        // 监听Slider组件的value改变事件
        
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = volume;
            OnVolumeChanged(volume);
        }
    }

    void OnVolumeChanged(float volume)
    {
        PlayerPrefs.DeleteKey("volume");
        PlayerPrefs.SetFloat("volume", volume);
        // 当Slider组件的值改变时，修改音频组件的音量
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.volume = volume;
        }
    }
}
