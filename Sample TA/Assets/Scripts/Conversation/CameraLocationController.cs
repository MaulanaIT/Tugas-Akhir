using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocationController : MonoBehaviour {

    [System.Serializable]
    public class CameraFunction {
        public float
            cameraDistanceX,
            cameraDistanceY,
            cameraDistanceZ,

            cameraRotationX,
            cameraRotationY,
            cameraRotationZ;
    }
    public CameraFunction cameraFunction = new CameraFunction();

    [System.Serializable]
    public class ConversationFunction {
        public GameObject 
            player;

        public float 
            distanceX, 
            distanceY, 
            distanceZ;

        public bool 
            isTouch;
    }
    public ConversationFunction conversationFunction = new ConversationFunction();

    void Start() {
        conversationFunction.player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        //Fungsi pengecekan jarak objek dengan player
        conversationFunction.distanceX = transform.position.x - conversationFunction.player.transform.position.x;
        conversationFunction.distanceZ = transform.position.z - conversationFunction.player.transform.position.z;

        //Kondisi pengaktifan isTouch
        if (conversationFunction.isTouch == false && Math.Abs(conversationFunction.distanceX) <= 0.7f && Math.Abs(conversationFunction.distanceZ) <= 0.7f) {

            conversationFunction.isTouch = true;
            ActionController.actionController.conversationFunction.isTouchNPC = true;

        } else if (conversationFunction.isTouch == true && (Math.Abs(conversationFunction.distanceX) > 0.7f || Math.Abs(conversationFunction.distanceZ) > 0.7f)) {

            conversationFunction.isTouch = false;
            ActionController.actionController.conversationFunction.isTouchNPC = false;
        }

        //Kondisi penggunaan isFlip
        if (conversationFunction.isTouch == true) {
            ActionController.actionController.conversationFunction.conversationCamera.transform.position = new Vector3(
                transform.position.x + cameraFunction.cameraDistanceX, transform.position.y + cameraFunction.cameraDistanceY, transform.position.z + cameraFunction.cameraDistanceZ);
            ActionController.actionController.conversationFunction.conversationCamera.transform.rotation = Quaternion.Euler(
                cameraFunction.cameraRotationX, cameraFunction.cameraRotationY, cameraFunction.cameraRotationZ);
        }
    }
}
