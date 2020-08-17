using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {

    public static ActionController 
        actionController;

    Animator 
        anim;

    public GameObject 
        prefebDigObject, 
        prefebSeedObject,
        gridObject, 
        actionPoint, 
        playerPoint;

    private float
        xRounded,
        zRounded,
        xPosition,
        zPosition,

        xRange,
        zRange;

    [System.Serializable]
    public class ConversationFunction {
        public GameObject 
            player,
            ui,
            conversation,
            conversationCamera;

        public Vector3
            cameraPosition;

        public float
            cameraRotationX,
            cameraRotationY,
            cameraRotationZ;

        public bool
            isConversation,
            isTouchNPC;
    }
    public ConversationFunction conversationFunction = new ConversationFunction();

    [System.Serializable]
    public class PickUp {
        public GameObject 
            itemDialogueBox;

        public Text 
            textItemName;

        public bool
            isTouchItem;

        public string 
            itemName;
    }
    public PickUp pickUp = new PickUp();

    [System.Serializable]
    public class ActionDuration {
        public GameObject 
            panelDuration;

        public Slider
            sliderDuration;

        public Text 
            nameAction;

        public float 
            duration;
    }
    public ActionDuration actionDuration =  new ActionDuration();

    public bool
        isAction = false,
        isPicking = false,
        isHarvest = false,
        isHarvesting = false, 
        isDigging = false;

    public float 
        cdAction = 0.5f;

    public int 
        indexLocation;

    private void Awake() {
        if (actionController == null) {
            actionController = this;
        } else if (actionController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        playerPoint = GameObject.FindGameObjectWithTag("Player");
        conversationFunction.player = GameObject.FindGameObjectWithTag("Player");

        anim = playerPoint.GetComponent<Animator>();

        indexLocation = PlayerPrefs.GetInt("IndexLocation");
    }

    void Update() {
        if (isAction == true) {
            if (isHarvesting == true) {
                actionDuration.nameAction.text = "Harvesting";
                actionDuration.panelDuration.gameObject.SetActive(true);

                if (actionDuration.sliderDuration.value < actionDuration.sliderDuration.maxValue) {
                    actionDuration.sliderDuration.value += Time.deltaTime / actionDuration.duration;

                    anim.SetBool("Harvesting", true);
                    anim.SetBool("Idle", false);
                } else {
                    actionDuration.panelDuration.gameObject.SetActive(false);

                    pickUp.itemDialogueBox.SetActive(true);

                    pickUp.textItemName.text = pickUp.itemName;
                    pickUp.itemDialogueBox.SetActive(true);

                    actionDuration.sliderDuration.value = 0;

                    anim.SetBool("Harvesting", false);
                    anim.SetBool("Idle", true);

                    isHarvesting = false;
                    isHarvest = false;

                    //Fungsi kondisi memenuhi quest
                    for (int i = 0; i < QuestController.questController.quest[indexLocation].questItem.Length; i++) {
                        if (pickUp.itemName == QuestController.questController.quest[indexLocation].questItem[i]) {
                            QuestController.questController.quest[indexLocation].questProgress[i]++;
                            break;
                        }
                    }
                }
            } else if (isDigging == true) {
                actionDuration.nameAction.text = "Digging";
                actionDuration.panelDuration.gameObject.SetActive(true);

                if (actionDuration.sliderDuration.value < actionDuration.sliderDuration.maxValue) {
                    actionDuration.sliderDuration.value += Time.deltaTime / actionDuration.duration;

                    anim.SetBool("Digging", true);
                    anim.SetBool("Idle", false);
                } else {
                    actionDuration.panelDuration.gameObject.SetActive(false);

                    GameObject obj = Instantiate(prefebDigObject, new Vector3(xPosition, 1.517f, zPosition), Quaternion.identity);

                    actionDuration.sliderDuration.value = 0;

                    anim.SetBool("Digging", false);
                    anim.SetBool("Idle", true);

                    isDigging = false;
                }
            } else if (conversationFunction.isConversation == true) {
                conversationFunction.conversationCamera.SetActive(true);
                conversationFunction.conversation.SetActive(true);
                conversationFunction.player.SetActive(false);
                conversationFunction.ui.SetActive(false);
            } else {
                conversationFunction.conversationCamera.SetActive(false);
                conversationFunction.conversation.SetActive(false);
                conversationFunction.player.SetActive(true);
                conversationFunction.ui.SetActive(true);

                cdAction -= Time.deltaTime;

                if (cdAction <= 0) {
                    cdAction = 0.5f;
                    isAction = false;
                }
            }
        }
    }

    public void ButtonActionFunction() {
        if (isAction == false) {
            if (GameController.gameController.action.nameSelectedAction == "Tools") {
                xRounded = Mathf.Round(actionPoint.transform.position.x);
                zRounded = Mathf.Round(actionPoint.transform.position.z);
                xPosition = actionPoint.transform.position.x;
                zPosition = actionPoint.transform.position.z;

                xRange = gridObject.transform.localScale.x / 2;
                zRange = gridObject.transform.localScale.z / 2;

                if (GameController.gameController.action.nameSelectedTools == "Hoe" && PlayerController.playerController.farm.digStatus == false &&
                    xPosition < gridObject.transform.position.x + xRange && xPosition > gridObject.transform.position.x - xRange &&
                    zPosition < gridObject.transform.position.z + zRange && zPosition > gridObject.transform.position.z - zRange) {

                    if (xPosition >= xRounded) {
                        xPosition = xRounded + 0.5f;
                    }
                    if (xPosition < xRounded) {
                        xPosition = xRounded - 0.5f;
                    }
                    if (zPosition >= zRounded) {
                        zPosition = zRounded + 0.5f;
                    }
                    if (zPosition < zRounded) {
                        zPosition = zRounded - 0.5f;
                    }

                    isDigging = true;
                    isAction = true;
                } else if (GameController.gameController.action.nameSelectedTools == "Sickle") {
                    isAction = true;
                }
            } else if (GameController.gameController.action.nameSelectedAction == "Seeds" && SelectSeedsController.selectSeedsController.selectSeeds.currentSlotStatus == true) {
                xRounded = Mathf.Round(playerPoint.transform.position.x);
                zRounded = Mathf.Round(playerPoint.transform.position.z);
                xPosition = playerPoint.transform.position.x;
                zPosition = playerPoint.transform.position.z;

                xRange = gridObject.transform.localScale.x / 2;
                zRange = gridObject.transform.localScale.z / 2;

                if (PlayerController.playerController.farm.digCurrentStatus == true && PlayerController.playerController.farm.seedStatus == false &&
                    xPosition < gridObject.transform.position.x + xRange && xPosition > gridObject.transform.position.x - xRange &&
                    zPosition < gridObject.transform.position.z + zRange && zPosition > gridObject.transform.position.z - zRange) {

                    if (xPosition >= xRounded) {
                        xPosition = xRounded + 0.5f;
                    }
                    if (xPosition < xRounded) {
                        xPosition = xRounded - 0.5f;
                    }
                    if (zPosition >= zRounded) {
                        zPosition = zRounded + 0.5f;
                    }
                    if (zPosition < zRounded) {
                        zPosition = zRounded - 0.5f;
                    }

                    GameObject obj = Instantiate(prefebSeedObject, new Vector3(xPosition, 1.517f, zPosition), Quaternion.identity);

                    isAction = true;
                }
            } else if (GameController.gameController.action.nameSelectedAction == "Conversations") {

                isAction = true;
            } else if (GameController.gameController.action.nameSelectedAction == "Pick" && pickUp.isTouchItem == true) {
                pickUp.itemDialogueBox.SetActive(true);

                pickUp.textItemName.text = pickUp.itemName;

                isPicking = true;
                isAction = true;
            } else if (GameController.gameController.action.nameSelectedAction == "Harvest" && isHarvest == true) {
                isHarvesting = true;

                isAction = true;
            }
        }
    }
}
