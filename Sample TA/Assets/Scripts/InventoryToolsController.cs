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
            toolsName;

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
                    }
                }
            }

            for (int i = slotTools.currentToolsSlot; i < slotTools.maxToolsSlot; i++) {
                slotTools.toolsSlot[i].SetActive(false);
            }

            isChecking = false;
        }
    }

    public void CheckingAllSlot() {
        InventorySeedsController.inventorySeedsController.isChecking = true;
        isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
}
