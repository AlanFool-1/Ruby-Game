using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public float volume = 0.5f; // Ҫ����������ֵ����Inspector���������

    public Slider volumeSlider; // Slider��������ڿ�������

    void Awake()
    {
        // ����Slider�����value�ı��¼�
        
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
        // ��Slider�����ֵ�ı�ʱ���޸���Ƶ���������
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.volume = volume;
        }
    }
}
