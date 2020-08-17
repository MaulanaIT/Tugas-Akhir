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
        InventoryItemController.inventoryItemController.title.text = "Inventory";
        panelInventory.SetActive(true);
        panelItemDescription.SetActive(true);

        InventoryItemController.inventoryItemController.isInventory = true;
    }

    public void ButtonCloseInventoryFunction() {
        panelInventory.SetActive(false);
        panelItemDescription.SetActive(false);

        InventoryItemController.inventoryItemController.isInventory = false;
    }
}
