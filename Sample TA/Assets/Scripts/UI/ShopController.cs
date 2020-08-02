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
        isShop;

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

                totalPrice += listShopItem.itemNumber[i] * listShopItem.itemPrice[i];
            }

            InventoryItemController.inventoryItemController.isSlotChecking = true;
        }
    }

    public void ButtonUpBuyFunction() {
        if (isBound == true) {
            isBound = false;
            isBuy = false;
        }
    }
}
