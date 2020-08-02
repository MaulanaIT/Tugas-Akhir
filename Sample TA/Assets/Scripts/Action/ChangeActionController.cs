using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActionController : MonoBehaviour {

    public GameObject 
        actionChat, 
        actionTools, 
        actionSeeds,
        actionPick, 
        actionHarvest;

    public int 
        isAction = 0, 
        maxAction;

    void Start() {
        
    }

    void Update() {
        switch (isAction) {
            case 0:
                HideGameObject();
                actionChat.SetActive(true);

                GameController.gameController.nameSelectedAction = "Conversations";
                break;
            case 1:
                HideGameObject();
                actionTools.SetActive(true);

                GameController.gameController.nameSelectedAction = "Tools";
                break;
            case 2:
                HideGameObject();
                actionSeeds.SetActive(true);

                GameController.gameController.nameSelectedAction = "Seeds";
                break;
            case 3:
                HideGameObject();
                actionPick.SetActive(true);

                GameController.gameController.nameSelectedAction = "Pick";
                break;
            case 4:
                HideGameObject();
                actionHarvest.SetActive(true);

                GameController.gameController.nameSelectedAction = "Harvest";
                break;
        }
    }

    public void buttonChangeActionFunction() {
        isAction++;

        if (isAction >= maxAction) {
            isAction = 0;
        }
    }

    public void HideGameObject() {
        actionChat.SetActive(false);
        actionTools.SetActive(false);
        actionSeeds.SetActive(false);
        actionPick.SetActive(false);
        actionHarvest.SetActive(false);
    }
}
