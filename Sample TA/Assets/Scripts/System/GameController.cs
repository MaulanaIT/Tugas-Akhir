using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController 
        gameController;

    public string 
        location;

    [System.Serializable]
    public class Action {
        public string
            nameSelectedAction,
            nameSelectedTools,
            nameSelectedSeeds;
    }
    public Action action = new Action();

    [System.Serializable]
    public class Clock {
        public int
            hours,
            minutes,
            date;

        public string 
            day, 
            month;
    }
    public Clock clock = new Clock();

    [System.Serializable]
    public class Notice {
        public GameObject
            notice;

        public Text 
            textNotice;

        public float 
            cooldown;

        public bool 
            isPressed;
    }
    public Notice notice = new Notice();

    [System.Serializable]
    public class Environment {
        public GameObject 
            sun, 
            moon;

        public float 
            timeCooldown;
    }
    public Environment environment = new Environment();

    [System.Serializable]
    public class Menu {
        public GameObject 
            panelUI, 
            panelMenu;
    }
    public Menu menu = new Menu();

    [System.Serializable]
    public class Setting {
        public GameObject
            panelSetting;
    }
    public Setting setting = new Setting();

    [System.Serializable]
    public class Audio {
        public AudioSource 
            audioButton, 
            audioTheme,
            audioEffect;

        public AudioClip 
            audioButtonClick, 
            audioButtonSelect, 
            audioPlayerWalk, 
            audioPlayerRun, 
            audioPlayerDigging, 
            audioPlayerHarvesting;

        public AudioClip[] 
            audioThemePlay;
    }
    public Audio audio = new Audio();

    public float
        sunEuler,
        moonEuler;

    private void Awake() {
        if (gameController == null) {
            gameController = this;
        } else if (gameController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        environment.sun = GameObject.FindGameObjectWithTag("Sun");
        environment.moon = GameObject.FindGameObjectWithTag("Moon");

        environment.timeCooldown = ClockController.clockController.timeCooldown;

        location = PlayerPrefs.GetString("Location");

        action.nameSelectedAction = "Conversation";
        action.nameSelectedTools = "Hoe";
    }

    void Update() {
        ActionFunction();
        QuitFunction();
        SunFunction();
        SettingFunction();
        ThemeFunction();
    }

    public void ActionFunction() {
        bool
            isListed = false;

        for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
            if (action.nameSelectedSeeds == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                isListed = true;
                break;
            } else {
                isListed = false;
            }
        }

        if (isListed == false) {
            action.nameSelectedSeeds = null;
        }

        for (int i = 0; i < InventoryToolsController.inventoryToolsController.toolsObtained.name.Length; i++) {
            if (action.nameSelectedTools == InventoryToolsController.inventoryToolsController.toolsObtained.name[i]) {
                isListed = true;
                break;
            } else {
                isListed = false;
            }
        }

        if (isListed == false) {
            action.nameSelectedTools = null;
        }
    }

    public void QuitFunction() {
        if (Input.GetKeyDown(KeyCode.Escape) && notice.isPressed == false) {
            notice.cooldown = 2f;
            notice.isPressed = true;

            notice.notice.SetActive(true);

            notice.textNotice.text = "Tap Again To Exit";
        } else if (Input.GetKeyDown(KeyCode.Escape) && notice.isPressed == true) {
            notice.isPressed = false;

            Application.Quit();

            Debug.Log("Application Quit");
        }

        if (notice.isPressed == true) {
            notice.cooldown -= Time.deltaTime;

            if (notice.cooldown <= 0f) {
                notice.isPressed = false;

                notice.notice.SetActive(false);
            }
        }
    }

    public void SunFunction() {
        sunEuler += 2.5f / environment.timeCooldown * Time.deltaTime;
        moonEuler += 2.5f / environment.timeCooldown * Time.deltaTime;

        if (sunEuler >= 360) {
            sunEuler -= 360;
        }

        if (moonEuler >= 360) {
            moonEuler -= 360;
        }


        if (sunEuler >= 180) {
            environment.sun.transform.eulerAngles = new Vector3(360 + sunEuler, 0, 0);
        } else {
            environment.sun.transform.eulerAngles = new Vector3(sunEuler, 0, 0);
        }

        if (moonEuler >= 180) {
            environment.moon.transform.eulerAngles = new Vector3(360 + moonEuler, 0, 0);
        } else {
            environment.moon.transform.eulerAngles = new Vector3(moonEuler, 0, 0);
        }
    }

    public void ThemeFunction() {
        if (location == "Jawa") {
            audio.audioTheme.clip = audio.audioThemePlay[0];
        } else if (location == "Sumatera") {
            audio.audioTheme.clip = audio.audioThemePlay[1];
        }

        if (!audio.audioTheme.isPlaying) {
            audio.audioTheme.Play();
        }

        audio.audioTheme.volume = SettingController.settingController.audioVolume.musicVolume.value / 100;
    }

    public void SettingFunction() {
        audio.audioButton.volume = SettingController.settingController.audioVolume.soundVolume.value / 100f;
        audio.audioEffect.volume = SettingController.settingController.audioVolume.soundVolume.value / 100f;
    }

    public void ButtonOpenSettingFunction() {
        AudioButtonFunction(audio.audioButtonSelect);

        setting.panelSetting.SetActive(true);
    }

    public void ButtonCloseSettingFunction() {
        AudioButtonFunction(audio.audioButtonSelect);

        setting.panelSetting.SetActive(false);
    }

    public void AudioButtonFunction(AudioClip audioClip) {
        audio.audioButton.Stop();
        audio.audioButton.clip = audioClip;
        audio.audioButton.Play();
    }
}
