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
        minHarvestItem,
        maxHarvestItem,
        timeCooldown, 
        speedCooldown;

    public bool
        isTouch, 
        isMoveAway,
        isHarvest, 
        isNotification;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        actionPoint = GameObject.FindGameObjectWithTag("ActionPoint");

        if (PlayerPrefs.GetInt("LoadGame") == 0) {
            seedName = GameController.gameController.action.nameSelectedSeeds;

            //Penentuan variabel sesuai nama benih
            switch (seedName) {
                case "Cengkeh":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 3;
                    maxHarvestItem = 5;
                    break;
                case "Karet":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 1;
                    maxHarvestItem = 2;
                    break;
                case "Kelapa Sawit":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 3;
                    maxHarvestItem = 5;
                    break;
                case "Tembakau":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 2;
                    maxHarvestItem = 3;
                    break;
                case "Kopi Robusta":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 1;
                    maxHarvestItem = 3;
                    break;
                case "Teh":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 3;
                    maxHarvestItem = 7;
                    break;
                case "Kopi Arabika":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 1;
                    maxHarvestItem = 3;
                    break;
                case "Tebu":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 1;
                    maxHarvestItem = 1;
                    break;
                case "Kakao":
                    firstPhase = 2;
                    secondPhase = 5;
                    thirdPhase = 1;
                    repeatableHarvestTime = 1;

                    minHarvestItem = 14;
                    maxHarvestItem = 20;
                    break;
            }
            timeCooldown = 86400f;

            for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
                if (GameController.gameController.action.nameSelectedSeeds == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                    InventorySeedsController.inventorySeedsController.seedsObtained.count[i]--;

                    if (InventorySeedsController.inventorySeedsController.seedsObtained.count[i] <= 0) {
                        InventorySeedsController.inventorySeedsController.seedsObtained.name[i] = null;
                        InventorySeedsController.inventorySeedsController.seedsObtained.count[i] = 0;
                    }
                }
            }
        }
    }

    void Update() {
        SetSeedFunction();
        SeedPlantingFunction();
        SeedGrowthFunction();
        DetectTouchObject();
        IsActedOn();
    }

    //Penentuan variabel sesuai nama benih
    public void SetSeedFunction() {
        switch (seedName) {
            case "Cengkeh":
                prefebSecondPhase = cengkeh.prefebSecondPhase;
                prefebThirdPhase = cengkeh.prefebThirdPhase;
                prefebHarvestPhase = cengkeh.prefebHarvestPhase;
                harvestName = "Daun Cengkeh";

                GetMeshFunction();
                break;
            case "Karet":
                prefebSecondPhase = karet.prefebSecondPhase;
                prefebThirdPhase = karet.prefebThirdPhase;
                prefebHarvestPhase = karet.prefebHarvestPhase;
                harvestName = "Getah Karet";

                GetMeshFunction();
                break;
            case "Kelapa Sawit":
                prefebSecondPhase = kelapaSawit.prefebSecondPhase;
                prefebThirdPhase = kelapaSawit.prefebThirdPhase;
                prefebHarvestPhase = kelapaSawit.prefebHarvestPhase;
                harvestName = "Buah Kelapa Sawit";

                GetMeshFunction();
                break;
            case "Tembakau":
                prefebSecondPhase = tembakau.prefebSecondPhase;
                prefebThirdPhase = tembakau.prefebThirdPhase;
                prefebHarvestPhase = tembakau.prefebHarvestPhase;
                harvestName = "Daun Tembakau";

                GetMeshFunction();
                break;
            case "Kopi Robusta":
                prefebSecondPhase = kopiRobusta.prefebSecondPhase;
                prefebThirdPhase = kopiRobusta.prefebThirdPhase;
                prefebHarvestPhase = kopiRobusta.prefebHarvestPhase;
                harvestName = "Biji Kopi Robusta";

                GetMeshFunction();
                break;
            case "Teh":
                prefebSecondPhase = teh.prefebSecondPhase;
                prefebThirdPhase = teh.prefebThirdPhase;
                prefebHarvestPhase = teh.prefebHarvestPhase;
                harvestName = "Daun Teh";

                GetMeshFunction();
                break;
            case "Kopi Arabika":
                prefebSecondPhase = kopiArabika.prefebSecondPhase;
                prefebThirdPhase = kopiArabika.prefebThirdPhase;
                prefebHarvestPhase = kopiArabika.prefebHarvestPhase;
                harvestName = "Biji Kopi Arabika";

                GetMeshFunction();
                break;
            case "Tebu":
                prefebSecondPhase = tebu.prefebSecondPhase;
                prefebThirdPhase = tebu.prefebThirdPhase;
                prefebHarvestPhase = tebu.prefebHarvestPhase;
                harvestName = "Tebu";

                GetMeshFunction();
                break;
            case "Kakao":
                prefebSecondPhase = kakao.prefebSecondPhase;
                prefebThirdPhase = kakao.prefebThirdPhase;
                prefebHarvestPhase = kakao.prefebHarvestPhase;
                harvestName = "Buah Kakao";

                GetMeshFunction();
                break;
        }
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
            }
            else if (firstPhase <= 0 && secondPhase > 0 && thirdPhase > 0) {
                secondPhase--;
            }
            else if (firstPhase <= 0 && secondPhase <= 0 && thirdPhase > 0) {
                thirdPhase--;
            }

            timeCooldown = 86400f;
        }

        if (firstPhase == 0 && secondPhase != 0 && thirdPhase != 0) {
            this.gameObject.GetComponent<MeshFilter>().mesh = meshSecondPhase;
            this.gameObject.GetComponent<CapsuleCollider>().radius = prefebSecondPhase.GetComponent<CapsuleCollider>().radius;
            this.gameObject.transform.localScale = prefebSecondPhase.transform.localScale;
            this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);
        }

        if (firstPhase == 0 && secondPhase == 0 && thirdPhase != 0) {
            this.gameObject.GetComponent<MeshFilter>().mesh = meshThirdPhase;
            this.gameObject.GetComponent<CapsuleCollider>().radius = prefebThirdPhase.GetComponent<CapsuleCollider>().radius;
            this.gameObject.transform.localScale = prefebThirdPhase.transform.localScale;
            this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);
        }

        if (firstPhase == 0 && secondPhase == 0 && thirdPhase == 0) {
            this.gameObject.GetComponent<MeshFilter>().mesh = meshHarvestPhase;
            this.gameObject.GetComponent<CapsuleCollider>().radius = prefebHarvestPhase.GetComponent<CapsuleCollider>().radius;
            this.gameObject.transform.localScale = prefebHarvestPhase.transform.localScale;
            this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, ActionController.actionController.gridObject.transform.position.y + transform.localScale.y, transform.localPosition.z);

            if (isNotification == false) {
                GameObject notification = Instantiate(NotificationController.notificationController.notification, NotificationController.notificationController.panelScrollable.transform);

                NotificationController.notificationController.listNotification.Add(notification);

                notification.GetComponent<Notification>().textTitle.text = "Panen";
                notification.GetComponent<Notification>().textDescription.text = "Tanaman " + seedName + " siap untuk dipanen";

                isNotification = true;
            }

            isHarvest = true;
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

            ActionController.actionController.pickUp.itemCount = UnityEngine.Random.Range(minHarvestItem, maxHarvestItem);

            isNotification = false;
            isHarvest = false;
        } else if (isHarvest == true && isTouch == true && ActionController.actionController.isSickled == true && firstPhase <= 0) {
            Destroy(gameObject);
            ActionController.actionController.isSickled = false;
        }
    }

    //Fungsi untuk meruabah bentuk objeck sesuai fase
    public void GetMeshFunction() {
        meshSecondPhase = prefebSecondPhase.GetComponent<MeshFilter>().sharedMesh;
        meshThirdPhase = prefebThirdPhase.GetComponent<MeshFilter>().sharedMesh;
        meshHarvestPhase = prefebHarvestPhase.GetComponent<MeshFilter>().sharedMesh;
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
