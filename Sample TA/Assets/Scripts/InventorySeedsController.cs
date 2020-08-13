using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySeedsController : MonoBehaviour {

    public static InventorySeedsController 
        inventorySeedsController;

    [System.Serializable]
    public class ListSeeds {
        public string[]
            seedsName;

        public Sprite[]
            seedsImage;
    }
    public ListSeeds listSeeds = new ListSeeds();

    [System.Serializable]
    public class SlotSeeds {
        public int
            currentSeedsSlot,
            maxSeedsSlot;

        public GameObject[]
            slot,
            image, 
            textCount;
    }
    public SlotSeeds slotSeeds = new SlotSeeds();

    [System.Serializable]
    public class SeedsObtained {
        public string[] 
            name;

        public int[] 
            count;
    }
    public SeedsObtained seedsObtained = new SeedsObtained();

    public bool
        isChecking;

    private void Awake() {
        if (inventorySeedsController == null) {
            inventorySeedsController = this;
        } else if (inventorySeedsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        CheckingAllSlot();
    }

    void Update() {
        if (isChecking == true) {
            for (int i = 0; i < seedsObtained.name.Length; i++) {
                if (seedsObtained.count[i] > 1) {
                    slotSeeds.textCount[i].GetComponent<Text>().text = "" + seedsObtained.count[i];
                } else {
                    slotSeeds.textCount[i].GetComponent<Text>().text = "";
                }

                for (int j = 0; j < listSeeds.seedsName.Length; j++) {
                    if (seedsObtained.name[i] == listSeeds.seedsName[j]) {
                        slotSeeds.image[i].gameObject.GetComponent<Image>().sprite = listSeeds.seedsImage[j];
                        slotSeeds.image[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        break;
                    } else {
                        slotSeeds.image[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }
            }

            isChecking = false;
        }
    }

    public void CheckingAllSlot() {
        isChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
}
