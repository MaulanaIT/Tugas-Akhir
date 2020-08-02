using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickTouch : MonoBehaviour {

    int i = 0, bounce = 0;

    void Start() {
        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && i == 0 && bounce == 0) {
            bounce = 1;
            if (bounce == 1) {
                Time.timeScale = 1;
                i = 1;
                bounce = 0;
            }
        } else if (Input.GetMouseButtonDown(0) && i == 1 && bounce == 0) {
            bounce = 1;
            if (bounce == 1) {
                Time.timeScale = 0;
                i = 0;
                bounce = 0;
            }
        }
    }
}
