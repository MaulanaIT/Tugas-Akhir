using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatalogueController : MonoBehaviour {

    public static CatalogueController 
        catalogueController;

    public Animator 
        animLocation, 
        animItem, 
        animMission;

    public GameObject 
        locationInformation, 
        itemInformation, 
        missionInformation;

    public GameObject
        itemPrefab, 
        itemParent;

    public GameObject[] 
        item, 
        quest;

    public string 
        locationName;

    public int 
        locationIndex;

    public string[]
        listLocationName, 
        locationDescription;

    [System.Serializable]
    public class ListItem {
        public string[] 
            itemName;
    }
    public ListItem[] listItem;

    [System.Serializable]
    public class ListQuest {
        public string[] 
            questName;
    }
    public ListQuest[] listQuest;

    public bool 
        isLocationShowing, 
        isItemShowing, 
        isMissionShowing, 
        checkList;

    private void Awake() {
        if (catalogueController == null) {
            catalogueController = this;
        } else if (catalogueController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

    }

    void Update() {
        for (int i = 0; i < listLocationName.Length; i++) {
            if (locationName == listLocationName[i]) {
                if (itemInformation.activeSelf == true && checkList == true) {
                    for (int j = 0; j < listItem[i].itemName.Length; j++) {
                        item[j].gameObject.GetComponent<Text>().text = listItem[i].itemName[j];
                    }

                    checkList = false;
                }

                if (missionInformation.activeSelf == true && checkList == true) {
                    for (int j = 0; j < listItem[i].itemName.Length; j++) {
                        quest[j].gameObject.GetComponent<Text>().text = listQuest[i].questName[j];
                    }

                    checkList = false;
                }
            }
        }

        SaveGameFunction();
    }

    public void ButtonLocationInformationFunction() {
        if (locationInformation.activeSelf == false) {
            ResetBooleanInformationFunction();

            isLocationShowing = true;

            locationInformation.SetActive(true);
            itemInformation.SetActive(false);
            missionInformation.SetActive(false);

            animLocation.SetBool("Idle", false);
            animLocation.SetBool("FadeOut", false);
            animLocation.SetBool("FadeIn", true);
        }
    }

    public void ButtonItemInformationFunction() {
        if (itemInformation.activeSelf == false) {
            ResetBooleanInformationFunction();

            isItemShowing = true;
            checkList = true;

            itemInformation.SetActive(true);
            locationInformation.SetActive(false);
            missionInformation.SetActive(false);

            animItem.SetBool("Idle", false);
            animItem.SetBool("FadeOut", false);
            animItem.SetBool("FadeIn", true);
        }
    }

    public void ButtonMissionInformationFunction() {
        if (missionInformation.activeSelf == false) {
            ResetBooleanInformationFunction();

            isMissionShowing = true;
            checkList = true;

            missionInformation.SetActive(true);
            locationInformation.SetActive(false);
            itemInformation.SetActive(false);

            animMission.SetBool("Idle", false);
            animMission.SetBool("FadeOut", false);
            animMission.SetBool("FadeIn", true);
        }
    }

    public void ButtonChoseLocationFunction() {
        SceneManager.LoadScene(1);
    }

    public void ResetBooleanInformationFunction() {
        isLocationShowing = false;
        isItemShowing = false;
        isMissionShowing = false;
    }

    public void SaveGameFunction() {
        PlayerPrefs.SetString("Location", locationName);
        for (int i = 0; i < listLocationName.Length; i++) {
            if (locationName == listLocationName[i]) {
                PlayerPrefs.SetInt("IndexLocation", i);
                break;
            }
        }
    }
}
