using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour {

    public static InventoryItemController
        inventoryItemController;

    public Text 
        title;

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
            slot,
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

    [System.Serializable]
    public class ItemSelected {
        public string 
            name;

        public int 
            count, 
            index;
    }
    public ItemSelected itemSelected = new ItemSelected();

    public bool
        isInventory,
        isAlreadyObtained,
        isSlotChecking;

    private void Awake() {
        if (inventoryItemController == null) {
            inventoryItemController = this;
        } else if (inventoryItemController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        CheckingAllSlot();

        slotItem.slot = new GameObject[slotItem.totalSlot];
        slotItem.image = new GameObject[slotItem.totalSlot];
        slotItem.textCount = new GameObject[slotItem.totalSlot];

        for (int i = 0; i < slotItem.totalSlot; i++) {
            slotItem.slot[i] = moveItem.panelItem.transform.GetChild(i).gameObject;
            slotItem.image[i] = moveItem.panelItem.transform.GetChild(i).GetChild(0).gameObject;
            slotItem.textCount[i] = moveItem.panelItem.transform.GetChild(i).GetChild(1).gameObject;
        }
    }

    void Update() {
        //Awal fungsi merefresh Slot Inventory
        if (isSlotChecking == true) {
            Debug.Log("Checking Slot");

            for (int i = 0; i < currentSlotItem.totalSlot; i++) {
                slotItem.slot[i].SetActive(true);
            }

            for (int i = currentSlotItem.totalSlot; i < slotItem.totalSlot; i++) {
                slotItem.slot[i].SetActive(false);
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
        //Akhir fungsi merefresh Slot Inventory

        //Awal fungsi pindah item
        if (isInventory == true) {
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
    }
    //Akhir fungsi pindah item

    //Awal fungsi menambahkan item ke slot
    public void ButtonAcceptItemFunction() {
        for (int i = 0; i < currentSlotItem.totalSlot; i++) {
            if (itemObtained.name[i] == itemDialogueBox.textItemName.text) {
                isAlreadyObtained = true;
                break;
            } else if (itemObtained.name[i] == null) {
                isAlreadyObtained = false;
            }

            if (i == currentSlotItem.totalSlot - 1 && itemObtained.name[i] != null) {
                Debug.Log("Inventory is full");
            }
        }

        if (isAlreadyObtained == true) {
            for (int i = 0; i < currentSlotItem.totalSlot; i++) {
                if (itemObtained.name[i] == itemDialogueBox.textItemName.text) {
                    itemObtained.count[i] = itemObtained.count[i] + 1;
                    break;
                }
            }
        } else {
            for (int i = 0; i < currentSlotItem.totalSlot; i++) {
                if (itemObtained.name[i] == null) {
                    itemObtained.name[i] = itemDialogueBox.textItemName.text;
                    itemObtained.count[i] = itemObtained.count[i] + 1;
                    break;
                }
            }
        }

        isAlreadyObtained = false;

        CheckingAllSlot();

        itemDialogueBox.gameObject.SetActive(false);
    }
    //Akhir fungsi menambahkan item ke slot

    //Awal fungsi membatalkan menambah item ke slot
    public void ButtonIgnoreItemFunction() {
        itemDialogueBox.gameObject.SetActive(false);
    }
    //Akhir fungsi membatalkan menambah item ke slot

    //Awal fungsi kontrol item slot
    public void ButtonControlItemFunction(GameObject Item) {
        if (moveItem.isMoveItem == false) {
            for (int i = 0; i < slotItem.slot.Length; i++) {
                InventoryController.inventoryController.setPanel.panelInventoryItem.transform.GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
            }

            Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

            for (int i = 0; i < slotItem.slot.Length; i++) {
                if (Item == slotItem.slot[i]) {
                    itemSelected.name = itemObtained.name[i];
                    itemSelected.count = itemObtained.count[i];
                    itemSelected.index = i;

                    for (int j = 0; j < listItem.name.Length; j++) {
                        if (itemObtained.name[i] == listItem.name[j]) {
                            itemDescription.image.color = new Color(255, 255, 255, 255);
                            itemDescription.image.sprite = listItem.image[j];
                            itemDescription.name.text = listItem.name[j];
                            itemDescription.description.text = listItem.description[j];
                        }
                    }
                }
            }
        } else {
            Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

            for (int i = 0; i < slotItem.slot.Length; i++) {
                if (Item == slotItem.slot[i]) {
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

                        slotItem.slot[moveItem.moveFromIndex].GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                        slotItem.slot[moveItem.moveToIndex].GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                        break;
                    }
                }
            }
        }
    }
    //Akhir fungsi kontrol item slot

    //Awal fungsi menutup otomatis pemberitahuan item
    public void AutoCloseItemDialogueFunction() {
        if (itemDialogueBox.gameObject.activeSelf == true) {
            itemDialogueBox.countDown -= Time.deltaTime;

            if (itemDialogueBox.countDown <= 0) {
                itemDialogueBox.gameObject.SetActive(false);
                itemDialogueBox.countDown = 5f;
            }
        }
    }
    //Akhir fungsi menutup otomatis pemberitahuan item

    //Awal fungsi mengaktifkan fitur pindah slot item
    public void ButtonMoveItemFunction() {
        moveItem.isMoveItem = true;
    }
    //Akhir fungsi mengaktifkan fitur pindah slot item

    //Awal fungsi pengecekan semua slot
    public void CheckingAllSlot() {
        InventorySeedsController.inventorySeedsController.isChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        isSlotChecking = true;
        SelectSeedsController.selectSeedsController.slotSeeds.slotChecking = true;
    }
    //Akhir fungsi pengecekan semua slot
}
