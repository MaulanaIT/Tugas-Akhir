using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour {

    public static NotificationController
        notificationController;

    public GameObject 
        panelNotification, 
        panelScrollable, 
        notification;

    public List<GameObject> 
        listNotification;

    public bool 
        isNotification, 
        isSort;

    private void Awake() {
        if (notificationController == null) {
            notificationController = this;
        } else if (notificationController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {
        if (isSort == true) {
            for (int i = 0; i < listNotification.Count; i++) {
                if (listNotification[i] == null) {
                    listNotification.RemoveAt(i);
                }
            }

            isSort = false;
        }
    }

    public void ButtonNotificationFunction() {
        GameController.gameController.audio.audioButton.Stop();
        GameController.gameController.audio.audioButton.clip = GameController.gameController.audio.audioButtonSelect;
        GameController.gameController.audio.audioButton.Play();

        if (isNotification == false) {
            panelNotification.SetActive(true);
            isNotification = true;
            isSort = true;
        } else {
            panelNotification.SetActive(false);
            isNotification = false;
            isSort = true;
        }
    }
}
