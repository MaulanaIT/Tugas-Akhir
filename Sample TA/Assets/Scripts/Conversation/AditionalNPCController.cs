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
            panelConversation;

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
            panelListitem;

        public bool
            isShop;
    }
    public Shop shop = new Shop();

    public bool
        isTouch;

    void Start() {
        cameraLocationController = GetComponent<CameraLocationController>();
    }

    void Update() {
        //Kondisi apabila isTouch = true
        if (cameraLocationController.conversationFunction.isTouch == true) {
            isTouch = true;
            conversation.textNameNPC.text = conversation.stringConversationName;
            conversation.textWordsNPC.text = conversation.stringConversationWords[conversation.currentConversationIndex];

            if (conversation.currentConversationIndex >= conversation.stringConversationWords.Length - 1) {
                conversation.textNextConversation.text = "Close >>>";
            } else {
                conversation.textNextConversation.text = "Next >>>";
            }
        } else {
            isTouch = false;
        }

        if (isTouch == true && ActionController.actionController.conversationFunction.isConversation == true) {
            conversation.panelConversation.SetActive(true);
        } else if (ActionController.actionController.conversationFunction.isConversation == false) {
            conversation.panelConversation.SetActive(false);
        }
    }

    //Fungsi button melanjutkan percakapan
    public void ButtonNextConversationFunction() {
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
            } else {
                ShopController.shopController.isShop = false;
                shop.panelShop.SetActive(false);
                shop.panelListitem.SetActive(false);
            }
        }
    }
}
