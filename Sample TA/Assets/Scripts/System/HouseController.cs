using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour {

    Animator 
        animFadeScreen, 
        animDoor;

    public GameObject
        nameHouse,
        fadeScreen;

    public float 
        cooldown = 2f;

    public bool 
        isTouch, 
        isOpen;

    void Start() {
        animFadeScreen = fadeScreen.GetComponent<Animator>();
        animDoor = gameObject.GetComponent<Animator>();
    }

    void Update() {
        if (isTouch == true) {
            nameHouse.SetActive(true);
            nameHouse.transform.LookAt(Camera.main.transform);
            if (ActionController.actionController.isAction == true && GameController.gameController.action.nameSelectedAction == "Interact") {
                isOpen = true;
            }
        } else {
            nameHouse.SetActive(false);
        }

        if (isOpen == true) {
            animFadeScreen.SetBool("Sleep", true);
            animFadeScreen.SetBool("Idle", false);
            animDoor.SetBool("Open", true);
            animDoor.SetBool("Idle", false);

            if (animFadeScreen.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) {
                GameController.gameController.audio.audioTheme.Stop();

                cooldown -= Time.deltaTime;

                if (cooldown <= 0) {
                    GameObject.FindGameObjectWithTag("Player").transform.localPosition = new Vector3(290f, 1.8f, 294f);
                    ClockController.clockController.minute = 0;
                    ClockController.clockController.hour = 30;
                    GameController.gameController.environment.sun.transform.localRotation = Quaternion.Euler(420f, 0, 0);
                    GameController.gameController.environment.moon.transform.localRotation = Quaternion.Euler(140f, 0, 0);
                    isOpen = false;

                    cooldown = 2f;
                }
            }
        } else {
            if (animFadeScreen.GetBool("Sleep") == true) {
                animDoor.SetBool("Open", false);
                animDoor.SetBool("Idle", true);

                animFadeScreen.SetBool("Sleep", false);
                animFadeScreen.SetBool("WakeUp", true);
            }
        }
    }

    //Awal fungsi objek tersentuh
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (isTouch == false) {
                isTouch = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            isTouch = false;
        }
    }
    //Akhir fungsi objek tersentuh
}
