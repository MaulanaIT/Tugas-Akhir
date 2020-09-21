using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LampController : MonoBehaviour {

    public GameObject 
        player,
        prefebLamp;

    public bool
        isLight;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        Transform parent = gameObject.transform;

        if (GameController.gameController.clock.hours > 15 || GameController.gameController.clock.hours < 7) {
            if (Math.Abs(gameObject.transform.position.x - player.transform.position.x) <= QualitySettings.shadowDistance &&
            Math.Abs(gameObject.transform.position.z - player.transform.position.z) <= QualitySettings.shadowDistance) {
                if (isLight == false) {
                    GameObject lamp = Instantiate(prefebLamp);
                    lamp.transform.SetParent(gameObject.transform);
                    lamp.transform.localPosition = new Vector3(0.95f, 2.2f, 0f);
                    isLight = true;
                }
            } else {
                if (isLight == true) {
                    for (int i = 0; i < gameObject.transform.parent.childCount; i++) {
                        Transform child = parent.GetChild(i);

                        if (child.tag == "Light") {
                            Destroy(child.gameObject);
                            isLight = false;
                            break;
                        }
                    }
                }
            }
        } else {
            isLight = false;
            Destroy(GameObject.FindGameObjectWithTag("Light"));
        }

        if (isLight == true) {
            for (int i = 0; i < gameObject.transform.parent.childCount; i++) {
                Transform child = parent.GetChild(i);

                if (child.tag == "Light") {
                    if (child.GetComponent<Light>().intensity <= 5) {
                        child.GetComponent<Light>().intensity = Math.Abs(QualitySettings.shadowDistance - ((Math.Abs(gameObject.transform.position.x - player.transform.position.x) + Math.Abs(gameObject.transform.position.z - player.transform.position.z)) / 2)) / (QualitySettings.shadowDistance * 17 / 100);

                        if (child.GetComponent<Light>().intensity > 5) {
                            child.GetComponent<Light>().intensity = 5;
                        }
                    }
                    break;
                }
            }
        }
    }
}
