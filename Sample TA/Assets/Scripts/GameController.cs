using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController 
        gameController;

    public string 
        nameSelectedAction,
        nameSelectedTools,
        nameSelectedSeeds;

    [System.Serializable]
    public class Clock {
        public int
            hours,
            minutes,
            date;

        public string 
            periods, 
            day, 
            month;
    }
    public Clock clock = new Clock();

    [System.Serializable]
    public class ExitApplication {
        public GameObject
            notice;

        public float 
            cooldown;

        public bool 
            isPressed;
    }
    public ExitApplication exitApplication = new ExitApplication();

    [System.Serializable]
    public class Environment {
        public GameObject 
            sun, moon;

        public float 
            timeCooldown;
    }
    public Environment environment = new Environment();

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
    }

    void Update() {
        QuitFunction();
        SunFunction();
    }

    public void QuitFunction() {
        if (Input.GetKeyDown(KeyCode.Escape) && exitApplication.isPressed == false) {
            exitApplication.cooldown = 2f;
            exitApplication.isPressed = true;

            exitApplication.notice.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && exitApplication.isPressed == true) {
            exitApplication.isPressed = false;

            Application.Quit();

            Debug.Log("Application Quit");
        }

        if (exitApplication.isPressed == true) {
            exitApplication.cooldown -= Time.deltaTime;

            if (exitApplication.cooldown <= 0f) {
                exitApplication.isPressed = false;

                exitApplication.notice.SetActive(false);
            }
        }
    }

    public void SunFunction() {
        environment.sun.transform.Rotate(new Vector3((2.5f / environment.timeCooldown) * Time.deltaTime, 0, 0));
        environment.moon.transform.Rotate(new Vector3((2.5f / environment.timeCooldown) * Time.deltaTime, 0, 0));
        if (environment.sun.transform.eulerAngles.x == 360) {
            environment.sun.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (environment.moon.transform.eulerAngles.x == 360) {
            environment.moon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void QualityLevelFunction(int indexLevel) {
        QualitySettings.SetQualityLevel(indexLevel, true);
    }
}
