using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AditionalNPCController : MonoBehaviour {

    public CameraLocationController 
        cameraLocationController;

    [System.Serializable]
    public class Conversation {
        public GameObject
            panelConversation, 
            nameNPC;

        public Text
            textNameNPC,
            textWordsNPC,
            textNextConversation;

        public string
            stringConversationName;

        public string[]
            stringConversationWords;

        public int
            currentConversationIndex = 0,
            shopPanelConversationIndex;
    }
    public Conversation conversation = new Conversation();

    [System.Serializable]
    public class Shop {
        public GameObject
            panelShop,
            panelSell,
            panelListitem,
            panelItemShop,
            panelToolShop, 
            panelSeedShop;

        public GameObject[] 
            panelSeedShopLocation;

        public bool
            isShop, 
            isItemShop, 
            isToolShop, 
            isSeedShop;
    }
    public Shop shop = new Shop();

    public int 
        indexLocation;

    public bool
        isTouch;

    void Start() {
        cameraLocationController = GetComponent<CameraLocationController>();

        indexLocation = PlayerPrefs.GetInt("IndexLocation");
    }

    void Update() {
        //Kondisi apabila isTouch = true
        if (cameraLocationController.conversationFunction.isTouch == true) {
            isTouch = true;
            conversation.textNameNPC.text = conversation.stringConversationName;
            conversation.textWordsNPC.text = conversation.stringConversationWords[conversation.currentConversationIndex];

            if (conversation.currentConversationIndex >= conversation.stringConversationWords.Length - 1) {
                conversation.textNextConversation.text = "Close";
            } else {
                conversation.textNextConversation.text = "Next";
            }
        } else {
            isTouch = false;
        }

        if (isTouch == true) {
            conversation.nameNPC.SetActive(true);
        } else {
            conversation.nameNPC.SetActive(false);
        }

        conversation.nameNPC.transform.LookAt(Camera.main.transform.position);

        if (isTouch == true && GameController.gameController.action.nameSelectedAction == "Interact" && ActionController.actionController.isAction == true) {
            ActionController.actionController.conversationFunction.isConversation = true;
            conversation.panelConversation.SetActive(true);
        } else if (ActionController.actionController.conversationFunction.isConversation == false) {
            conversation.panelConversation.SetActive(false);
        }
    }

    //Fungsi button melanjutkan percakapan
    public void ButtonNextConversationFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

        if (isTouch == true && shop.isShop == false) {
            if (conversation.currentConversationIndex >= conversation.stringConversationWords.Length - 1) {
                conversation.panelConversation.SetActive(false);
                conversation.currentConversationIndex = 0;

                ActionController.actionController.conversationFunction.isConversation = false;
            } else {
                conversation.currentConversationIndex++;
            }
        }

        if (isTouch == true && shop.isShop == true) {
            if (conversation.currentConversationIndex >= conversation.stringConversationWords.Length - 1) {
                conversation.panelConversation.SetActive(false);
                conversation.currentConversationIndex = 0;

                ActionController.actionController.conversationFunction.isConversation = false;
            } else {
                conversation.currentConversationIndex++;
            }

            if (conversation.currentConversationIndex == conversation.shopPanelConversationIndex) {
                ShopController.shopController.isShop = true;

                shop.panelShop.SetActive(true);
                shop.panelListitem.SetActive(true);

                if (shop.isItemShop == true) {
                    ShopController.shopController.listSellItem.listItem = new GameObject[InventoryItemController.inventoryItemController.listItem.name.Length];
                    ShopController.shopController.isShopItem = true;
                    ShopController.shopController.isShopTool = false;
                    ShopController.shopController.isShopSeed = false;

                    shop.panelListitem.GetComponent<ScrollRect>().content = shop.panelItemShop.GetComponent<RectTransform>();

                    shop.panelSell.SetActive(true);
                    shop.panelItemShop.SetActive(true);
                    shop.panelToolShop.SetActive(false);
                    shop.panelSeedShop.SetActive(false);

                    for (int i = 0; i < shop.panelSeedShopLocation.Length; i++) {
                        shop.panelSeedShopLocation[i].SetActive(false);
                    }
                } else if (shop.isToolShop == true) {
                    //ShopController.shopController.listSellItem.listItem = new GameObject[InventoryToolsController.inventoryToolsController.listTools.toolsName.Length];
                    ShopController.shopController.isShopItem = false;
                    ShopController.shopController.isShopTool = true;
                    ShopController.shopController.isShopSeed = false;

                    shop.panelListitem.GetComponent<ScrollRect>().content = shop.panelToolShop.GetComponent<RectTransform>();

                    shop.panelSell.SetActive(false);
                    shop.panelItemShop.SetActive(false);
                    shop.panelToolShop.SetActive(true);
                    shop.panelSeedShop.SetActive(false);

                    for (int i = 0; i < shop.panelSeedShopLocation.Length; i++) {
                        shop.panelSeedShopLocation[i].SetActive(false);
                    }
                } else if (shop.isSeedShop == true) {
                    ShopController.shopController.listSellItem.listItem = new GameObject[InventorySeedsController.inventorySeedsController.listSeeds.seedsName.Length];
                    ShopController.shopController.isShopItem = false;
                    ShopController.shopController.isShopTool = false;
                    ShopController.shopController.isShopSeed = true;

                    if (GameController.gameController.location == "Jawa") {
                        shop.panelListitem.GetComponent<ScrollRect>().content = shop.panelSeedShop.transform.GetChild(0).GetComponent<RectTransform>();
                    } else if (GameController.gameController.location == "Sumatera") {
                        shop.panelListitem.GetComponent<ScrollRect>().content = shop.panelSeedShop.transform.GetChild(1).GetComponent<RectTransform>();
                    }

                    shop.panelSell.SetActive(true);
                    shop.panelItemShop.SetActive(false);
                    shop.panelToolShop.SetActive(false);
                    shop.panelSeedShop.SetActive(true);

                    for (int i = 0; i < shop.panelSeedShopLocation.Length; i++) {
                        if (i == indexLocation) {
                            shop.panelSeedShopLocation[i].SetActive(true);
                        } else {
                            shop.panelSeedShopLocation[i].SetActive(false);
                        }
                    }
                }
            } else {
                for (int i = 0; i < ShopController.shopController.listSellItem.listItem.Length; i++) {
                    Destroy(ShopController.shopController.listSellItem.listItem[i]);
                }
                ShopController.shopController.listSellItem.listItem = new GameObject[0];
                ShopController.shopController.isShop = false;
                shop.panelShop.SetActive(false);
                shop.panelSell.SetActive(false);
                shop.panelListitem.SetActive(false);
            }
        }
    }
}
