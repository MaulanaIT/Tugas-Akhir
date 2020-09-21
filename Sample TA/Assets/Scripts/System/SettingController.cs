using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour {

    public static SettingController 
        settingController;

    public Sprite 
        spriteButtonSelected, 
        spriteButtonUnselected;

    [System.Serializable]
    public class Quality {
        public GameObject
            lowerQuality,
            higherQuality;

        public Text 
            textQuality;

        public string[]
            stringQuality;

        public int
            indexQuality;
    }
    public Quality quality = new Quality();

    [System.Serializable]
    public class TextureQuality {
        public GameObject 
            lowQuality, 
            mediumQuality, 
            highQuality;
    }
    public TextureQuality textureQuality = new TextureQuality();

    [System.Serializable]
    public class AntiAliasing {
        public GameObject
            offAntiAliasing,
            x2AntiAliasing,
            x4AntiAliasing, 
            x8AntiAliasing;
    }
    public AntiAliasing antiAliasing = new AntiAliasing();

    [System.Serializable]
    public class ShadowMode {
        public GameObject
            disableShadow,
            hardShadow,
            allShadow;
    }
    public ShadowMode shadowMode = new ShadowMode();

    [System.Serializable]
    public class ShadowScale {
        public GameObject
            lowResolution,
            mediumResolution,
            highResolution,
            veryHighResolution;
    }
    public ShadowScale shadowScale = new ShadowScale();

    [System.Serializable]
    public class CameraSensitivity {
        public Text
            textCameraSensitivity;

        public Slider
            cameraSensitivity;
    }
    public CameraSensitivity cameraSensitivity = new CameraSensitivity();

    [System.Serializable]
    public class AudioVolume {
        public Text
            textSoundVolume,
            textMusicVolume;

        public Slider
            soundVolume,
            musicVolume;
    }
    public AudioVolume audioVolume = new AudioVolume();

    public bool 
        isMainMenu, 
        isMain;

    private void Awake() {
        if (settingController == null) {
            settingController = this;
        } else if (settingController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

    }

    void Update() {
        QualityFunction();
        TextureQualityFunction();
        AntiAliasingFunction();
        ShadowQualityFunction();
        ShadowResolutionFunction();

        cameraSensitivity.textCameraSensitivity.text = "" + Convert.ToInt32(cameraSensitivity.cameraSensitivity.value);
        audioVolume.textSoundVolume.text = "" + Convert.ToInt32(audioVolume.soundVolume.value);
        audioVolume.textMusicVolume.text = "" + Convert.ToInt32(audioVolume.musicVolume.value);
    }

    public void QualityFunction() {
        quality.textQuality.text = quality.stringQuality[quality.indexQuality];

        if (quality.indexQuality <= 0) {
            quality.lowerQuality.SetActive(false);
        } else {
            quality.lowerQuality.SetActive(true);
        }

        if (quality.indexQuality >= 4) {
            quality.higherQuality.SetActive(false);
        } else {
            quality.higherQuality.SetActive(true);
        }
    }

    public void TextureQualityFunction() {
        if (QualitySettings.masterTextureLimit == 2) {
            ButtonSelectedFunction(textureQuality.lowQuality);
            ButtonUnselectedFunction(textureQuality.mediumQuality);
            ButtonUnselectedFunction(textureQuality.highQuality);
        } else if (QualitySettings.masterTextureLimit == 1) {
            ButtonUnselectedFunction(textureQuality.lowQuality);
            ButtonSelectedFunction(textureQuality.mediumQuality);
            ButtonUnselectedFunction(textureQuality.highQuality);
        } else if (QualitySettings.masterTextureLimit == 0) {
            ButtonUnselectedFunction(textureQuality.lowQuality);
            ButtonUnselectedFunction(textureQuality.mediumQuality);
            ButtonSelectedFunction(textureQuality.highQuality);
        }
    }

    public void AntiAliasingFunction() {
        if (QualitySettings.antiAliasing == 0) {
            ButtonSelectedFunction(antiAliasing.offAntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);
        } else if (QualitySettings.antiAliasing == 2) {
            ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
            ButtonSelectedFunction(antiAliasing.x2AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);
        } else if (QualitySettings.antiAliasing == 4) {
            ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
            ButtonSelectedFunction(antiAliasing.x4AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);
        } else if (QualitySettings.antiAliasing == 8) {
            ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
            ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
            ButtonSelectedFunction(antiAliasing.x8AntiAliasing);
        }
    }

    public void ShadowQualityFunction() {
        if (QualitySettings.shadows == ShadowQuality.Disable) {
            ButtonSelectedFunction(shadowMode.disableShadow);
            ButtonUnselectedFunction(shadowMode.hardShadow);
            ButtonUnselectedFunction(shadowMode.allShadow);
        } else if (QualitySettings.shadows == ShadowQuality.HardOnly) {
            ButtonUnselectedFunction(shadowMode.disableShadow);
            ButtonSelectedFunction(shadowMode.hardShadow);
            ButtonUnselectedFunction(shadowMode.allShadow);
        } else if (QualitySettings.shadows == ShadowQuality.All) {
            ButtonUnselectedFunction(shadowMode.disableShadow);
            ButtonUnselectedFunction(shadowMode.hardShadow);
            ButtonSelectedFunction(shadowMode.allShadow);
        }
    }

    public void ShadowResolutionFunction() {
        if (QualitySettings.shadowResolution == ShadowResolution.Low) {
            ButtonSelectedFunction(shadowScale.lowResolution);
            ButtonUnselectedFunction(shadowScale.mediumResolution);
            ButtonUnselectedFunction(shadowScale.highResolution);
            ButtonUnselectedFunction(shadowScale.veryHighResolution);
        } else if (QualitySettings.shadowResolution == ShadowResolution.Medium) {
            ButtonUnselectedFunction(shadowScale.lowResolution);
            ButtonSelectedFunction(shadowScale.mediumResolution);
            ButtonUnselectedFunction(shadowScale.highResolution);
            ButtonUnselectedFunction(shadowScale.veryHighResolution);
        } else if (QualitySettings.shadowResolution == ShadowResolution.High) {
            ButtonUnselectedFunction(shadowScale.lowResolution);
            ButtonUnselectedFunction(shadowScale.mediumResolution);
            ButtonSelectedFunction(shadowScale.highResolution);
            ButtonUnselectedFunction(shadowScale.veryHighResolution);
        } else if (QualitySettings.shadowResolution == ShadowResolution.VeryHigh) {
            ButtonUnselectedFunction(shadowScale.lowResolution);
            ButtonUnselectedFunction(shadowScale.mediumResolution);
            ButtonUnselectedFunction(shadowScale.highResolution);
            ButtonSelectedFunction(shadowScale.veryHighResolution);
        }
    }

    public void TextureQualityLowFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonSelectedFunction(textureQuality.lowQuality);
        ButtonUnselectedFunction(textureQuality.mediumQuality);
        ButtonUnselectedFunction(textureQuality.highQuality);

        quality.indexQuality = 5;

        QualitySettings.masterTextureLimit = 2;
    }

    public void TextureQualityMediumFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(textureQuality.lowQuality);
        ButtonSelectedFunction(textureQuality.mediumQuality);
        ButtonUnselectedFunction(textureQuality.highQuality);

        quality.indexQuality = 5;

        QualitySettings.masterTextureLimit = 1;
    }

    public void TextureQualityHighFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(textureQuality.lowQuality);
        ButtonUnselectedFunction(textureQuality.mediumQuality);
        ButtonSelectedFunction(textureQuality.highQuality);

        quality.indexQuality = 5;

        QualitySettings.masterTextureLimit = 0;
    }

    public void AntiAliasingOffFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonSelectedFunction(antiAliasing.offAntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);

        quality.indexQuality = 5;

        QualitySettings.antiAliasing = 0;
    }

    public void AntiAliasingX2Function() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
        ButtonSelectedFunction(antiAliasing.x2AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);

        quality.indexQuality = 5;

        QualitySettings.antiAliasing = 2;
    }

    public void AntiAliasingX4Function() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
        ButtonSelectedFunction(antiAliasing.x4AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x8AntiAliasing);

        quality.indexQuality = 5;

        QualitySettings.antiAliasing = 4;
    }

    public void AntiAliasingX8Function() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(antiAliasing.offAntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x2AntiAliasing);
        ButtonUnselectedFunction(antiAliasing.x4AntiAliasing);
        ButtonSelectedFunction(antiAliasing.x8AntiAliasing);

        quality.indexQuality = 5;

        QualitySettings.antiAliasing = 8;
    }

    public void ShadowQualityDisabledFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonSelectedFunction(shadowMode.disableShadow);
        ButtonUnselectedFunction(shadowMode.hardShadow);
        ButtonUnselectedFunction(shadowMode.allShadow);

        quality.indexQuality = 5;

        QualitySettings.shadows = ShadowQuality.Disable;
    }

    public void ShadowQualityHardFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(shadowMode.disableShadow);
        ButtonSelectedFunction(shadowMode.hardShadow);
        ButtonUnselectedFunction(shadowMode.allShadow);

        quality.indexQuality = 5;

        QualitySettings.shadows = ShadowQuality.HardOnly;
    }

    public void ShadowQualityAllFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(shadowMode.disableShadow);
        ButtonUnselectedFunction(shadowMode.hardShadow);
        ButtonSelectedFunction(shadowMode.allShadow);

        quality.indexQuality = 5;

        QualitySettings.shadows = ShadowQuality.All;
    }

    public void ShadowResolutionLowFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonSelectedFunction(shadowScale.lowResolution);
        ButtonUnselectedFunction(shadowScale.mediumResolution);
        ButtonUnselectedFunction(shadowScale.highResolution);
        ButtonUnselectedFunction(shadowScale.veryHighResolution);

        quality.indexQuality = 5;

        QualitySettings.shadowResolution = ShadowResolution.Low;
    }

    public void ShadowResolutionMediumFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(shadowScale.lowResolution);
        ButtonSelectedFunction(shadowScale.mediumResolution);
        ButtonUnselectedFunction(shadowScale.highResolution);
        ButtonUnselectedFunction(shadowScale.veryHighResolution);

        quality.indexQuality = 5;

        QualitySettings.shadowResolution = ShadowResolution.Medium;
    }

    public void ShadowResolutionHighFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(shadowScale.lowResolution);
        ButtonUnselectedFunction(shadowScale.mediumResolution);
        ButtonSelectedFunction(shadowScale.highResolution);
        ButtonUnselectedFunction(shadowScale.veryHighResolution);

        quality.indexQuality = 5;

        QualitySettings.shadowResolution = ShadowResolution.High;
    }

    public void ShadowResolutionVeryHighFunction() {
        if (isMainMenu == true) {
            ButtonSelectedAudioFunction();
        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);
        }

        ButtonUnselectedFunction(shadowScale.lowResolution);
        ButtonUnselectedFunction(shadowScale.mediumResolution);
        ButtonUnselectedFunction(shadowScale.highResolution);
        ButtonSelectedFunction(shadowScale.veryHighResolution);

        quality.indexQuality = 5;

        QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
    }

    public void ButtonLowerQualityFunction() {
        if (isMainMenu == true) {
            ButtonClickAudioFunction();

        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);
        }

        quality.indexQuality--;

        if (quality.indexQuality < 0) {
            quality.indexQuality = quality.stringQuality.Length - 1;
        }

        if (quality.indexQuality <= 4) {
            QualitySettings.SetQualityLevel(quality.indexQuality, true);
        }

        PlayerPrefs.SetInt("IndexQuality", quality.indexQuality);
    }

    public void ButtonHigherQualityFunction() {
        if (isMainMenu == true) {
            ButtonClickAudioFunction();

        } else if (isMain == true) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);
        }

        quality.indexQuality++;

        if (quality.indexQuality == quality.stringQuality.Length - 1) {
            quality.indexQuality = 0;
        }

        if (quality.indexQuality <= 4) {
            QualitySettings.SetQualityLevel(quality.indexQuality, true);
        }

        PlayerPrefs.SetInt("IndexQuality", quality.indexQuality);
    }

    public void ButtonSelectedFunction(GameObject gameObject) {
        gameObject.GetComponent<Image>().sprite = spriteButtonSelected;
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void ButtonSelectedAudioFunction() {
        MainMenuController.mainMenuController.mainMenu.audioButton.Stop();
        MainMenuController.mainMenuController.mainMenu.audioButton.clip = MainMenuController.mainMenuController.mainMenu.audioButtonSelect;
        MainMenuController.mainMenuController.mainMenu.audioButton.Play();
    }

    public void ButtonClickAudioFunction() {
        MainMenuController.mainMenuController.mainMenu.audioButton.Stop();
        MainMenuController.mainMenuController.mainMenu.audioButton.clip = MainMenuController.mainMenuController.mainMenu.audioButtonClick;
        MainMenuController.mainMenuController.mainMenu.audioButton.Play();
    }

    public void ButtonUnselectedFunction(GameObject gameObject) {
        gameObject.GetComponent<Image>().sprite = spriteButtonUnselected;
        gameObject.GetComponent<Image>().color = new Color32(200, 100, 50, 255);
    }
}
