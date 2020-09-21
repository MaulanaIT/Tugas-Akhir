using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeActionController : MonoBehaviour {

    public Text
        textAction;

    public int 
        isAction = 0, 
        maxAction;

    void Start() {
        
    }

    void Update() {
        switch (isAction) {
            case 0:
                textAction.text = "Interaksi";
                GameController.gameController.action.nameSelectedAction = "Interact";
                break;
            case 1:
                textAction.text = "Alat";
                GameController.gameController.action.nameSelectedAction = "Tools";
                break;
            case 2:
                textAction.text = "Tanam";
                GameController.gameController.action.nameSelectedAction = "Seeds";
                break;
            case 3:
                textAction.text = "Ambil";
                GameController.gameController.action.nameSelectedAction = "Pick";
                break;
            case 4:
                textAction.text = "Panen";
                GameController.gameController.action.nameSelectedAction = "Harvest";
                break;
        }
    }

    public void buttonChangeActionFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonSelect);

        isAction++;

        if (isAction >= maxAction) {
            isAction = 0;
        }
    }
}
