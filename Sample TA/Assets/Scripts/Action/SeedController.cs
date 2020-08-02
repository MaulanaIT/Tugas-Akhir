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
        prefebHarvestPhase,
        prefebHarvestItem;

    public Mesh
        meshSecondPhase,
        meshThirdPhase,
        meshHarvestPhase;

    [System.Serializable]
    public class Cengkeh {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase, 
            prefebItem;
    }
    public Cengkeh cengkeh = new Cengkeh();

    [System.Serializable]
    public class Karet {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Karet karet = new Karet();

    [System.Serializable]
    public class Lada {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Lada lada = new Lada();

    [System.Serializable]
    public class Tembakau {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Tembakau tembakau = new Tembakau();

    [System.Serializable]
    public class Kapas {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Kapas kapas = new Kapas();

    [System.Serializable]
    public class Teh {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Teh teh = new Teh();

    [System.Serializable]
    public class Kopi {
        public GameObject
            prefebSecondPhase,
            prefebThirdPhase,
            prefebHarvestPhase,
            prefebItem;
    }
    public Kopi kopi = new Kopi();

    private float
        xRange, 
        zRange,
        xPosition, 
        zPosition;

    public string 
        seedName;

    public float 
        firstPhase, 
        secondPhase, 
        thirdPhase, 
        repeatableHarvestTime, 
        timeCooldown, 
        speedCooldown;

    public bool
        isTouch,
        isHarvest;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        actionPoint = GameObject.FindGameObjectWithTag("ActionPoint");
        seedName = GameController.gameController.nameSelectedSeeds;

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
                prefebHarvestItem = cengkeh.prefebItem;

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
                prefebHarvestItem = karet.prefebItem;

                GetMeshFunction();
                break;
            case "Lada":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = lada.prefebSecondPhase;
                prefebThirdPhase = lada.prefebThirdPhase;
                prefebHarvestPhase = lada.prefebHarvestPhase;
                prefebHarvestItem = lada.prefebItem;

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
                prefebHarvestItem = tembakau.prefebItem;

                GetMeshFunction();
                break;
            case "Kapas":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kapas.prefebSecondPhase;
                prefebThirdPhase = kapas.prefebThirdPhase;
                prefebHarvestPhase = kapas.prefebHarvestPhase;
                prefebHarvestItem = kapas.prefebItem;

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
                prefebHarvestItem = teh.prefebItem;

                GetMeshFunction();
                break;
            case "Kopi":
                firstPhase = 2;
                secondPhase = 5;
                thirdPhase = 1;
                repeatableHarvestTime = 1;

                prefebSecondPhase = kopi.prefebSecondPhase;
                prefebThirdPhase = kopi.prefebThirdPhase;
                prefebHarvestPhase = kopi.prefebHarvestPhase;
                prefebHarvestItem = kopi.prefebItem;

                GetMeshFunction();
                break;
        }

        timeCooldown = 86400f;
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
                    this.gameObject.transform.localScale = prefebSecondPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y - 0.05f, transform.localPosition.z);
                }
            }
            else if (firstPhase <= 0 && secondPhase > 0 && thirdPhase > 0) {
                secondPhase--;

                if (secondPhase == 0) {
                    this.gameObject.GetComponent<MeshFilter>().mesh = meshThirdPhase;
                    this.gameObject.transform.localScale = prefebThirdPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y - 0.05f, transform.localPosition.z);
                }
            }
            else if (firstPhase <= 0 && secondPhase <= 0 && thirdPhase > 0) {
                thirdPhase--;

                if (thirdPhase == 0) {
                    this.gameObject.GetComponent<MeshFilter>().mesh = meshHarvestPhase;
                    this.gameObject.transform.localScale = prefebHarvestPhase.transform.localScale;
                    this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y - 0.05f, transform.localPosition.z);

                    isHarvest = true;
                }
            }

            timeCooldown = 86400f;
        }
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
        provisionX = (transform.localScale.x / 2) + (player.GetComponent<CapsuleCollider>().radius / 2);
        provisionZ = (transform.localScale.z / 2) + (player.GetComponent<CapsuleCollider>().radius / 2);

        //Kondisi pengaktifan isTouch
        if (firstPhase <= 0) {
            if (ActionController.actionController.isHarvest == false && Math.Abs(distanceX) <= provisionX && Math.Abs(distanceZ) <= provisionZ) {
                isTouch = true;

                ActionController.actionController.isHarvest = isHarvest;
                ActionController.actionController.pickUp.itemName = seedName;
            } else if (isTouch == true && Math.Abs(distanceX) > provisionX || Math.Abs(distanceZ) > provisionZ) {
                isTouch = false;

                ActionController.actionController.isHarvest = false;
                ActionController.actionController.pickUp.itemName = "";
            }
        }
    }

    //Fungsi aksi saat tersentuh
    public void IsActedOn() {
        if (isTouch == true && isHarvest == true && ActionController.actionController.isHarvesting == true && GameController.gameController.nameSelectedAction == "Harvest" && 
            ActionController.actionController.actionDuration.sliderDuration.value >= 1 && thirdPhase <= 0) {
            this.gameObject.GetComponent<MeshFilter>().mesh = meshThirdPhase;
            this.gameObject.transform.localScale = prefebThirdPhase.transform.localScale;
            this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y - 0.05f, transform.localPosition.z);

            thirdPhase = repeatableHarvestTime;

            isHarvest = false;
        } else if (isHarvest == true && isTouch == true && GameController.gameController.nameSelectedAction == "Tools" && GameController.gameController.nameSelectedTools == "Sickle" && 
            ActionController.actionController.isAction == true && firstPhase <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    //Fungsi untuk meruabah bentuk objeck sesuai fase
    public void GetMeshFunction() {
        meshSecondPhase = prefebSecondPhase.GetComponent<MeshFilter>().sharedMesh;
        meshThirdPhase = prefebThirdPhase.GetComponent<MeshFilter>().sharedMesh;
        meshHarvestPhase = prefebHarvestPhase.GetComponent<MeshFilter>().sharedMesh;
    }
}
