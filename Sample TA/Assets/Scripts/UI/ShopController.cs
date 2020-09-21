using System.Collections;
using System.Collections.Generic;
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

    [System.Serializable]
    public class ListSellItem {
        public GameObject[] 
            listItem,
            textItemName,
            textItemPrice,
            textItemNumber;

        public GameObject
            panelSellItem,
            prefabItem;

        public string[]
            itemName;

        public int[]
            itemPrice,
            itemNumber;
    }
    public ListSellItem listSellItem = new ListSellItem();

    public GameObject 
        buttonBuy;

    public bool
        isAlreadyObtained,
        isBound,
        isBuy,
        isSell,
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
            //Awal fungsi mengatur list item toko
            listShopItem.textItemName = GameObject.FindGameObjectsWithTag("ShopItemName");
            listShopItem.textItemPrice = GameObject.FindGameObjectsWithTag("ShopItemPrice");
            listShopItem.textItemNumber = GameObject.FindGameObjectsWithTag("ShopItemNumber");

            listShopItem.itemName = new string[listShopItem.textItemName.Length];
            listShopItem.itemPrice = new int[listShopItem.textItemPrice.Length];
            listShopItem.itemNumber = new int[listShopItem.textItemNumber.Length];

            for (int i = 0; i < listShopItem.itemName.Length; i++) {
                listShopItem.itemName[i] = listShopItem.textItemName[i].GetComponent<Text>().text;
                listShopItem.itemPrice[i] = int.Parse(listShopItem.textItemPrice[i].GetComponent<Text>().text);
                if (isShopTool == false) {
                    listShopItem.itemNumber[i] = int.Parse(listShopItem.textItemNumber[i].GetComponent<Text>().text);
                }
            }

            if (isShopTool == true) {
                buttonBuy.SetActive(false);
            } else {
                buttonBuy.SetActive(true);
            }
            //Akhir fungsi mengatur list item toko

            //Awal fungsi mengatur list item jual
            listSellItem.textItemName = GameObject.FindGameObjectsWithTag("SellItemName");
            listSellItem.textItemPrice = GameObject.FindGameObjectsWithTag("SellItemPrice");
            listSellItem.textItemNumber = GameObject.FindGameObjectsWithTag("SellItemNumber");

            listSellItem.itemName = new string[listSellItem.textItemName.Length];
            listSellItem.itemPrice = new int[listSellItem.textItemPrice.Length];
            listSellItem.itemNumber = new int[listSellItem.textItemNumber.Length];

            for (int i = 0; i < listSellItem.itemName.Length; i++) {
                listSellItem.itemName[i] = listSellItem.textItemName[i].GetComponent<Text>().text;
                listSellItem.itemPrice[i] = int.Parse(listSellItem.textItemPrice[i].GetComponent<Text>().text);
                if (isShopTool == false) {
                    listSellItem.itemNumber[i] = int.Parse(listSellItem.textItemNumber[i].GetComponent<Text>().text);
                }
            }
            if (isShopItem == true) {
                for (int i = 0; i < InventoryItemController.inventoryItemController.itemObtained.name.Length; i++) {
                    if (InventoryItemController.inventoryItemController.itemObtained.name[i] != null && InventoryItemController.inventoryItemController.itemObtained.name[i] != "") {
                        if (listSellItem.listItem[i] == null) {
                            GameObject item = Instantiate(listSellItem.prefabItem);

                            listSellItem.listItem[i] = item;

                            listSellItem.listItem[i].transform.SetParent(listSellItem.panelSellItem.transform);

                            listSellItem.listItem[i].GetComponent<SellItemController>().name = InventoryItemController.inventoryItemController.itemObtained.name[i];
                        }
                    }
                }
            } else if (isShopSeed == true) {
                for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
                    if (InventorySeedsController.inventorySeedsController.seedsObtained.name[i] != null && InventorySeedsController.inventorySeedsController.seedsObtained.name[i] != "") {
                        if (listSellItem.listItem[i] == null) {
                            GameObject item = Instantiate(listSellItem.prefabItem);

                            listSellItem.listItem[i] = item;

                            listSellItem.listItem[i].transform.SetParent(listSellItem.panelSellItem.transform);

                            listSellItem.listItem[i].GetComponent<SellItemController>().name = InventorySeedsController.inventorySeedsController.seedsObtained.name[i];
                            listSellItem.listItem[i].GetComponent<SellItemController>().total = InventorySeedsController.inventorySeedsController.seedsObtained.count[i];

                            for (int j = 0; j < InventorySeedsController.inventorySeedsController.listSeeds.seedsName.Length; j++) {
                                if (InventorySeedsController.inventorySeedsController.seedsObtained.name[i] == InventorySeedsController.inventorySeedsController.listSeeds.seedsName[j]) {
                                    listSellItem.listItem[i].GetComponent<SellItemController>().price = InventorySeedsController.inventorySeedsController.listSeeds.price[j];
                                }
                            }
                        }
                    }
                }
            }
            //Akhir fungsi mengatur list item jual
        } else {
            listShopItem.itemName = new string[0];
            listShopItem.itemNumber = new int[0];
            listShopItem.itemPrice = new int[0];
            listShopItem.textItemName = new GameObject[0];
            listShopItem.textItemNumber = new GameObject[0];
            listShopItem.textItemPrice = new GameObject[0];
        }
    }

    public void ButtonBuyToolFunction(GameObject button) {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

        for (int i = 0; i < button.transform.parent.childCount; i++) {
            for (int j = 0; j < listShopItem.textItemPrice.Length; j++) {
                if (button.transform.parent.GetChild(i).tag == "ShopItemPrice") {
                    if (button.transform.parent.GetChild(i) == listShopItem.textItemPrice[j].transform) {
                        if (ProfileController.profileController.money >= listShopItem.itemPrice[j]) {
                            ProfileController.profileController.money -= listShopItem.itemPrice[j];
                        } else {
                            Debug.Log("Money is not enough");
                        }
                        break;
                    }
                }
            }
        }
    }

    public void ButtonDownBuyFunction() {
        if (isBuy == false && isBound == false) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

            isBound = true;

            totalPrice = 0;

            for (int i = 0; i < listShopItem.itemName.Length; i++) {
                totalPrice += listShopItem.itemNumber[i] * listShopItem.itemPrice[i];
            }

            if (ProfileController.profileController.money >= totalPrice) {
                isBuy = true;

                for (int i = 0; i < listShopItem.itemName.Length; i++) {
                    if (listShopItem.itemNumber[i] > 0) {
                        if (isShopItem == true) {
                            for (int j = 0; j < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; j++) {
                                if (InventoryItemController.inventoryItemController.itemObtained.name[j] == "" || InventoryItemController.inventoryItemController.itemObtained.name[j] == null) {
                                    isAlreadyObtained = false;
                                } else if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listShopItem.itemName[i]) {
                                    isAlreadyObtained = true;
                                    break;
                                }

                                if (i == InventoryItemController.inventoryItemController.currentSlotItem.totalSlot - 1 && InventoryItemController.inventoryItemController.itemObtained.name[j] != "" && InventoryItemController.inventoryItemController.itemObtained.name[j] != null) {
                                    Debug.Log("Inventory is full");
                                }
                            }

                            if (isAlreadyObtained == false) {
                                for (int j = 0; j < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; j++) {
                                    if (InventoryItemController.inventoryItemController.itemObtained.name[j] == "" || InventoryItemController.inventoryItemController.itemObtained.name[j] == null) {
                                        InventoryItemController.inventoryItemController.itemObtained.count[j] = listShopItem.itemNumber[i];
                                        InventoryItemController.inventoryItemController.itemObtained.name[j] = listShopItem.itemName[i];
                                        break;
                                    }
                                }
                            } else {
                                for (int j = 0; j < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; j++) {
                                    if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listShopItem.itemName[i]) {
                                        InventoryItemController.inventoryItemController.itemObtained.count[j] += listShopItem.itemNumber[i];
                                        break;
                                    }
                                }
                            }
                        }

                        if (isShopTool == true) {
                            for (int j = 0; j < InventoryToolsController.inventoryToolsController.slotTools.maxToolsSlot; j++) {
                                if (InventoryToolsController.inventoryToolsController.toolsObtained.name[j] == "" || InventoryToolsController.inventoryToolsController.toolsObtained.name[j] == null) {
                                    InventoryToolsController.inventoryToolsController.toolsObtained.name[j] = listShopItem.itemName[i];
                                    break;
                                } else if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listShopItem.itemName[i]) {
                                    Debug.Log("Tool already obtained");
                                    break;
                                }

                                if (i == InventoryToolsController.inventoryToolsController.slotTools.maxToolsSlot - 1 && InventoryToolsController.inventoryToolsController.toolsObtained.name[j] != "" && InventoryToolsController.inventoryToolsController.toolsObtained.name[j] != null) {
                                    Debug.Log("Inventory is full");
                                }
                            }
                        }

                        if (isShopSeed == true) {
                            for (int j = 0; j < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; j++) {
                                if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == "" || InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == null) {
                                    isAlreadyObtained = false;
                                } else if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == listShopItem.itemName[i]) {
                                    isAlreadyObtained = true;
                                    break;
                                }

                                if (i == InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot - 1 && InventorySeedsController.inventorySeedsController.seedsObtained.name[j] != "" && InventorySeedsController.inventorySeedsController.seedsObtained.name[j] != null) {
                                    Debug.Log("Inventory is full");
                                }
                            }

                            if (isAlreadyObtained == false) {
                                for (int j = 0; j < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; j++) {
                                    if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == "" || InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == null) {
                                        InventorySeedsController.inventorySeedsController.seedsObtained.count[j] = listShopItem.itemNumber[i];
                                        InventorySeedsController.inventorySeedsController.seedsObtained.name[j] = listShopItem.itemName[i];
                                        break;
                                    }
                                }
                            } else {
                                for (int j = 0; j < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; j++) {
                                    if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == listShopItem.itemName[i]) {
                                        InventorySeedsController.inventorySeedsController.seedsObtained.count[j] += listShopItem.itemNumber[i];
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                ProfileController.profileController.money -= totalPrice;
            } else {
                Debug.Log("Money is not enough");
            }
        }
    }

    public void ButtonUpBuyFunction() {
        if (isBound == true) {
            isBound = false;
            isBuy = false;
        }
    }

    public void ButtonDownSellFunction() {
        if (isSell == false && isBound == false) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

            isBound = true;
            isSell = true;

            totalPrice = 0;

            for (int i = 0; i < listSellItem.itemName.Length; i++) {
                totalPrice += listSellItem.itemNumber[i] * listSellItem.itemPrice[i];
            }
            if (totalPrice > 0) {
                for (int i = 0; i < listSellItem.itemName.Length; i++) {
                    if (listSellItem.itemNumber[i] > 0) {
                        if (isShopItem == true) {
                            for (int j = 0; j < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; j++) {
                                if (InventoryItemController.inventoryItemController.itemObtained.name[j] == listSellItem.itemName[i]) {
                                    InventoryItemController.inventoryItemController.itemObtained.count[j] -= listShopItem.itemNumber[i];

                                    if (InventoryItemController.inventoryItemController.itemObtained.count[j] <= 0) {
                                        InventoryItemController.inventoryItemController.itemObtained.name[j] = null;
                                        Destroy(listSellItem.listItem[i]);
                                    }
                                    break;
                                }
                            }
                        }

                        if (isShopSeed == true) {
                            for (int j = 0; j < InventorySeedsController.inventorySeedsController.slotSeeds.maxSeedsSlot; j++) {
                                if (InventorySeedsController.inventorySeedsController.seedsObtained.name[j] == listSellItem.itemName[i]) {
                                    InventorySeedsController.inventorySeedsController.seedsObtained.count[j] -= listSellItem.itemNumber[i];

                                    if (InventorySeedsController.inventorySeedsController.seedsObtained.count[j] <= 0) {
                                        InventorySeedsController.inventorySeedsController.seedsObtained.name[j] = null;
                                        Destroy(listSellItem.listItem[i]);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                ProfileController.profileController.money += totalPrice;
            }
        }
    }

    public void ButtonUpSellFunction() {
        if (isBound == true) {
            isBound = false;
            isSell = false;
        }
    }
}
