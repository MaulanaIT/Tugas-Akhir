using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    public static InventoryController 
        inventoryController;

    [System.Serializable]
    public class SetPanel {
        public GameObject
            buttonInventoryItem,
            buttonInventoryTool,
            buttonInventorySeed,
            panelInventoryItem,
            panelInventoryTool,
            panelInventorySeed;
    }
    public SetPanel setPanel = new SetPanel();

    public bool 
        isItem, 
        isTool, 
        isSeed;

    private void Awake() {
        if (inventoryController == null) {
            inventoryController = this;
        } else if (inventoryController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {

    }

    public void ButtonOpenInventoryItemFunction() {
        if (InventoryItemController.inventoryItemController.isInventory == true) {
            isItem = true;
            isTool = false;
            isSeed = false;

            setPanel.panelInventoryItem.SetActive(true);
            setPanel.panelInventoryTool.SetActive(false);
            setPanel.panelInventorySeed.SetActive(false);

            setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(214, 179, 130, 150);
            setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
            setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
        }
    }

    public void ButtonOpenInventoryToolFunction() {
        if (InventoryItemController.inventoryItemController.isInventory == true) {
            isItem = false;
            isTool = true;
            isSeed = false;

            setPanel.panelInventoryItem.SetActive(false);
            setPanel.panelInventoryTool.SetActive(true);
            setPanel.panelInventorySeed.SetActive(false);

            setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
            setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(214, 179, 130, 150);
            setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
        }
    }

    public void ButtonOpenInventorySeedFunction() {
        if (InventoryItemController.inventoryItemController.isInventory == true) {
            isItem = false;
            isTool = false;
            isSeed = true;

            setPanel.panelInventoryItem.SetActive(false);
            setPanel.panelInventoryTool.SetActive(false);
            setPanel.panelInventorySeed.SetActive(true);

            setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
            setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
            setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(214, 179, 130, 150);
        }
    }
}
