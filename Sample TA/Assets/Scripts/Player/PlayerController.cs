using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController playerController;

    [System.Serializable]
    public class Farm {
        public float 
            digTimer, 
            digCurrentTimer, 
            seedTimer;

        public bool
            digStatus,
            digCurrentStatus,
            seedStatus;
    }
    public Farm farm = new Farm();

    [System.Serializable]
    public class Boolean {
        public bool isGrounded;
    }
    public Boolean boolean = new Boolean();

    private void Awake() {
        if (playerController == null) {
            playerController = this;
        } else if (playerController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

    }

    void Update() {
        PlayerMove();
        PlayerDig();
        PlayerSeed();
        PlayerCurrentDig();
    }

    public void PlayerMove() {

    }

    public void PlayerDig() {
        if (farm.digTimer >= 0) {
            farm.digTimer -= Time.deltaTime;
        }

        if (farm.digTimer <= 0) {
            farm.digStatus = false;
        }
    }
    public void PlayerSeed() {
        if (farm.seedTimer >= 0) {
            farm.seedTimer -= Time.deltaTime;
        }

        if (farm.seedTimer <= 0) {
            farm.seedStatus = false;
        }
    }

    public void PlayerCurrentDig() {
        if (farm.digCurrentTimer >= 0) {
            farm.digCurrentTimer -= Time.deltaTime;
        }

        if (farm.digCurrentTimer <= 0) {
            farm.digCurrentStatus = false;
        }
    }
}
