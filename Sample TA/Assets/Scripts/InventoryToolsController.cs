using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToolsController : MonoBehaviour {

    public static InventoryToolsController
        inventoryToolsController;

    [System.Serializable]
    public class ListTools {
        public string[]
            toolsName, 
            toolsDescription;

        public Sprite[]
            toolsImage;
    }
    public ListTools listTools = new ListTools();

    [System.Serializable]
    public class SlotTools {
        public int
            currentToolsSlot,
            maxToolsSlot;

        public GameObject[]
            toolsSlot,
            toolsSlotImage;
    }
    public SlotTools slotTools = new SlotTools();

    [System.Serializable]
    public class ToolsObtained {
        public string[] 
            name;
    }
    public ToolsObtained toolsObtained = new ToolsObtained();

    [System.Serializable]
    public class ItemDescription {
        public Image
            image;

        public Text
            name,
            description;
    }
    public ItemDescription itemDescription = new ItemDescription();

    [System.Serializable]
    public class ToolsSelected {
        public string
            name;

        public int 
            index;
    }
    public ToolsSelected toolsSelected = new ToolsSelected();

    public bool
        isChecking;

    private void Awake() {
        if (inventoryToolsController == null) {
            inventoryToolsController = this;
        } else if (inventoryToolsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        CheckingAllSlot();
    }

    void Update() {
        if (isChecking == true) {
            for (int i = 0; i < slotTools.currentToolsSlot; i++) {
                slotTools.toolsSlot[i].SetActive(true);

                for (int j = 0; j < listTools.toolsName.Length; j++) {
                    if (toolsObtained.name[i] == listTools.toolsName[j]) {
                        slotTools.toolsSlotImage[i].GetComponent<Image>().sprite = listTools.toolsImage[i];
                        slotTools.toolsSlotImage[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        break;
                    } else {
                        slotTools.toolsSlotImage[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }
            }

            for (int i = slotTools.currentToolsSlot; i < slotTools.maxToolsSlot; i++) {
                slotTools.toolsSlot[i].SetActive(false);
            }

            isChecking = false;
        }
    }

    //Awal fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        for (int i = 0; i < slotTools.toolsSlot.Length; i++) {
            InventoryController.inventoryController.setPanel.panelInventoryTool.transform.GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
        }

        Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

        for (int i = 0; i < slotTools.toolsSlot.Length; i++) {
            if (Item == slotTools.toolsSlot[i]) {
                toolsSelected.name = toolsObtained.name[i];
                toolsSelected.index = i;

                for (int j = 0; j < listTools.toolsName.Length; j++) {
                    if (toolsObtained.name[i] == listTools.toolsName[j]) {
                        itemDescription.image.color = new Color(255, 255, 255, 255);
                        itemDescription.image.sprite = listTools.toolsImage[j];
                        itemDescription.name.text = listTools.toolsName[j];
                        itemDescription.description.text = listTools.toolsDescription[j];
                    }
                }
            }
        }
    }
    //Akhir fungsi kontrol item slot

    public void CheckingAllSlot() {
        InventorySeedsController.inventorySeedsController.isChecking = true;
        isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
}
