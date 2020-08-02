using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToolsController : MonoBehaviour {

    public static InventoryToolsController
        inventoryToolsController;

    [System.Serializable]
    public class ListTools {
        public string[]
            toolsName;

        public Sprite[]
            toolsImage;
    }
    public ListTools listTools = new ListTools();

    public string[] 
        currentToolsName;

    public int
        currentToolsSlot,
        maxToolsSlot;

    public GameObject[]
        toolsSlot,
        toolsSlotImage;

    public bool
        isChecking;

    private void Awake() {
        if (inventoryToolsController == null) {
            inventoryToolsController = this;
        } else if (inventoryToolsController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {
        if (isChecking == true) {
            for (int i = 0; i < currentToolsSlot; i++) {
                toolsSlot[i].SetActive(true);

                toolsSlotImage[i].GetComponent<Image>().sprite = listTools.toolsImage[i];
            }

            for (int i = currentToolsSlot; i < maxToolsSlot; i++) {
                toolsSlot[i].SetActive(false);
            }

            isChecking = false;
        }
    }
}
