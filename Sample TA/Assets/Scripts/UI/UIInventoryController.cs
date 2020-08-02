using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour {

    public GameObject
        panelInventory;

    public bool
        isActive;

    void Start() {
        
    }

    void Update() {
        
    }

    public void ButtonInventoryFunction() {
        if (isActive == false) {
            panelInventory.SetActive(true);

            isActive = true;
        } else if (isActive == true) {
            panelInventory.SetActive(false);

            isActive = false;
        }
    }
}
