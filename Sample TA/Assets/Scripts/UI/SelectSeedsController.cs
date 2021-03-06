﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSeedsController : MonoBehaviour {

    public static SelectSeedsController 
        selectSeedsController;

    [System.Serializable]
    public class ListSeeds {
        public string[] 
            name, 
            description;

        public Sprite[] 
            image;
    }
    public ListSeeds[] listSeeds;

    //[System.Serializable]
    //public class SlotSeeds {
    //    public GameObject[]
    //        slotDisabled;

    //    public Image[] 
    //        slotImage;

    //    public bool[]
    //        slotStatus;

    //    public string[] 
    //        seedsName;
    //}
    //public SlotSeeds slotSeeds = new SlotSeeds();

    [System.Serializable]
    public class SelectSeeds {
        public Image
            selectedSeeds;

        public bool 
            currentSlotStatus;
    }
    public SelectSeeds selectSeeds = new SelectSeeds();

    public int 
        indexLocation;

    private void Awake() {
        if (selectSeedsController == null) {
            selectSeedsController = this;
        } else if (selectSeedsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        indexLocation = PlayerPrefs.GetInt("IndexLocation");
    }

    void Update() {
        //Kondisi pengecekan slot
        //for (int i = 0; i < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; i++) {
        //    if (i < InventorySeedsController.inventorySeedsController.slotSeeds.currentSeedsSlot) {
        //        slotSeeds.slotStatus[i] = true;

        //        slotSeeds.seedsName[i] = InventorySeedsController.inventorySeedsController.seedsObtained.name[i];
        //    } else {
        //        slotSeeds.slotStatus[i] = false;
        //    }
        //}

        //SlotSeedsImageFunction();
    }

    //Fungsi cek gambar icon benih
    //public void SlotSeedsImageFunction() {
    //    for (int i = 0; i < slotSeeds.seedsName.Length; i++) {
    //        for (int j = 0; j < listSeeds[indexLocation].name.Length; j++) {
    //            if (slotSeeds.seedsName[i] == listSeeds[indexLocation].name[j]) {
    //                slotSeeds.slotImage[i].GetComponent<Image>().sprite = listSeeds[indexLocation].image[j];
    //                break;
    //            }
    //        }
    //    }
    //}

    //Fungsi button seleksi slot benih
    //public void ButtonSelectFunction(int numberSlot) {
    //    selectSeeds.currentSlotStatus = slotSeeds.slotStatus[numberSlot - 1];

    //    if (selectSeeds.currentSlotStatus == true) {
    //        if (slotSeeds.seedsName[numberSlot - 1] == "") {
    //            selectSeeds.selectedSeeds.gameObject.SetActive(false);
    //        } else {
    //            selectSeeds.selectedSeeds.gameObject.SetActive(true);

    //            selectSeeds.selectedSeeds.gameObject.GetComponent<Image>().sprite = slotSeeds.slotImage[numberSlot - 1].sprite;
    //            GameController.gameController.action.nameSelectedSeeds = slotSeeds.seedsName[numberSlot - 1];
    //        }
    //    } else if (selectSeeds.currentSlotStatus == false) {
    //        selectSeeds.selectedSeeds.gameObject.GetComponent<Image>().sprite = slotSeeds.slotDisabled[numberSlot - 1].GetComponent<Image>().sprite;
    //        GameController.gameController.action.nameSelectedSeeds = "Disable";
    //    }
    //}
}
