using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContainerController : MonoBehaviour {

    [System.Serializable]
    public class SetPanel {
        public GameObject
            buttonMoveContainer, 
            buttonMoveInventory,
            buttonInventoryItem,
            buttonInventoryTool,
            buttonInventorySeed,
            panelInventoryItem,
            panelInventoryTool,
            panelInventorySeed,
            panelInventory, 
            panelDescription,
            panelIContainer, 
            textNameContainer;
    }
    public SetPanel setPanel = new SetPanel();

    [System.Serializable]
    public class ItemSlot {
        public int 
            totalSlot;

        public GameObject[] 
            slot, 
            image, 
            textCount;
    }
    public ItemSlot itemSlot = new ItemSlot();

    [System.Serializable]
    public class ItemContainer {
        public int 
            totalSlot;

        public string[] 
            name;

        public int[] 
            count;
    }
    public ItemContainer itemContainer = new ItemContainer();

    [System.Serializable]
    public class ToolContainer {
        public int
            totalSlot;

        public string[]
            name;

        public int[]
            count;

    }
    public ToolContainer toolContainer = new ToolContainer();

    [System.Serializable]
    public class SeedContainer {
        public int
            totalSlot;

        public string[]
            name;

        public int[]
            count;

    }
    public SeedContainer seedContainer = new SeedContainer();

    [System.Serializable]
    public class SlotSelected {
        public string 
            name;

        public int
            count, 
            index;
    }
    public SlotSelected slotSelected = new SlotSelected();

    [System.Serializable]
    public class Status {
        public bool
            isItemContainer,
            isToolContainer,
            isSeedContainer;
    }
    public Status status = new Status();

    public bool
        isTouch,
        isObtained;

    void Start() {
        //Awal fungsi set total size array
        itemSlot.slot = new GameObject[itemSlot.totalSlot];
        itemSlot.image = new GameObject[itemSlot.totalSlot];
        itemSlot.textCount = new GameObject[itemSlot.totalSlot];

        for (int i = 0; i < itemSlot.totalSlot; i++) {
            itemSlot.slot[i] = setPanel.panelIContainer.transform.GetChild(0).GetChild(i).gameObject;
            itemSlot.image[i] = setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetChild(0).gameObject;
            itemSlot.textCount[i] = setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetChild(1).gameObject;
        }

        if (status.isItemContainer == true) {
            for (int i = 0; i < itemSlot.totalSlot; i++) {
                if (i < itemContainer.totalSlot) {
                    itemSlot.slot[i].SetActive(true);
                } else {
                    itemSlot.slot[i].SetActive(false);
                }
            }
        }

        if (status.isToolContainer == true) {
            for (int i = 0; i < itemSlot.totalSlot; i++) {
                if (i < toolContainer.totalSlot) {
                    itemSlot.slot[i].SetActive(true);
                } else {
                    itemSlot.slot[i].SetActive(false);
                }
            }
        }

        if (status.isSeedContainer == true) {
            for (int i = 0; i < itemSlot.totalSlot; i++) {
                if (i < seedContainer.totalSlot) {
                    itemSlot.slot[i].SetActive(true);
                } else {
                    itemSlot.slot[i].SetActive(false);
                }
            }
        }


        itemContainer.name = new string[itemContainer.totalSlot];
        itemContainer.count = new int[itemContainer.totalSlot];

        toolContainer.name = new string[toolContainer.totalSlot];
        toolContainer.count = new int[toolContainer.totalSlot];

        seedContainer.name = new string[seedContainer.totalSlot];
        seedContainer.count = new int[seedContainer.totalSlot];
        //Akhir fungsi set total size array
    }

    void Update() {
        if (isTouch == true) {
            setPanel.textNameContainer.SetActive(true);
        } else {
            setPanel.textNameContainer.SetActive(false);
        }

        setPanel.textNameContainer.transform.LookAt(Camera.main.transform.position);

        //Awal fungsi membuka container
        if (isTouch == true && ActionController.actionController.isAction == true && GameController.gameController.action.nameSelectedAction == "Interact") {
            setPanel.buttonMoveContainer.SetActive(true);
            setPanel.buttonMoveInventory.SetActive(true);
            setPanel.panelDescription.SetActive(false);

            if (status.isItemContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(true);
                setPanel.panelInventoryTool.SetActive(false);
                setPanel.panelInventorySeed.SetActive(false);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(0, 0, 150, 150);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(0, 0, 50, 150);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(0, 0, 50, 150);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Barang";
            }

            if (status.isToolContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(false);
                setPanel.panelInventoryTool.SetActive(true);
                setPanel.panelInventorySeed.SetActive(false);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(0, 0, 50, 150);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(0, 0, 150, 150);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(0, 0, 50, 150);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Peralatan";
            }

            if (status.isSeedContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(false);
                setPanel.panelInventoryTool.SetActive(false);
                setPanel.panelInventorySeed.SetActive(true);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(0, 0, 50, 150);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(0, 0, 50, 150);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(0, 0, 150, 150);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Benih";
            }
        }
        //Akhir fungsi membuka container

        //Awal fungsi pengecekan slot
        if (status.isItemContainer == true && isTouch == true) {
            for (int i = 0; i < itemContainer.totalSlot; i++) {
                for (int j = 0; j < InventoryItemController.inventoryItemController.listItem.name.Length; j++) {
                    if (itemContainer.name[i] == InventoryItemController.inventoryItemController.listItem.name[j]) {
                        itemSlot.image[i].GetComponent<Image>().sprite = InventoryItemController.inventoryItemController.listItem.image[j];
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 150);
                        break;
                    } else {
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }

                if (itemContainer.count[i] > 1) {
                    itemSlot.textCount[i].GetComponent<Text>().text = "" + itemContainer.count[i];
                } else {
                    itemSlot.textCount[i].GetComponent<Text>().text = "";
                }

                if (itemContainer.name[i] == null || itemContainer.name[i] == "") {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }
            }
        }

        if (status.isToolContainer == true && isTouch == true) {
            for (int i = 0; i < toolContainer.totalSlot; i++) {
                for (int j = 0; j < InventoryToolsController.inventoryToolsController.listTools.toolsName.Length; j++) {
                    if (toolContainer.name[i] == InventoryToolsController.inventoryToolsController.listTools.toolsName[j]) {
                        itemSlot.image[i].GetComponent<Image>().sprite = InventoryToolsController.inventoryToolsController.listTools.toolsImage[j];
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 150);
                        break;
                    } else {
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }

                if (toolContainer.count[i] > 1) {
                    itemSlot.textCount[i].GetComponent<Text>().text = "" + toolContainer.count[i];
                } else {
                    itemSlot.textCount[i].GetComponent<Text>().text = "";
                }

                if (toolContainer.name[i] == null || toolContainer.name[i] == "") {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }
            }
        }

        if (status.isSeedContainer == true && isTouch == true) {
            for (int i = 0; i < seedContainer.totalSlot; i++) {
                for (int j = 0; j < InventorySeedsController.inventorySeedsController.listSeeds.seedsName.Length; j++) {
                    if (seedContainer.name[i] == InventorySeedsController.inventorySeedsController.listSeeds.seedsName[j]) {
                        itemSlot.image[i].GetComponent<Image>().sprite = InventorySeedsController.inventorySeedsController.listSeeds.seedsImage[j];
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 150);
                        break;
                    } else {
                        itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                }

                if (seedContainer.count[i] > 1) {
                    itemSlot.textCount[i].GetComponent<Text>().text = "" + seedContainer.count[i];
                } else {
                    itemSlot.textCount[i].GetComponent<Text>().text = "";
                }

                if (seedContainer.name[i] == null || seedContainer.name[i] == "") {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }
            }
        }

        //AutoArrangeFunction();
        //Akhir fungsi pengecekan slot
    }

    //Awal fungsi pengurutan otomatis
    public void AutoArrangeFunction() {
        if (isTouch == true) {
            if (status.isItemContainer == true) {
                for (int i = 1; i < itemContainer.totalSlot; i++) {
                    if (i > 0) {
                        if (itemContainer.name[i - 1] == null && itemContainer.name[i - 1] == "") {
                            itemContainer.name[i - 1] = itemContainer.name[i];
                            itemContainer.count[i - 1] = itemContainer.count[i];

                            itemContainer.name[i] = null;
                            itemContainer.count[i] = 0;
                            break;
                        }
                    }
                }
            }

            if (status.isToolContainer == true) {
                for (int i = 0; i < toolContainer.totalSlot; i++) {
                    if (i > 0) {
                        if (toolContainer.name[i - 1] == null && toolContainer.name[i - 1] == "") {
                            toolContainer.name[i - 1] = toolContainer.name[i];
                            toolContainer.count[i - 1] = toolContainer.count[i];

                            toolContainer.name[i] = null;
                            toolContainer.count[i] = 0;
                            break;
                        }
                    }
                }
            }

            if (status.isSeedContainer == true) {
                for (int i = 0; i < seedContainer.totalSlot; i++) {
                    if (i > 0) {
                        if (seedContainer.name[i - 1] == null && seedContainer.name[i - 1] == "") {
                            seedContainer.name[i - 1] = seedContainer.name[i];
                            seedContainer.count[i - 1] = seedContainer.count[i];

                            seedContainer.name[i] = null;
                            seedContainer.count[i] = 0;
                            break;
                        }
                    }
                }
            }
        }
    }
    //Akhir fungsi pengurutan otomatis

    //Awal fungsi kontrol seleksi slot
    public void ButtonControlItemFunction(GameObject Item) {
        if (isTouch == true) {
            if (status.isItemContainer == true) {
                for (int i = 0; i < itemContainer.totalSlot; i++) {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }

                Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

                for (int i = 0; i < itemContainer.totalSlot; i++) {
                    if (Item == itemSlot.slot[i]) {
                        slotSelected.name = itemContainer.name[i];
                        slotSelected.count = itemContainer.count[i];
                        slotSelected.index = i;
                        break;
                    }
                }
            }

            if (status.isToolContainer == true) {
                for (int i = 0; i < toolContainer.totalSlot; i++) {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }

                Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

                for (int i = 0; i < toolContainer.totalSlot; i++) {
                    if (Item == itemSlot.slot[i]) {
                        slotSelected.name = toolContainer.name[i];
                        slotSelected.count = toolContainer.count[i];
                        slotSelected.index = i;
                        break;
                    }
                }
            }

            if (status.isSeedContainer == true) {
                for (int i = 0; i < seedContainer.totalSlot; i++) {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                }

                Item.GetComponent<Image>().color = new Color32(0, 0, 255, 150);

                for (int i = 0; i < seedContainer.totalSlot; i++) {
                    if (Item == itemSlot.slot[i]) {
                        slotSelected.name = seedContainer.name[i];
                        slotSelected.count = seedContainer.count[i];
                        slotSelected.index = i;
                        break;
                    }
                }
            }
        }
    }
    //Akhir fungsi kontrol seleksi slot

    //Awal fungsi memindah item
    public void ButtonMoveToContainer() {
        if (isTouch == true) {
            if (status.isItemContainer == true && InventoryItemController.inventoryItemController.itemSelected.name != null && InventoryItemController.inventoryItemController.itemSelected.name != "") {
                for (int i = 0; i < itemContainer.totalSlot; i++) {
                    if (itemContainer.name[i] == InventoryItemController.inventoryItemController.itemSelected.name) {
                        isObtained = true;
                        break;
                    } else if (itemContainer.name[i] == null || itemContainer.name[i] == "") {
                        isObtained = false;
                    }
                }

                if (isObtained == true) {
                    for (int i = 0; i < itemContainer.totalSlot; i++) {
                        if (itemContainer.name[i] == InventoryItemController.inventoryItemController.itemSelected.name) {
                            itemContainer.count[i] = itemContainer.count[i] + InventoryItemController.inventoryItemController.itemSelected.count;
                            break;
                        }
                    }
                }

                if (isObtained == false) {
                    for (int i = 0; i < itemContainer.totalSlot; i++) {
                        if (itemContainer.name[i] == null || itemContainer.name[i] == "") {
                            itemContainer.name[i] = InventoryItemController.inventoryItemController.itemSelected.name;
                            itemContainer.count[i] = itemContainer.count[i] + InventoryItemController.inventoryItemController.itemSelected.count;
                            break;
                        }
                    }
                }
                InventoryItemController.inventoryItemController.itemSelected.name = null;
                InventoryItemController.inventoryItemController.itemSelected.count = 0;
                InventoryItemController.inventoryItemController.itemObtained.name[InventoryItemController.inventoryItemController.itemSelected.index] = null;
                InventoryItemController.inventoryItemController.itemObtained.count[InventoryItemController.inventoryItemController.itemSelected.index] = 0;
            }

            if (status.isToolContainer == true && InventoryToolsController.inventoryToolsController.toolsSelected.name != null && InventoryToolsController.inventoryToolsController.toolsSelected.name != "") {
                for (int i = 0; i < toolContainer.totalSlot; i++) {
                    if (toolContainer.name[i] == InventoryToolsController.inventoryToolsController.toolsSelected.name) {
                        isObtained = true;
                        break;
                    } else if (toolContainer.name[i] == null || toolContainer.name[i] == "") {
                        isObtained = false;
                    }
                }

                if (isObtained == true) {
                    for (int i = 0; i < toolContainer.totalSlot; i++) {
                        if (toolContainer.name[i] == InventoryToolsController.inventoryToolsController.toolsSelected.name) {
                            toolContainer.count[i] = toolContainer.count[i] + 1;
                            break;
                        }
                    }
                }

                if (isObtained == false) {
                    for (int i = 0; i < toolContainer.totalSlot; i++) {
                        if (toolContainer.name[i] == null || toolContainer.name[i] == "") {
                            toolContainer.name[i] = InventoryToolsController.inventoryToolsController.toolsSelected.name;
                            toolContainer.count[i] = toolContainer.count[i] + 1;
                            break;
                        }
                    }
                }
                InventoryToolsController.inventoryToolsController.toolsSelected.name = null;
                InventoryToolsController.inventoryToolsController.toolsObtained.name[InventoryToolsController.inventoryToolsController.toolsSelected.index] = null;
            }

            if (status.isSeedContainer == true && InventorySeedsController.inventorySeedsController.seedsSelected.name != null && InventorySeedsController.inventorySeedsController.seedsSelected.name != "") {
                for (int i = 0; i < seedContainer.totalSlot; i++) {
                    if (seedContainer.name[i] == InventorySeedsController.inventorySeedsController.seedsSelected.name) {
                        isObtained = true;
                        break;
                    } else if (seedContainer.name[i] == null || seedContainer.name[i] == "") {
                        isObtained = false;
                    }
                }

                if (isObtained == true) {
                    for (int i = 0; i < seedContainer.totalSlot; i++) {
                        if (seedContainer.name[i] == InventorySeedsController.inventorySeedsController.seedsSelected.name) {
                            seedContainer.count[i] = seedContainer.count[i] + InventorySeedsController.inventorySeedsController.seedsSelected.count;
                            break;
                        }
                    }
                }

                if (isObtained == false) {
                    for (int i = 0; i < seedContainer.totalSlot; i++) {
                        if (seedContainer.name[i] == null || seedContainer.name[i] == "") {
                            seedContainer.name[i] = InventorySeedsController.inventorySeedsController.seedsSelected.name;
                            seedContainer.count[i] = seedContainer.count[i] + InventorySeedsController.inventorySeedsController.seedsSelected.count;
                            break;
                        }
                    }
                }
                InventorySeedsController.inventorySeedsController.seedsSelected.name = null;
                InventorySeedsController.inventorySeedsController.seedsSelected.count = 0;
                InventorySeedsController.inventorySeedsController.seedsObtained.name[InventorySeedsController.inventorySeedsController.seedsSelected.index] = null;
                InventorySeedsController.inventorySeedsController.seedsObtained.count[InventorySeedsController.inventorySeedsController.seedsSelected.index] = 0;
            }

            isObtained = false;
        }
    }

    public void ButtonMoveToInventory() {
        if (isTouch == true) {
            if (status.isItemContainer == true && slotSelected.name != null && slotSelected.name != "") {
                for (int i = 0; i < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; i++) {
                    if (slotSelected.name == InventoryItemController.inventoryItemController.itemObtained.name[i]) {
                        isObtained = true;
                        break;
                    } else if (InventoryItemController.inventoryItemController.itemObtained.name[i] == null) {
                        isObtained = false;
                    }

                    if (i == InventoryItemController.inventoryItemController.currentSlotItem.totalSlot - 1 && InventoryItemController.inventoryItemController.itemObtained.name[i] != null) {
                        Debug.Log("Slot is full");
                    }
                }

                if (isObtained == true) {
                    for (int i = 0; i < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; i++) {
                        if (slotSelected.name == InventoryItemController.inventoryItemController.itemObtained.name[i]) {
                            InventoryItemController.inventoryItemController.itemObtained.count[i] = InventoryItemController.inventoryItemController.itemObtained.count[i] + slotSelected.count;
                            break;
                        }
                    }
                }

                if (isObtained == false) {
                    for (int i = 0; i < InventoryItemController.inventoryItemController.currentSlotItem.totalSlot; i++) {
                        if (InventoryItemController.inventoryItemController.itemObtained.name[i] == null) {
                            InventoryItemController.inventoryItemController.itemObtained.name[i] = slotSelected.name;
                            InventoryItemController.inventoryItemController.itemObtained.count[i] = InventoryItemController.inventoryItemController.itemObtained.count[i] + slotSelected.count;
                            break;
                        }
                    }
                }

                setPanel.panelIContainer.transform.GetChild(0).GetChild(slotSelected.index).GetComponent<Image>().color = new Color32(0, 0, 0, 150);

                itemContainer.name[slotSelected.index] = null;
                itemContainer.count[slotSelected.index] = 0;

                slotSelected.name = itemContainer.name[slotSelected.index];
                slotSelected.count = itemContainer.count[slotSelected.index];
            }

            if (status.isToolContainer == true && slotSelected.name != null && slotSelected.name != "") {
                for (int i = 0; i < InventoryToolsController.inventoryToolsController.slotTools.currentToolsSlot; i++) {
                    if (InventoryToolsController.inventoryToolsController.toolsObtained.name[i] == null) {
                        InventoryToolsController.inventoryToolsController.toolsObtained.name[i] = slotSelected.name;
                        break;
                    }

                    if (i == InventoryToolsController.inventoryToolsController.slotTools.currentToolsSlot - 1 && InventoryToolsController.inventoryToolsController.toolsObtained.name[i] != null) {
                        Debug.Log("Slot is full");
                    }
                }

                if (toolContainer.count[slotSelected.index] <= 1) {
                    setPanel.panelIContainer.transform.GetChild(0).GetChild(slotSelected.index).GetComponent<Image>().color = new Color32(0, 0, 0, 150);

                    toolContainer.name[slotSelected.index] = null;
                    toolContainer.count[slotSelected.index] = 0;
                } else {
                    toolContainer.count[slotSelected.index] = toolContainer.count[slotSelected.index] - 1;
                }

                slotSelected.name = toolContainer.name[slotSelected.index];
                slotSelected.count = toolContainer.count[slotSelected.index];
            }

            if (status.isSeedContainer == true && slotSelected.name != null && slotSelected.name != "") {
                for (int i = 0; i < InventorySeedsController.inventorySeedsController.slotSeeds.currentSeedsSlot; i++) {
                    if (slotSelected.name == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                        isObtained = true;
                        break;
                    } else if (InventorySeedsController.inventorySeedsController.seedsObtained.name[i] == null) {
                        isObtained = false;
                    }

                    if (i == InventorySeedsController.inventorySeedsController.slotSeeds.currentSeedsSlot - 1 && InventorySeedsController.inventorySeedsController.seedsObtained.name[i] != null) {
                        Debug.Log("Slot is full");
                    }
                }

                if (isObtained == true) {
                    for (int i = 0; i < InventorySeedsController.inventorySeedsController.slotSeeds.currentSeedsSlot; i++) {
                        if (slotSelected.name == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                            InventorySeedsController.inventorySeedsController.seedsObtained.count[i] = InventorySeedsController.inventorySeedsController.seedsObtained.count[i] + slotSelected.count;
                            break;
                        }
                    }
                }

                if (isObtained == false) {
                    for (int i = 0; i < InventorySeedsController.inventorySeedsController.slotSeeds.currentSeedsSlot; i++) {
                        if (InventorySeedsController.inventorySeedsController.seedsObtained.name[i] == null) {
                            InventorySeedsController.inventorySeedsController.seedsObtained.name[i] = slotSelected.name;
                            InventorySeedsController.inventorySeedsController.seedsObtained.count[i] = InventorySeedsController.inventorySeedsController.seedsObtained.count[i] + slotSelected.count;
                            break;
                        }
                    }
                }

                setPanel.panelIContainer.transform.GetChild(0).GetChild(slotSelected.index).GetComponent<Image>().color = new Color32(0, 0, 0, 150);

                seedContainer.name[slotSelected.index] = null;
                seedContainer.count[slotSelected.index] = 0;

                slotSelected.name = seedContainer.name[slotSelected.index];
                slotSelected.count = seedContainer.count[slotSelected.index];
            }

            isObtained = false;
        }
    }
    //Akhir fungsi memindah item

    //Awal fungsi menutup container
    public void ButtonCloseContainerFunction() {
        if (isTouch == true) {
            if (status.isItemContainer == true || status.isToolContainer == true || status.isSeedContainer == true) {
                setPanel.panelInventory.SetActive(false);
                setPanel.panelInventoryItem.SetActive(false);
                setPanel.panelInventoryTool.SetActive(false);
                setPanel.panelInventorySeed.SetActive(false);
                setPanel.panelIContainer.SetActive(false);
                setPanel.buttonMoveContainer.SetActive(false);
                setPanel.buttonMoveInventory.SetActive(false);

                SaveController.saveController.isSaveInventory = true;
            }
        }
    }
    //Akhir fungsi menutup container

    //Awal fungsi objek tersentuh
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (ActionController.actionController.pickUp.isTouchItem == false) {
                isTouch = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            isTouch = false;
        }
    }
    //Akhir fungsi objek tersentuh
}
