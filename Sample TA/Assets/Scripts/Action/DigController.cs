using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigController : MonoBehaviour {

    public GameObject actionPoint, playerPoint;

    private float 
        xRange, 
        zRange;

    void Start() {
        actionPoint = GameObject.FindGameObjectWithTag("ActionPoint");
        playerPoint = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        xRange = actionPoint.transform.localScale.x / 2;
        zRange = actionPoint.transform.localScale.z / 2;

        if (actionPoint.transform.position.x > transform.position.x - xRange && actionPoint.transform.position.x < transform.position.x + xRange &&
            actionPoint.transform.position.z > transform.position.z - zRange && actionPoint.transform.position.z < transform.position.z + zRange) {
            PlayerController.playerController.farm.digStatus = true;

            PlayerController.playerController.farm.digTimer = 0.2f;
        }

        if (playerPoint.transform.position.x > transform.position.x - playerPoint.transform.localScale.x / 2 && playerPoint.transform.position.x < transform.position.x + playerPoint.transform.localScale.x / 2 &&
            playerPoint.transform.position.z > transform.position.z - playerPoint.transform.localScale.z / 2 && playerPoint.transform.position.z < transform.position.z + playerPoint.transform.localScale.z / 2) {
            PlayerController.playerController.farm.digCurrentStatus = true;

            PlayerController.playerController.farm.digCurrentTimer = 0.2f;
        }
    }
}
