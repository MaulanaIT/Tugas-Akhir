using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
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
            //periods, 
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
            panelSetting, 
            lowerQuality, 
            higherQuality;

        public Text 
            textQuality, 
            textCameraSensitivity, 
            textSoundVolume, 
            textMusicVolume;

        public Slider 
            cameraSensitivity, 
            soundVolume, 
            musicVolume;

        public string []
            stringQuality;

        public int 
            indexQuality = 2;
    }
    public Setting setting = new Setting();

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
    }

    void Update() {
        QuitFunction();
        SunFunction();
        SettingFunction();
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
        environment.sun.transform.Rotate((2.5f / environment.timeCooldown) * Time.deltaTime, 0, 0);
        environment.moon.transform.Rotate((2.5f / environment.timeCooldown) * Time.deltaTime, 0, 0);
        if (environment.sun.transform.eulerAngles.x == 360) {
            environment.sun.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (environment.moon.transform.eulerAngles.x == 360) {
            environment.moon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void SettingFunction() {
        QualitySettings.SetQualityLevel(setting.indexQuality, true);

        setting.textQuality.text = setting.stringQuality[setting.indexQuality];

        if (setting.indexQuality <= 0) {
            setting.lowerQuality.SetActive(false);
        } else {
            setting.lowerQuality.SetActive(true);
        }

        if (setting.indexQuality >= 4) {
            setting.higherQuality.SetActive(false);
        } else {
            setting.higherQuality.SetActive(true);
        }

        setting.textCameraSensitivity.text = "" + Convert.ToInt32(setting.cameraSensitivity.value);
        setting.textSoundVolume.text = "" + Convert.ToInt32(setting.soundVolume.value);
        setting.textMusicVolume.text = "" + Convert.ToInt32(setting.musicVolume.value);
    }

    public void ButtonMenuFunction() {
        this.gameObject.GetComponent<GameController>().enabled = true;

        menu.panelUI.SetActive(false);
        menu.panelMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void ButtonResumeFunction() {
        this.gameObject.GetComponent<GameController>().enabled = true;

        menu.panelMenu.SetActive(false);
        menu.panelUI.SetActive(true);

        Time.timeScale = 1;
    }

    public void ButtonMainMenuFunction() {

    }

    public void ButtonSettingFunction() {
        setting.panelSetting.SetActive(true);
        menu.panelMenu.SetActive(false);
    }

    public void ButtonLowerQualityFunction() {
        setting.indexQuality--;
    }

    public void ButtonHigherQualityFunction() {
        setting.indexQuality++;
    }

    public void ButtonCloseSettingFunction() {
        setting.panelSetting.SetActive(false);
        menu.panelMenu.SetActive(true);
    }
}
