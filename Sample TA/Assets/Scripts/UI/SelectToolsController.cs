using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectToolsController : MonoBehaviour {

    public Image
        selectedTools;

    public bool 
        isActive;

    void Start() {

    }

    void Update() {
        
    }

    public void buttonSelectFunction(string nameSelectedTools) {
        for (int i = 0; i < 4; i++) {
            if (nameSelectedTools == InventoryToolsController.inventoryToolsController.toolsObtained.name[i]) {
                selectedTools.gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;

                GameController.gameController.action.nameSelectedTools = nameSelectedTools;

                isActive = true;
                break;
            } else {
                isActive = false;
            }
        }

        if (isActive == false) {
            GameController.gameController.notice.cooldown = 2f;
            GameController.gameController.notice.isPressed = true;

            GameController.gameController.notice.notice.SetActive(true);

            GameController.gameController.notice.textNotice.text = "Perlengkapan Tidak Tersedia";
        }
    }
}
