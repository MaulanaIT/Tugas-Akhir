using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour {

    public GameObject
        panelInventory, 
        panelItemDescription;

    void Start() {
        
    }

    void Update() {
        
    }

    public void ButtonOpenInventoryFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

        InventoryItemController.inventoryItemController.title.text = "Inventory";
        panelInventory.SetActive(true);
        panelItemDescription.SetActive(true);

        InventoryController.inventoryController.setPanel.panelInventoryItem.SetActive(true);
        InventoryController.inventoryController.setPanel.panelInventoryTool.SetActive(false);
        InventoryController.inventoryController.setPanel.panelInventorySeed.SetActive(false);

        InventoryController.inventoryController.setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        InventoryController.inventoryController.setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        InventoryController.inventoryController.setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        InventoryItemController.inventoryItemController.isInventory = true;
    }

    public void ButtonCloseInventoryFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

        panelInventory.SetActive(false);
        panelItemDescription.SetActive(false);

        InventoryController.inventoryController.setPanel.panelInventoryItem.SetActive(false);
        InventoryController.inventoryController.setPanel.panelInventoryTool.SetActive(false);
        InventoryController.inventoryController.setPanel.panelInventorySeed.SetActive(false);

        InventoryController.inventoryController.setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        InventoryController.inventoryController.setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        InventoryController.inventoryController.setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        InventoryItemController.inventoryItemController.isInventory = false;

        InventorySeedsController.inventorySeedsController.seedsSelected.buttonSelect.SetActive(false);
        InventoryToolsController.inventoryToolsController.toolsSelected.buttonSelect.SetActive(false);

        SaveController.saveController.isSaveInventory = true;
    }
}
