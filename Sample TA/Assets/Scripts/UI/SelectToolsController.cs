using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectToolsController : MonoBehaviour {

    public Image
        selectedTools;

    void Start() {

    }

    void Update() {
        
    }

    public void buttonSelectFunction(string nameSelectedTools) {
        selectedTools.gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;

        GameController.gameController.nameSelectedTools = nameSelectedTools;
    }
}
