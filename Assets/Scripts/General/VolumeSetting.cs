using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            //SetMusicVolume();
            //SetSFXVolume();

            myMixer.SetFloat("Music", Mathf.Log10(1) * 20);
            myMixer.SetFloat("SFX", Mathf.Log10(1) * 20);

            if (musicSlider != null && SFXSlider != null)
            {
                musicSlider.value = 1;
                SFXSlider.value = 1;
            }
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        if (musicSlider != null && SFXSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        //SetMusicVolume();
        //SetSFXVolume();

        myMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        myMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
    }
}
