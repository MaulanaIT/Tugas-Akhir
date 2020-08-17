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
            panelIContainer;
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
    public class Status {
        public bool
            isItemContainer,
            isToolContainer,
            isSeedContainer;
    }
    public Status status = new Status();

    public int test;

    public bool
        isTouch, 
        isObtained,
        isSlotChecking;

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

        isSlotChecking = true;
    }

    void Update() {
        //Awal fungsi membuka container
        if (isTouch == true && ActionController.actionController.isAction == true && GameController.gameController.action.nameSelectedAction == "Conversations") {
            setPanel.buttonMoveContainer.SetActive(true);
            setPanel.buttonMoveInventory.SetActive(true);

            if (status.isItemContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(true);
                setPanel.panelInventoryTool.SetActive(false);
                setPanel.panelInventorySeed.SetActive(false);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(214, 179, 130, 150);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(74, 56, 31, 255);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Barang";
            }

            if (status.isToolContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(false);
                setPanel.panelInventoryTool.SetActive(true);
                setPanel.panelInventorySeed.SetActive(false);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(214, 179, 130, 150);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(74, 56, 31, 255);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Peralatan";
            }

            if (status.isSeedContainer == true) {
                setPanel.panelInventory.SetActive(true);
                setPanel.panelInventoryItem.SetActive(false);
                setPanel.panelInventoryTool.SetActive(false);
                setPanel.panelInventorySeed.SetActive(true);

                setPanel.panelIContainer.SetActive(true);

                setPanel.buttonInventoryItem.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
                setPanel.buttonInventoryTool.GetComponent<Image>().color = new Color32(74, 56, 31, 255);
                setPanel.buttonInventorySeed.GetComponent<Image>().color = new Color32(214, 179, 130, 150);

                InventoryItemController.inventoryItemController.title.text = "Penyimpanan Benih";
            }
        }
        //Akhir fungsi membuka container

        //Awal fungsi pengecekan slot
        if (isSlotChecking == true) {

            if (status.isItemContainer == true) {
                for (int i = 0; i < itemContainer.totalSlot; i++) {
                    for (int j = 0; j < InventoryItemController.inventoryItemController.listItem.name.Length; j++) {
                        if (itemContainer.name[i] == InventoryItemController.inventoryItemController.listItem.name[j]) {
                            itemSlot.image[i].GetComponent<Image>().sprite = InventoryItemController.inventoryItemController.listItem.image[j];
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                            break;
                        } else {
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                        }
                    }
                }

                for (int i = 0; i < itemContainer.name.Length; i++) {
                    if (itemContainer.count[i] > 1) {
                        itemSlot.textCount[i].GetComponent<Text>().text = "" + itemContainer.count[i];
                    } else {
                        itemSlot.textCount[i].GetComponent<Text>().text = "";
                    }
                }
            }

            if (status.isToolContainer == true) {
                for (int i = 0; i < toolContainer.totalSlot; i++) {
                    for (int j = 0; j < InventoryToolsController.inventoryToolsController.listTools.toolsName.Length; j++) {
                        if (toolContainer.name[i] == InventoryToolsController.inventoryToolsController.listTools.toolsName[j]) {
                            itemSlot.image[i].GetComponent<Image>().sprite = InventoryToolsController.inventoryToolsController.listTools.toolsImage[j];
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                            break;
                        } else {
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                        }
                    }
                }

                for (int i = 0; i < toolContainer.name.Length; i++) {
                    if (toolContainer.count[i] > 1) {
                        itemSlot.textCount[i].GetComponent<Text>().text = "" + toolContainer.count[i];
                    } else {
                        itemSlot.textCount[i].GetComponent<Text>().text = "";
                    }
                }
            }

            if (status.isSeedContainer == true) {
                for (int i = 0; i < seedContainer.totalSlot; i++) {
                    for (int j = 0; j < InventorySeedsController.inventorySeedsController.listSeeds.seedsName.Length; j++) {
                        if (seedContainer.name[i] == InventorySeedsController.inventorySeedsController.listSeeds.seedsName[j]) {
                            itemSlot.image[i].GetComponent<Image>().sprite = InventorySeedsController.inventorySeedsController.listSeeds.seedsImage[j];
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                            break;
                        } else {
                            itemSlot.image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                        }
                    }
                }

                for (int i = 0; i < seedContainer.name.Length; i++) {
                    if (seedContainer.count[i] > 1) {
                        itemSlot.textCount[i].GetComponent<Text>().text = "" + seedContainer.count[i];
                    } else {
                        itemSlot.textCount[i].GetComponent<Text>().text = "";
                    }
                }
            }

            isSlotChecking = false;
        }
        //Akhir fungsi pengecekan slot
    }

    //Awal fungsi memindah item
    public void ButtonMoveToContainer() {
        if (status.isItemContainer == true && InventoryItemController.inventoryItemController.itemSelected.name != null) {
            for (int i = 0; i < itemContainer.totalSlot; i++) {
                if (itemContainer.name[i] == InventoryItemController.inventoryItemController.itemSelected.name) {
                    isObtained = true;
                    break;
                } else if (itemContainer.name[i] == null) {
                    isObtained = false;
                }

                if (i == itemContainer.totalSlot - 1 && itemContainer.name[i] != null) {
                    Debug.Log("Slot is full");
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
                    if (itemContainer.name[i] == null) {
                        itemContainer.name[i] = InventoryItemController.inventoryItemController.itemSelected.name;
                        itemContainer.count[i] = itemContainer.count[i] + InventoryItemController.inventoryItemController.itemSelected.count;
                        break;
                    }
                }
            }
            InventoryItemController.inventoryItemController.itemSelected.name = null;
            InventoryItemController.inventoryItemController.itemSelected.count = 0;
            InventoryItemController.inventoryItemController.itemObtained.name[InventoryItemController.inventoryItemController.itemSelected.index] = "";
            InventoryItemController.inventoryItemController.itemObtained.count[InventoryItemController.inventoryItemController.itemSelected.index] = 0;
        }

        if (status.isToolContainer == true && InventoryToolsController.inventoryToolsController.toolsSelected.name != null) {
            for (int i = 0; i < toolContainer.totalSlot; i++) {
                if (toolContainer.name[i] == InventoryToolsController.inventoryToolsController.toolsSelected.name) {
                    isObtained = true;
                    break;
                } else if (toolContainer.name[i] == null) {
                    isObtained = false;
                }

                if (i == toolContainer.totalSlot - 1 && toolContainer.name[i] != null) {
                    Debug.Log("Slot is full");
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
                    if (toolContainer.name[i] == null) {
                        toolContainer.name[i] = InventoryToolsController.inventoryToolsController.toolsSelected.name;
                        toolContainer.count[i] = toolContainer.count[i] + 1;
                        break;
                    }
                }
            }
            InventoryToolsController.inventoryToolsController.toolsSelected.name = null;
            InventoryToolsController.inventoryToolsController.toolsObtained.name[InventoryToolsController.inventoryToolsController.toolsSelected.index] = "";
        }

        if (status.isSeedContainer == true && InventorySeedsController.inventorySeedsController.seedsSelected.name != null) {
            for (int i = 0; i < seedContainer.totalSlot; i++) {
                if (seedContainer.name[i] == InventorySeedsController.inventorySeedsController.seedsSelected.name) {
                    isObtained = true;
                    break;
                } else if (seedContainer.name[i] == null) {
                    isObtained = false;
                }

                if (i == seedContainer.totalSlot - 1 && seedContainer.name[i] != null) {
                    Debug.Log("Slot is full");
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
                    if (seedContainer.name[i] == null) {
                        seedContainer.name[i] = InventorySeedsController.inventorySeedsController.seedsSelected.name;
                        seedContainer.count[i] = seedContainer.count[i] + InventorySeedsController.inventorySeedsController.seedsSelected.count;
                        break;
                    }
                }
            }
            InventorySeedsController.inventorySeedsController.seedsSelected.name = null;
            InventorySeedsController.inventorySeedsController.seedsSelected.count = 0;
            InventorySeedsController.inventorySeedsController.seedsObtained.name[InventorySeedsController.inventorySeedsController.seedsSelected.index] = "";
            InventorySeedsController.inventorySeedsController.seedsObtained.count[InventorySeedsController.inventorySeedsController.seedsSelected.index] = 0;
        }
        isObtained = false;
        isSlotChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
        InventoryToolsController.inventoryToolsController.isChecking = true;
        InventorySeedsController.inventorySeedsController.isChecking = true;
    }

    public void ButtonMoveToInventory() {
        if (status.isItemContainer == true) {

        }

        if (status.isToolContainer == true) {

        }

        if (status.isSeedContainer == true) {

        }

        isSlotChecking = true;
        InventoryItemController.inventoryItemController.isSlotChecking = true;
    }

    //Awal fungsi menutup container
    public void ButtonCloseContainerFunction() {
        if (status.isItemContainer == true || status.isToolContainer == true || status.isSeedContainer == true) {
            setPanel.panelInventory.SetActive(false);
            setPanel.panelInventoryItem.SetActive(true);
            setPanel.panelInventoryTool.SetActive(false);
            setPanel.panelInventorySeed.SetActive(false);
            setPanel.panelIContainer.SetActive(false);
            setPanel.buttonMoveContainer.SetActive(false);
            setPanel.buttonMoveInventory.SetActive(false);
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
