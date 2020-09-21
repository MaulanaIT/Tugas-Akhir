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
    public class ToolsDescription {
        public Image
            image;

        public Text
            name,
            description;
    }
    public ToolsDescription toolsDescription = new ToolsDescription();

    [System.Serializable]
    public class ToolsSelected {
        public GameObject
            selectedTool,
            buttonSelect;

        public string
            name;

        public int 
            index;
    }
    public ToolsSelected toolsSelected = new ToolsSelected();

    private void Awake() {
        if (inventoryToolsController == null) {
            inventoryToolsController = this;
        } else if (inventoryToolsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        for (int i = 0; i < listTools.toolsName.Length; i++) {
            if (toolsSelected.name == listTools.toolsName[i]) {
                toolsSelected.selectedTool.GetComponent<Image>().sprite = listTools.toolsImage[i];
            } else if (toolsSelected.name == null || toolsSelected.name == "") {

            }
        }
    }

    void Update() {
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

        if (GameController.gameController.action.nameSelectedTools == null || GameController.gameController.action.nameSelectedTools == "") {
            toolsSelected.selectedTool.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            toolsSelected.selectedTool.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    //Awal fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        for (int i = 0; i < slotTools.toolsSlot.Length; i++) {
            InventoryController.inventoryController.setPanel.panelInventoryTool.transform.GetChild(i).GetComponent<Image>().color = new Color32(80, 30, 0, 255);
        }

        Item.GetComponent<Image>().color = new Color32(0, 0, 255, 255);

        for (int i = 0; i < slotTools.toolsSlot.Length; i++) {
            if (Item == slotTools.toolsSlot[i]) {
                toolsSelected.name = toolsObtained.name[i];
                toolsSelected.index = i;

                if (toolsObtained.name[i] == null || toolsObtained.name[i] == "") {
                    toolsDescription.image.color = new Color(255, 255, 255, 0);
                    toolsDescription.image.sprite = null;
                    toolsDescription.name.text = null;
                    toolsDescription.description.text = null;

                    toolsSelected.buttonSelect.SetActive(false);
                    break;
                }

                if (InventoryItemController.inventoryItemController.isInventory == true) {
                    for (int j = 0; j < listTools.toolsName.Length; j++) {
                        if (toolsObtained.name[i] == listTools.toolsName[j]) {
                            toolsDescription.image.color = new Color(255, 255, 255, 255);
                            toolsDescription.image.sprite = listTools.toolsImage[j];
                            toolsDescription.name.text = listTools.toolsName[j];
                            toolsDescription.description.text = listTools.toolsDescription[j];
                            break;
                        }
                    }
                }
                break;
            }
        }

        if (toolsSelected.name == GameController.gameController.action.nameSelectedTools) {
            toolsSelected.buttonSelect.SetActive(false);
        } else {
            toolsSelected.buttonSelect.SetActive(true);
        }
    }
    //Akhir fungsi kontrol item slot

    public void ButtonSelectToolFunction() {
        GameController.gameController.action.nameSelectedTools = toolsSelected.name;
        toolsSelected.buttonSelect.SetActive(false);

        for (int i = 0; i < listTools.toolsName.Length; i++) {
            if (toolsSelected.name == listTools.toolsName[i]) {
                toolsSelected.selectedTool.GetComponent<Image>().sprite = listTools.toolsImage[i];
            }
        }
    }
}
