using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScroller : MonoBehaviour
{
    public enum AudioType
    {
        Music,
        SFX
    }
    public AudioType audioType;
    void Start()
    {
        ModifySlider();
    }
    void OnEnable()
    {
        ModifySlider();
    }

    private void ModifySlider()
    {
        if (audioType == AudioType.Music)
        {
            gameObject.GetComponent<Slider>().value = AudioManager.instance.GetMusicVolume();
        }
        if (audioType == AudioType.SFX)
        {
            gameObject.GetComponent<Slider>().value = AudioManager.instance.GetSfxVolume();
        }
    }

    private void Update()
    {
        if (audioType == AudioType.Music)
        {
            VolumeController.i.musicVolume = gameObject.GetComponent<Slider>().value;
        }
        if (audioType == AudioType.SFX)
        {
            VolumeController.i.SfxVolume = gameObject.GetComponent<Slider>().value;
        }
    }
}
