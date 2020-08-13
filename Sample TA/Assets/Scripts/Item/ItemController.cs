using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemController : MonoBehaviour {

    GameObject 
        player;

    public string
        itemName;

    public bool 
        isTouch;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        DetectTouchObject();
    }

    //Fungsi deteksi objek tersentuh
    public void DetectTouchObject() {
        //Kondisi pengaktifan isTouch
        if (isTouch == true) {
            ActionController.actionController.pickUp.isTouchItem = true;
            ActionController.actionController.pickUp.itemName = itemName;
        } else if (isTouch == false) {
            ActionController.actionController.pickUp.isTouchItem = false;
            ActionController.actionController.pickUp.itemName = "";
        }
        
        if (isTouch == true && ActionController.actionController.isPicking == true) {
            ActionController.actionController.isAction = false;
            ActionController.actionController.isPicking = false;
            ActionController.actionController.pickUp.isTouchItem = false;
            ActionController.actionController.pickUp.itemName = "";
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (ActionController.actionController.pickUp.isTouchItem == false) {
                isTouch = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            isTouch = false;
        }
    }
}
