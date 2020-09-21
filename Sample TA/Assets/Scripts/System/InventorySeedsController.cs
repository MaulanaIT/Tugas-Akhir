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

        public int[] 
            price;
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
        public GameObject
            selectedSeed,
            buttonSelect;

        public string
            name;

        public int
            count, 
            index;
    }
    public SeedsSelected seedsSelected = new SeedsSelected();

    private void Awake() {
        if (inventorySeedsController == null) {
            inventorySeedsController = this;
        } else if (inventorySeedsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        for (int i = 0; i < listSeeds.seedsName.Length; i++) {
            if (seedsSelected.name == listSeeds.seedsName[i]) {
                seedsSelected.selectedSeed.GetComponent<Image>().sprite = listSeeds.seedsImage[i];
            }
        }
    }

    void Update() {
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

        if (GameController.gameController.action.nameSelectedSeeds == null || GameController.gameController.action.nameSelectedSeeds == "") {
            seedsSelected.selectedSeed.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            seedsSelected.selectedSeed.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    //Awal fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        for (int i = 0; i < slotSeeds.slot.Length; i++) {
            InventoryController.inventoryController.setPanel.panelInventorySeed.transform.GetChild(i).GetComponent<Image>().color = new Color32(80, 30, 0, 255);
        }

        Item.GetComponent<Image>().color = new Color32(0, 0, 255, 255);

        for (int i = 0; i < slotSeeds.slot.Length; i++) {
            if (Item == slotSeeds.slot[i]) {
                seedsSelected.name = seedsObtained.name[i];
                seedsSelected.count = seedsObtained.count[i];
                seedsSelected.index = i;

                if (seedsObtained.name[i] == null || seedsObtained.name[i] == "") {
                    seedsDescription.image.color = new Color(255, 255, 255, 0);
                    seedsDescription.image.sprite = null;
                    seedsDescription.name.text = null;
                    seedsDescription.description.text = null;

                    seedsSelected.buttonSelect.SetActive(false);
                    break;
                }

                if (InventoryItemController.inventoryItemController.isInventory == true) {
                    for (int j = 0; j < listSeeds.seedsName.Length; j++) {
                        if (seedsObtained.name[i] == listSeeds.seedsName[j]) {
                            seedsDescription.image.color = new Color(255, 255, 255, 255);
                            seedsDescription.image.sprite = listSeeds.seedsImage[j];
                            seedsDescription.name.text = listSeeds.seedsName[j];
                            seedsDescription.description.text = listSeeds.seedsDescription[j];

                            break;
                        }
                    }
                }
                break;
            }
        }

        if (seedsSelected.name == GameController.gameController.action.nameSelectedSeeds) {
            seedsSelected.buttonSelect.SetActive(false);
        } else {
            seedsSelected.buttonSelect.SetActive(true);
        }
    }
    //Akhir fungsi kontrol item slot

    public void ButtonSelectSeedFunction() {
        GameController.gameController.action.nameSelectedSeeds = seedsSelected.name;
        seedsSelected.buttonSelect.SetActive(false);

        for (int i = 0; i < listSeeds.seedsName.Length; i++) {
            if (seedsSelected.name == listSeeds.seedsName[i]) {
                seedsSelected.selectedSeed.GetComponent<Image>().sprite = listSeeds.seedsImage[i];
            }
        }
    }
}
