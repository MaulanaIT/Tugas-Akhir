using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

    public GameObject
        player,
        actionPoint,
        prefebSecondPhase,
        prefebThirdPhase,
        prefebHarvestPhase;

    public Mesh
        meshSecondPhase,
        meshThirdPhase,
        meshHarvestPhase;

    [System.Serializable]
    public class Cengkeh {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;

    }
    public Cengkeh cengkeh = new Cengkeh();

    [System.Serializable]
    public class Karet {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public Karet karet = new Karet();

    [System.Serializable]
    public class KelapaSawit {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public KelapaSawit kelapaSawit = new KelapaSawit();

    [System.Serializable]
    public class Tembakau {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public Tembakau tembakau = new Tembakau();

    [System.Serializable]
    public class KopiRobusta {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public KopiRobusta kopiRobusta = new KopiRobusta();

    [System.Serializable]
    public class Teh {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public Teh teh = new Teh();

    [System.Serializable]
    public class KopiArabika {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public KopiArabika kopiArabika = new KopiArabika();

    [System.Serializable]
    public class Tebu {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public Tebu tebu = new Tebu();

    [System.Serializable]
    public class Kakao {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase;
    }
    public Kakao kakao = new Kakao();

    private float
        xRange, 
        zRange,
        xPosition, 
        zPosition;

    public string 
        seedName, 
        harvestName;

    public float 
        firstPhase, 
        secondPhase, 
        thirdPhase, 
        repeatableHarvestTime, 
        timeCooldown, 
        speedCooldown;

    public bool
        isTouch, 
        isMoveAway,
        isHarvest;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        actionPoint = GameObject.FindGameObjectWithTag("ActionPoint");
        seedName = GameController.gameController.action.nameSelectedSeeds;

        //Penentuan variabel sesuai nama benih
        switch (seedName) {
            case "Cengkeh":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = cengkeh.prefebSecondPhase;
                prefebThirdPhase = cengkeh.prefebThirdPhase;
                prefebHarvestPhase = cengkeh.prefebHarvestPhase;
                harvestName = "Daun Cengkeh";

                GetMeshFunction();
                break;
            case "Karet":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = karet.prefebSecondPhase;
                prefebThirdPhase = karet.prefebThirdPhase;
                prefebHarvestPhase = karet.prefebHarvestPhase;
                harvestName = "Getah Karet";

                GetMeshFunction();
                break;
            case "Kelapa Sawit":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kelapaSawit.prefebSecondPhase;
                prefebThirdPhase = kelapaSawit.prefebThirdPhase;
                prefebHarvestPhase = kelapaSawit.prefebHarvestPhase;
                harvestName = "Buah Kelapa Sawit";

                GetMeshFunction();
                break;
            case "Tembakau":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = tembakau.prefebSecondPhase;
                prefebThirdPhase = tembakau.prefebThirdPhase;
                prefebHarvestPhase = tembakau.prefebHarvestPhase;
                harvestName = "Daun Tembakau";

                GetMeshFunction();
                break;
            case "Kopi Robusta":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kopiRobusta.prefebSecondPhase;
                prefebThirdPhase = kopiRobusta.prefebThirdPhase;
                prefebHarvestPhase = kopiRobusta.prefebHarvestPhase;
                harvestName = "Biji Kopi Robusta";

                GetMeshFunction();
                break;
            case "Teh":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = teh.prefebSecondPhase;
                prefebThirdPhase = teh.prefebThirdPhase;
                prefebHarvestPhase = teh.prefebHarvestPhase;
                harvestName = "Daun Teh";

                GetMeshFunction();
                break;
            case "Kopi Arabika":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kopiArabika.prefebSecondPhase;
                prefebThirdPhase = kopiArabika.prefebThirdPhase;
                prefebHarvestPhase = kopiArabika.prefebHarvestPhase;
                harvestName = "Biji Kopi Arabika";

                GetMeshFunction();
                break;
            case "Tebu":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = tebu.prefebSecondPhase;
                prefebThirdPhase = tebu.prefebThirdPhase;
                prefebHarvestPhase = tebu.prefebHarvestPhase;
                harvestName = "Tebu";

                GetMeshFunction();
                break;
            case "Kakao":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kakao.prefebSecondPhase;
                prefebThirdPhase = kakao.prefebThirdPhase;
                prefebHarvestPhase = kakao.prefebHarvestPhase;
                harvestName = "Buah Kakao";

                GetMeshFunction();
                break;
        }

        timeCooldown = 86400f;

        for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
            if (GameController.gameController.action.nameSelectedSeeds == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                InventorySeedsController.inventorySeedsController.seedsObtained.count[i]--;
            }
        }

        CheckingAllSlot();
    }

    void Update() {
        SeedPlantingFunction();
        SeedGrowthFunction();
        DetectTouchObject();
        IsActedOn();
    }

    //Fungsi pengecekan status lahan penanaman
    public void SeedPlantingFunction() {
        xRange = player.transform.localScale.x / 2;
        zRange = player.transform.localScale.z / 2;

        if (player.transform.position.x > transform.position.x - xRange && player.transform.position.x < transform.position.x + xRange &&
            player.transform.position.z > transform.position.z - zRange && player.transform.position.z < transform.position.z + zRange) {
            PlayerController.playerController.farm.seedStatus = true;

            PlayerController.playerController.farm.seedTimer = 0.2f;
        }
    }

    public void SeedGrowthFunction() {
        //Kondisi timer fase
        if (thirdPhase > 0) {
            timeCooldown -= Time.deltaTime * speedCooldown;
        }

        //Kondisi apabila timer fase telah habis
        if (timeCooldown <= 0) {
            if (firstPhase > 0 && secondPhase > 0 && thirdPhase > 0) {
                firstPhase--;

                if (firstPhase == 0) {
                    this.gameObject.GetComponent<MeshFilter>().mesh = meshSecondPhase;
                    this.gameObject.GetComponent<CapsuleCollider>().radius = prefebSecondPhase.GetComponent<CapsuleCollider>().radius;
                    this.gameObject.transform.localScale = prefebSecondPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);
                }
            }
            else if (firstPhase <= 0 && secondPhase > 0 && thirdPhase > 0) {
                secondPhase--;

                if (secondPhase == 0) {
                    this.gameObject.GetComponent<MeshFilter>().mesh = meshThirdPhase;
                    this.gameObject.GetComponent<CapsuleCollider>().radius = prefebThirdPhase.GetComponent<CapsuleCollider>().radius;
                    this.gameObject.transform.localScale = prefebThirdPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);
                }
            }
            else if (firstPhase <= 0 && secondPhase <= 0 && thirdPhase > 0) {
                thirdPhase--;

                if (thirdPhase == 0) {
                    this.gameObject.GetComponent<MeshFilter>().mesh = meshHarvestPhase;
                    this.gameObject.GetComponent<CapsuleCollider>().radius = prefebHarvestPhase.GetComponent<CapsuleCollider>().radius;
                    this.gameObject.transform.localScale = prefebHarvestPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);

                    isHarvest = true;
                }
            }

            timeCooldown = 86400f;
        }
    }

    //Fungsi deteksi objek tersentuh
    public void DetectTouchObject() {

        //Kondisi pengaktifan isTouch
        if (firstPhase <= 0) {
            if (isTouch == true) {
                ActionController.actionController.isHarvest = isHarvest;
                ActionController.actionController.pickUp.itemName = harvestName;
            } else if (isTouch == false && isMoveAway == true) {
                ActionController.actionController.isHarvest = false;
                ActionController.actionController.pickUp.itemName = "";

                isMoveAway = false;
            }
        }
    }

    //Fungsi aksi saat tersentuh
    public void IsActedOn() {
        if (isTouch == true && isHarvest == true && ActionController.actionController.isHarvesting == true && GameController.gameController.action.nameSelectedAction == "Harvest" && 
            ActionController.actionController.actionDuration.sliderDuration.value >= 1 && thirdPhase <= 0) {
            this.gameObject.GetComponent<MeshFilter>().mesh = meshThirdPhase;
            this.gameObject.transform.localScale = prefebThirdPhase.transform.localScale;
            this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);

            thirdPhase = repeatableHarvestTime;

            isHarvest = false;
        } else if (isHarvest == true && isTouch == true && GameController.gameController.action.nameSelectedAction == "Tools" && GameController.gameController.action.nameSelectedTools == "Sickle" && 
            ActionController.actionController.isAction == true && firstPhase <= 0) {
            Destroy(gameObject);
        }
    }

    //Fungsi untuk meruabah bentuk objeck sesuai fase
    public void GetMeshFunction() {
        meshSecondPhase = prefebSecondPhase.GetComponent<MeshFilter>().sharedMesh;
        meshThirdPhase = prefebThirdPhase.GetComponent<MeshFilter>().sharedMesh;
        meshHarvestPhase = prefebHarvestPhase.GetComponent<MeshFilter>().sharedMesh;
    }

    public void CheckingAllSlot() {
        InventorySeedsController.inventorySeedsController.isChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
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
            isMoveAway = true;
        }
    }
}
