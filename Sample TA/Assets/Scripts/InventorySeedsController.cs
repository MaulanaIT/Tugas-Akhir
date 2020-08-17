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
            seedsName, 
            seedsDescription;

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

    [System.Serializable]
    public class SeedsDescription {
        public Image
            image;

        public Text
            name,
            description;
    }
    public SeedsDescription seedsDescription = new SeedsDescription();

    [System.Serializable]
    public class SeedsSelected {
        public string
            name;

        public int
            count, 
            index;
    }
    public SeedsSelected seedsSelected = new SeedsSelected();

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

    //Awal fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        for (int i = 0; i < slotSeeds.slot.Length; i++) {
            InventoryController.inventoryController.setPanel.panelInventorySeed.transform.GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
        }

        Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

        for (int i = 0; i < slotSeeds.slot.Length; i++) {
            if (Item == slotSeeds.slot[i]) {
                seedsSelected.name = seedsObtained.name[i];
                seedsSelected.count = seedsObtained.count[i];
                seedsSelected.index = i;

                for (int j = 0; j < listSeeds.seedsName.Length; j++) {
                    if (seedsObtained.name[i] == listSeeds.seedsName[j]) {
                        seedsDescription.image.color = new Color(255, 255, 255, 255);
                        seedsDescription.image.sprite = listSeeds.seedsImage[j];
                        seedsDescription.name.text = listSeeds.seedsName[j];
                        seedsDescription.description.text = listSeeds.seedsDescription[j];
                    }
                }
            }
        }
    }
    //Akhir fungsi kontrol item slot

    public void CheckingAllSlot() {
        isChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
}
