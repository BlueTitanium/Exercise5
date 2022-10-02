using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("DeathScreen")]
    public GameObject deathScreen;
    [Header("PauseScreen")]
    public GameObject pauseMenu;
    public GameObject overView;
    public GameObject caseFiles;
    public Image[] casePics;
    public TextMeshProUGUI[] caseTitles;
    public TextMeshProUGUI[] caseDescriptions;
    [Header("Options")]
    public GameObject options;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    public GameObject leaveScreen;
    [Header("ItemObtainedScreen")]
    public GameObject itemObtained;
    public GameObject PictureBackground;
    public Image itemPicture;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemDescription;
    // Start is called before the first frame update
    void Start()
    {
        LoadOptions();
        Time.timeScale = 1f;
        deathScreen.SetActive(false);
        itemObtained.SetActive(false);
        pauseMenu.SetActive(false);
        overView.SetActive(false);
        caseFiles.SetActive(false);
        options.SetActive(false);
        leaveScreen.SetActive(false);
        fillCaseEvidence();
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
    public void GoToMainMenu()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            fillCaseEvidence();
            if (itemObtained.activeInHierarchy)
            {
                CloseItemObtained();
            } else 
            if (pauseMenu.activeInHierarchy)
            {
                ClosePauseMenu();
                
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }
    public void fillCaseEvidence()
    {
        for(int i = 0; i < casePics.Length; i++)
        {
            if (Inventory.collected[i])
            {
                casePics[i].sprite = Inventory.pictures[i];
                caseTitles[i].text = Inventory.titles[i];
                caseDescriptions[i].text = Inventory.descriptions[i];
            }
        }
    }
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    public void ShowItemObtained(int itemID)
    {
        fillCaseEvidence();
        Time.timeScale = 0f;
        PictureBackground.transform.eulerAngles = new Vector3(0, 0, Random.Range(-10f, 10f));
        if (Inventory.collected[itemID])
        {
            itemPicture.sprite = Inventory.pictures[itemID];
            itemTitle.text = Inventory.titles[itemID];
            itemDescription.text = Inventory.descriptions[itemID];
        } else
        {
            itemPicture.sprite = null;
            itemTitle.text = "???";
            itemDescription.text = "I have to find this somewhere...";
        }
        
        itemObtained.SetActive(true);
    }
    public void CloseItemObtained()
    {
        itemObtained.SetActive(false);
        if(!pauseMenu.activeInHierarchy)
            Time.timeScale = 1f;
    }
    public void ShowPauseMenu()
    {
        Time.timeScale = 0f;
        overView.SetActive(true);
        pauseMenu.SetActive(true);
        leaveScreen.SetActive(false);
        options.SetActive(false);
        caseFiles.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        overView.SetActive(true);
        caseFiles.SetActive(false);
        options.SetActive(false);
        leaveScreen.SetActive(false);
    }

    public void ShowCaseFiles()
    {
        overView.SetActive(false);
        caseFiles.SetActive(true);
        options.SetActive(false);
        leaveScreen.SetActive(false);
    }
    public void CloseCaseFiles()
    {
        overView.SetActive(true);
        caseFiles.SetActive(false);
        options.SetActive(false);
        leaveScreen.SetActive(false);
    }

    public void ShowSettings()
    {
        options.SetActive(true);
        caseFiles.SetActive(false);
        overView.SetActive(false);
        leaveScreen.SetActive(false);
    }

    public void CloseSettings()
    {
        overView.SetActive(true);
        caseFiles.SetActive(false);
        options.SetActive(false);
        leaveScreen.SetActive(false);
    }

    public void ShowLeaveScreen()
    {
        options.SetActive(false);
        caseFiles.SetActive(false);
        overView.SetActive(false);
        leaveScreen.SetActive(true);
    }

    public void CloseLeaveScreen()
    {
        overView.SetActive(true);
        caseFiles.SetActive(false);
        options.SetActive(false);
        leaveScreen.SetActive(false);
    }

    public void RestartFromCheckPoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
