using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour {

    public GameObject
        panelInventory;

    void Start() {
        
    }

    void Update() {
        
    }

    public void ButtonInventoryFunction() {
        if (InventoryItemController.inventoryItemController.isInventory == false) {
            InventoryItemController.inventoryItemController.title.text = "Inventory";
            panelInventory.SetActive(true);

            InventoryItemController.inventoryItemController.isInventory = true;
        } else if (InventoryItemController.inventoryItemController.isInventory == true) {
            panelInventory.SetActive(false);

            InventoryItemController.inventoryItemController.isInventory = false;
        }
    }
}
