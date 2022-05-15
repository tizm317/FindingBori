using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public Slider slider2;
    public Image button;
    public Sprite volume;
    public Sprite mute;

    static bool onlyonce;

    void Awake()
    {
        if(!onlyonce)
        {
            // 초기화
            // static 이용해서 게임 처음 켜졌을 때 딱 1번 실행
            PlayerPrefs.SetFloat("BGM", 0.5f);
            PlayerPrefs.SetFloat("SFX", 0.5f);
            onlyonce = true;
        }
        slider.value = PlayerPrefs.GetFloat("BGM");
        slider2.value = PlayerPrefs.GetFloat("SFX");
        mixer.SetFloat("BGM", Mathf.Log10(slider.value) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(slider2.value) * 20);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGM", sliderValue);
        if(sliderValue <= 0.01f)
        {
            button.sprite = mute;
        }
        else
        {
            button.sprite = volume;
        }
    }
    public void SetLevel2(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX", sliderValue);
        if(sliderValue <= 0.01f)
        {
            button.sprite = mute;
        }
        else
        {
            button.sprite = volume;
        }
    }
}
