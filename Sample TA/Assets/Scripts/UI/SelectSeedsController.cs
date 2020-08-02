using System.Collections;
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
    public ListSeeds listSeeds = new ListSeeds();

    [System.Serializable]
    public class SlotSeeds {
        public GameObject[]
            slotDisabled;

        public Image[] 
            slotImage;

        public bool[]
            slotStatus;

        public string[] 
            seedsName;

        public bool 
            slotChecking;
    }
    public SlotSeeds slotSeeds = new SlotSeeds();

    [System.Serializable]
    public class SelectSeeds {
        public Image
            selectedSeeds;

        public bool 
            currentSlotStatus;
    }
    public SelectSeeds selectSeeds = new SelectSeeds();

    private void Awake() {
        if (selectSeedsController == null) {
            selectSeedsController = this;
        } else if (selectSeedsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        slotSeeds.slotChecking = true;
    }

    void Update() {
        //Kondisi pengecekan slot
        if (slotSeeds.slotChecking == true) {
            for (int i = 0; i < slotSeeds.slotStatus.Length; i++) {
                if (slotSeeds.slotStatus[i] == false) {
                    slotSeeds.slotDisabled[i].SetActive(true);
                    slotSeeds.slotImage[i].gameObject.SetActive(false);
                    slotSeeds.seedsName[i] = "Disable";
                } else if (slotSeeds.slotStatus[i] == true) {
                    slotSeeds.slotDisabled[i].SetActive(false);
                    slotSeeds.slotImage[i].gameObject.SetActive(true);
                    if (slotSeeds.seedsName[i] == "Disable") {
                        slotSeeds.seedsName[i] = "";
                    }
                }
            }

            SlotSeedsImageFunction();

            slotSeeds.slotChecking = false;
        }
    }

    //Fungsi cek gambar icon benih
    public void SlotSeedsImageFunction() {
        for (int i = 0; i < slotSeeds.seedsName.Length; i++) {
            for (int j = 0; j < listSeeds.name.Length; j++) {
                if (slotSeeds.seedsName[i] == listSeeds.name[j]) {
                    slotSeeds.slotImage[i].GetComponent<Image>().sprite = listSeeds.image[j];
                    break;
                }
            }
        }
    }

    //Fungsi button seleksi slot benih
    public void ButtonSelectFunction(int numberSlot) {
        selectSeeds.currentSlotStatus = slotSeeds.slotStatus[numberSlot - 1];

        if (selectSeeds.currentSlotStatus == true) {
            selectSeeds.selectedSeeds.gameObject.GetComponent<Image>().sprite = slotSeeds.slotImage[numberSlot - 1].sprite;
            GameController.gameController.nameSelectedSeeds = slotSeeds.seedsName[numberSlot - 1];
        } else if (selectSeeds.currentSlotStatus == false) {
            selectSeeds.selectedSeeds.gameObject.GetComponent<Image>().sprite = slotSeeds.slotDisabled[numberSlot - 1].GetComponent<Image>().sprite;
            GameController.gameController.nameSelectedSeeds = "Disable";
        }
    }
}
