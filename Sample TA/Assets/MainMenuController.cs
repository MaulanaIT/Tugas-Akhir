using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public static MainMenuController 
        mainMenuController;

    [System.Serializable]
    public class Setting {
        public GameObject
            panelSetting,
            panelGuide, 
            buttonGuide;

        public Slider
            cameraSensitivity,
            soundVolume,
            musicVolume;
    }
    public Setting setting = new Setting();

    [System.Serializable]
    public class MainMenu {
        public GameObject 
            panelSelectMenu, 
            panelCreateName;

        public AudioSource 
            audioButton;

        public AudioClip 
            audioButtonClick, 
            audioButtonSelect;

        public InputField
            inputName;

        public Text 
            textButton;

        public string[]
            stringButtonIndonesia;

        public int 
            indexButton;
    }
    public MainMenu mainMenu = new MainMenu();

    private void Awake() {
        if (mainMenuController == null) {
            mainMenuController = this;
        } else if (mainMenuController != this) {
            Destroy(gameObject);
        }
    }
    void Start() {

    }

    void Update() {
        mainMenu.textButton.text = mainMenu.stringButtonIndonesia[mainMenu.indexButton];
    }

    public void ButtonSelectRightFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonClick;
        mainMenu.audioButton.Play();

        mainMenu.indexButton++;

        if (PlayerPrefs.GetInt("SaveGame") == 0) {
            if (mainMenu.indexButton == 1) {
                mainMenu.indexButton = 2;
            }
        }

        if (mainMenu.indexButton == mainMenu.stringButtonIndonesia.Length) {
            mainMenu.indexButton = 0;
        }
    }

    public void ButtonSelectLeftFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonClick;
        mainMenu.audioButton.Play();

        mainMenu.indexButton--;

        if (PlayerPrefs.GetInt("SaveGame") == 0) {
            if (mainMenu.indexButton == 1) {
                mainMenu.indexButton = 0;
            }
        }

        if (mainMenu.indexButton == -1) {
            mainMenu.indexButton = mainMenu.stringButtonIndonesia.Length - 1;
        }
    }

    public void ButtonPlayFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonSelect;
        mainMenu.audioButton.Play();

        PlayerPrefs.SetString("PlayerName", mainMenu.inputName.text);

        PlayerPrefs.SetInt("TextureQuality", QualitySettings.masterTextureLimit);
        PlayerPrefs.SetInt("AntiAliasing", QualitySettings.antiAliasing);

        if (QualitySettings.shadows == ShadowQuality.Disable) {
            PlayerPrefs.SetInt("ShadowQuality", 0);
        } else if (QualitySettings.shadows == ShadowQuality.HardOnly) {
            PlayerPrefs.SetInt("ShadowQuality", 1);
        } else if (QualitySettings.shadows == ShadowQuality.All) {
            PlayerPrefs.SetInt("ShadowQuality", 2);
        }

        if (QualitySettings.shadowResolution == ShadowResolution.Low) {
            PlayerPrefs.SetInt("ShadowResolution", 0);
        } else if (QualitySettings.shadowResolution == ShadowResolution.Medium) {
            PlayerPrefs.SetInt("ShadowResolution", 1);
        } else if (QualitySettings.shadowResolution == ShadowResolution.High) {
            PlayerPrefs.SetInt("ShadowResolution", 2);
        } else if (QualitySettings.shadowResolution == ShadowResolution.VeryHigh) {
            PlayerPrefs.SetInt("ShadowResolution", 3);
        }

        PlayerPrefs.SetInt("IndexQuality", SettingController.settingController.quality.indexQuality);
        PlayerPrefs.SetInt("CameraSensitivity", Convert.ToInt32(SettingController.settingController.cameraSensitivity.cameraSensitivity.value));
        PlayerPrefs.SetInt("SoundVolume", Convert.ToInt32(SettingController.settingController.audioVolume.soundVolume.value));
        PlayerPrefs.SetInt("MusicVolume", Convert.ToInt32(SettingController.settingController.audioVolume.musicVolume.value));

        LoadingController.loadingController.ButtonLoadSceneFunction(1);
    }

    public void ButtonGameFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonSelect;
        mainMenu.audioButton.Play();

        if (mainMenu.indexButton == 0) {
            mainMenu.panelSelectMenu.SetActive(false);
            mainMenu.panelCreateName.SetActive(true);
            setting.buttonGuide.SetActive(false);

            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("SaveGame", 0);
        } else if (mainMenu.indexButton == 1) {
            if (PlayerPrefs.GetInt("SaveGame") == 1) {
                SceneManager.LoadScene(2);
            }
        } else if (mainMenu.indexButton == 2) {
            setting.panelSetting.SetActive(true);
        } else if (mainMenu.indexButton == 3) {
            Application.Quit();
        }
    }

    public void ButtonCloseSettingFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonSelect;
        mainMenu.audioButton.Play();

        PlayerPrefs.SetInt("CameraSensitivity", Convert.ToInt32(setting.cameraSensitivity.value));
        PlayerPrefs.SetInt("SoundVolume", Convert.ToInt32(setting.soundVolume.value));
        PlayerPrefs.SetInt("MusicVolume", Convert.ToInt32(setting.musicVolume.value));

        setting.panelSetting.SetActive(false);
    }

    public void ButtonOpenGuideFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonSelect;
        mainMenu.audioButton.Play();

        setting.panelGuide.SetActive(true);
    }

    public void ButtonCloseGuideFunction() {
        mainMenu.audioButton.Stop();
        mainMenu.audioButton.clip = mainMenu.audioButtonSelect;
        mainMenu.audioButton.Play();

        setting.panelGuide.SetActive(false);
    }
}
