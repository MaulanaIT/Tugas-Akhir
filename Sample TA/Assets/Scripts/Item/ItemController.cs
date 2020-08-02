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
        float
            //Seberapa panjang jarak antara objek dengan player
            distanceX,
            distanceZ,

            //Seberapa panjang jarak untuk mengaktifkan isTouch
            provisionX,
            provisionZ;

        //Fungsi untuk mengetahui jarak objek dengan player
        distanceX = transform.position.x - player.transform.position.x;
        distanceZ = transform.position.z - player.transform.position.z;

        //Fungsi untuk menentukan jarak pengaktifan isTouch
        provisionX = (transform.localScale.x / 2) + player.GetComponent<CapsuleCollider>().radius;
        provisionZ = (transform.localScale.z / 2) + player.GetComponent<CapsuleCollider>().radius;

        //Kondisi pengaktifan isTouch
        if (ActionController.actionController.pickUp.isTouchItem == false && Math.Abs(distanceX) <= provisionX && Math.Abs(distanceZ) <= provisionZ) {
            isTouch = true;

            ActionController.actionController.pickUp.isTouchItem = true;
            ActionController.actionController.pickUp.itemName = itemName;
        } else if (isTouch == true && (Math.Abs(distanceX) > provisionX || Math.Abs(distanceZ) > provisionZ)) {
            isTouch = false;

            ActionController.actionController.pickUp.isTouchItem = false;
            ActionController.actionController.pickUp.itemName = "";
        }
        
        if (ActionController.actionController.isPicking == true) {
            ActionController.actionController.isPicking = false;
            ActionController.actionController.pickUp.isTouchItem = false;
            ActionController.actionController.pickUp.itemName = "";
            Destroy(gameObject);
        }
    }
}
