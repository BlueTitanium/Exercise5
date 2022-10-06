using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StartManager : MonoBehaviour
{
    public bool settingsOn = false;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public GameObject SettingsPage;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    // Start is called before the first frame update
    void Start()
    {
        LoadOptions();
        Time.timeScale = 1f;
        SettingsPage.SetActive(settingsOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
    public void LoadOptions()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            SaveOptions();
        }
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }
    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        sfxMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;

        UpdateMixerVolume();
        SaveOptions();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        sfxVolume = value;

        UpdateMixerVolume();
        SaveOptions();
    }

    public void ToggleSettings()
    {
        settingsOn = !settingsOn;
        SettingsPage.SetActive(settingsOn);
    }
}
