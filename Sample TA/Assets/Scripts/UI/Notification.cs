using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text 
        textTitle, 
        textDescription;

    void Start() {
        
    }

    void Update() {
        
    }

    public void ButtonClearNotification() {
        Destroy(gameObject);
    }
}
