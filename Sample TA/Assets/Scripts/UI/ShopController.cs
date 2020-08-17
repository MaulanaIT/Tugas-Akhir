using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public static ShopController 
        shopController;

    [System.Serializable]
    public class ListShopItem {
        public GameObject[]
            textItemName,
            textItemPrice,
            textItemNumber;

        public string[]
            itemName;

        public int[] 
            itemPrice,
            itemNumber;
    }
    public ListShopItem listShopItem = new ListShopItem();

    public bool
        isBound,
        isBuy,
        isShop, 
        isShopItem, 
        isShopTool, 
        isShopSeed;

    public int 
        totalPrice;

    private void Awake() {
        if (shopController == null) {
            shopController = this;
        } else if (shopController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {
        if (isShop == true) {
            listShopItem.textItemName = GameObject.FindGameObjectsWithTag("ShopItemName");
            listShopItem.textItemPrice = GameObject.FindGameObjectsWithTag("ShopItemPrice");
            listShopItem.textItemNumber = GameObject.FindGameObjectsWithTag("ShopItemNumber");

            listShopItem.itemName = new string[listShopItem.textItemName.Length];
            listShopItem.itemPrice = new int[listShopItem.textItemPrice.Length];
            listShopItem.itemNumber = new int[listShopItem.textItemNumber.Length];

            for (int i = 0; i < listShopItem.itemName.Length; i++) {
                listShopItem.itemName[i] = listShopItem.textItemName[i].GetComponent<Text>().text;
                listShopItem.itemPrice[i] = int.Parse(listShopItem.textItemPrice[i].GetComponent<Text>().text);
                listShopItem.itemNumber[i] = int.Parse(listShopItem.textItemNumber[i].GetComponent<Text>().text);
            }
        } else {
            listShopItem.itemName = new string[0];
            listShopItem.itemNumber = new int[0];
            listShopItem.itemPrice = new int[0];
            listShopItem.textItemName = new GameObject[0];
            listShopItem.textItemNumber = new GameObject[0];
            listShopItem.textItemPrice = new GameObject[0];
        }
    }

    public void ButtonDownBuyFunction() {
        if (isBuy == false && isBound == false) {
            isBuy = true;
            isBound = true;

            totalPrice = 0;

            for (int i = 0; i < listShopItem.itemName.Length; i++) {
                if (listShopItem.itemNumber[i] > 0) {
                    if (isShopItem == true) {
                        for (int j = 0; j < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; j++) {
                            if (InventoryItemController.inventoryItemController.itemObtained.name[j] == "") {
                                InventoryItemController.inventoryItemController.itemObtained.name[j] = listShopItem.itemName[i];
                                InventoryItemController.inventoryItemController.itemObtained.count[j] = listShopItem.itemNumber[i];
                                break;
                            } else if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listShopItem.itemName[i]) {
                                InventoryItemController.inventoryItemController.itemObtained.count[j] += listShopItem.itemNumber[i];
                                break;
                            } else {
                                Debug.Log("Inventory is full");
                            }
                        }
                    }

                    if (isShopTool == true) {
                        for (int j = 0; j < InventoryToolsController.inventoryToolsController.slotTools.maxToolsSlot; j++) {
                            if (InventoryToolsController.inventoryToolsController.toolsObtained.name[j] == "") {
                                InventoryToolsController.inventoryToolsController.toolsObtained.name[j] = listShopItem.itemName[i];
                                break;
                            } else if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listShopItem.itemName[i]) {
                                Debug.Log("Tool already obtained");
                                break;
                            } else {
                                Debug.Log("Inventory is full");
                            }
                        }
                    }

                    if (isShopSeed == true) {
                        for (int j = 0; j < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; j++) {
                            if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == "") {
                                InventorySeedsController.inventorySeedsController.seedsObtained.name[j] = listShopItem.itemName[i];
                                InventorySeedsController.inventorySeedsController.seedsObtained.count[j] = listShopItem.itemNumber[i];
                                break;
                            } else if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == listShopItem.itemName[i]) {
                                InventorySeedsController.inventorySeedsController.seedsObtained.count[j] += listShopItem.itemNumber[i];
                                break;
                            } else {
                                Debug.Log("Inventory is full");
                            }
                        }
                    }
                }

                totalPrice += listShopItem.itemNumber[i] * listShopItem.itemPrice[i];
            }

            CheckingAllSlot();
        }
    }

    public void ButtonUpBuyFunction() {
        if (isBound == true) {
            isBound = false;
            isBuy = false;
        }
    }

    public void CheckingAllSlot() {
        InventorySeedsController.inventorySeedsController.isChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
}
