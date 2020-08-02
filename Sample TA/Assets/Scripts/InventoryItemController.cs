using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour {

    public static InventoryItemController
        inventoryItemController;

    [System.Serializable]
    public class ListItem {
        public Sprite[]
            image;

        public string[]
            name, 
            description;
    }
    public ListItem listItem = new ListItem();

    [System.Serializable]
    public class SlotItem {

        public int
            totalSlot;

        public GameObject[]
            gameObject,
            image,
            textCount;
    }
    public SlotItem slotItem = new SlotItem();

    [System.Serializable]
    public class CurrentSlotItem {
        public int
            totalSlot;
    }
    public CurrentSlotItem currentSlotItem = new CurrentSlotItem();

    [System.Serializable]
    public class ItemDialogueBox {
        public GameObject 
            gameObject;

        public Text 
            textItemName;

        public float 
            countDown = 5;
    }
    public ItemDialogueBox itemDialogueBox = new ItemDialogueBox();

    [System.Serializable]
    public class ItemObtained {
        public string[] 
            name;

        public int[]
            count;
    }
    public ItemObtained itemObtained = new ItemObtained();

    [System.Serializable]
    public class MoveItem {
        public GameObject 
            panelInventory, 
            panelItem;

        public int
            moveFromIndex, moveFromCount,
            moveToIndex, moveToCount;

        public string
            moveFromName,
            moveToName;

        public bool
            isMoveItem,
            moveFromChecked,
            moveToChecked;
    }
    public MoveItem moveItem = new MoveItem();

    [System.Serializable]
    public class ItemDescription {
        public Image 
            image;

        public Text 
            name, 
            description;
    }
    public ItemDescription itemDescription = new ItemDescription();

    public bool
        isSlotChecking;

    private void Awake() {
        if (inventoryItemController == null) {
            inventoryItemController = this;
        } else if (inventoryItemController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        isSlotChecking = true;

        slotItem.gameObject = GameObject.FindGameObjectsWithTag("Slot");
        slotItem.image = GameObject.FindGameObjectsWithTag("ItemImage");
        slotItem.textCount = GameObject.FindGameObjectsWithTag("ItemNumber");

        moveItem.panelInventory.SetActive(false);
    }

    void Update() {
        //Merefresh Slot Inventory
        if (isSlotChecking == true) {
            Debug.Log("Checking Slot");

            for (int i = 0; i < currentSlotItem.totalSlot; i++) {
                slotItem.gameObject[i].SetActive(true);
            }

            for (int i = currentSlotItem.totalSlot; i < slotItem.totalSlot; i++) {
                slotItem.gameObject[i].SetActive(false);
            }

            for (int i = 0; i < itemObtained.name.Length; i++) {
                if (itemObtained.count[i] > 1) {
                    slotItem.textCount[i].GetComponent<Text>().text = "" + itemObtained.count[i];
                } else {
                    slotItem.textCount[i].GetComponent<Text>().text = "";
                }

                for (int j = 0; j < listItem.name.Length; j++) {
                    if (itemObtained.name[i] == listItem.name[j]) {
                        slotItem.image[i].gameObject.GetComponent<Image>().sprite = listItem.image[j];
                        slotItem.image[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        break;
                    } else {
                        slotItem.image[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }
            }

            isSlotChecking = false;
        }

        //Fungsi pindah item
        if (moveItem.moveFromChecked == true && moveItem.moveToChecked == true) {
            isSlotChecking = true;

            itemObtained.name[moveItem.moveFromIndex] = moveItem.moveToName;
            itemObtained.count[moveItem.moveFromIndex] = moveItem.moveToCount;

            itemObtained.name[moveItem.moveToIndex] = moveItem.moveFromName;
            itemObtained.count[moveItem.moveToIndex] = moveItem.moveFromCount;

            moveItem.moveFromChecked = false;
            moveItem.moveToChecked = false;
            moveItem.isMoveItem = false;
        }

        if (moveItem.panelInventory.activeSelf == false || moveItem.panelItem.activeSelf == false) {
            moveItem.isMoveItem = false;

            moveItem.moveFromChecked = false;
            moveItem.moveToChecked = false;
        }

        if (moveItem.panelInventory.activeSelf == false) {
            itemDescription.image.color = new Color(255, 255, 255, 0);
            itemDescription.image.sprite = null;
            itemDescription.name.text = null;
            itemDescription.description.text = null;
        }
    }

    //Menambahkan item ke slot
    public void ButtonAcceptItemFunction() {
        for (int i = 0; i < currentSlotItem.totalSlot; i++) {
            if (itemObtained.name[i] == itemDialogueBox.textItemName.text) {
                itemObtained.count[i] = itemObtained.count[i] + 1;
                break;
            }else if (itemObtained.name[i] == "") {
                itemObtained.name[i] = itemDialogueBox.textItemName.text;
                itemObtained.count[i] = itemObtained.count[i] + 1;
                break;
            } else {
                Debug.Log("Inventory is full");
            }
        }

        isSlotChecking = true;

        itemDialogueBox.gameObject.SetActive(false);
    }

    //Membatalkan menambah item ke slot
    public void ButtonIgnoreItemFunction() {
        itemDialogueBox.gameObject.SetActive(false);
    }

    //Fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        if (moveItem.isMoveItem == false) {
            for (int i = 0; i < slotItem.gameObject.Length; i++) {
                if (Item == slotItem.gameObject[i]) {
                    for (int j = 0; j < listItem.name.Length; j++) {
                        if (itemObtained.name[i] == listItem.name[j]) {
                            itemDescription.image.color = new Color(255, 255, 255, 255);
                            itemDescription.image.sprite = listItem.image[j];
                            itemDescription.name.text = listItem.name[j];
                            itemDescription.description.text = listItem.description[j];
                            break;
                        }
                    }
                    break;
                }
            }
        } else {
            for (int i = 0; i < slotItem.gameObject.Length; i++) {
                if (Item == slotItem.gameObject[i]) {
                    if (moveItem.moveFromChecked == false) {
                        moveItem.moveFromIndex = i;
                        moveItem.moveFromName = itemObtained.name[i];
                        moveItem.moveFromCount = itemObtained.count[i];
                        moveItem.moveFromChecked = true;
                        break;
                    } else {
                        moveItem.moveToIndex = i;
                        moveItem.moveToName = itemObtained.name[i];
                        moveItem.moveToCount = itemObtained.count[i];
                        moveItem.moveToChecked = true;
                        break;
                    }
                }
            }
        }
    }

    public void AutoCloseItemDialogueFunction() {
        if (itemDialogueBox.gameObject.activeSelf == true) {
            itemDialogueBox.countDown -= Time.deltaTime;

            if (itemDialogueBox.countDown <= 0) {
                itemDialogueBox.gameObject.SetActive(false);
                itemDialogueBox.countDown = 5f;
            }
        }
    }

    //Fungsi mengaktifkan fitur pindah slot item
    public void ButtonMoveItemFunction() {
        moveItem.isMoveItem = true;
    }
}
