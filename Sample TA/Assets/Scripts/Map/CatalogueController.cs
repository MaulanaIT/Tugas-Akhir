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

    public GameObject 
        locationInformation, 
        listPlantInformation, 
        questInformation;

    public GameObject
        itemPrefab, 
        itemParent;

    public GameObject 
        info;

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
                if (listPlantInformation.activeSelf == true && checkList == true) {
                    for (int j = 0; j < listItem[i].itemName.Length; j++) {
                        item[j].gameObject.GetComponent<Text>().text = listItem[i].itemName[j];
                    }

                    checkList = false;

                    break;
                }

                if (questInformation.activeSelf == true && checkList == true) {
                    for (int j = 0; j < listQuest[i].questName.Length; j++) {
                        quest[j].gameObject.GetComponent<Text>().text = listQuest[i].questName[j];
                    }

                    checkList = false;

                    break;
                }

                if (locationInformation.activeSelf == true && checkList == true) {
                    info.gameObject.GetComponent<Text>().text = locationDescription[i];

                    checkList = false;

                    break;
                }
            }
        }

        SaveGameFunction();
    }

    public void ButtonInfoFunction() {
        MapController.mapController.audioButton.Stop();
        MapController.mapController.audioButton.clip = MapController.mapController.audioButtonSelect;
        MapController.mapController.audioButton.Play();

        if (locationInformation.activeSelf == false) {
            locationInformation.SetActive(true);

            listPlantInformation.SetActive(false);
            questInformation.SetActive(false);

            checkList = true;
        } else {
            locationInformation.SetActive(false);
        }
    }

    public void ButtonListPlantFunction() {
        MapController.mapController.audioButton.Stop();
        MapController.mapController.audioButton.clip = MapController.mapController.audioButtonSelect;
        MapController.mapController.audioButton.Play();

        if (listPlantInformation.activeSelf == false) {
            listPlantInformation.SetActive(true);

            locationInformation.SetActive(false);
            questInformation.SetActive(false);

            checkList = true;
        } else {
            listPlantInformation.SetActive(false);
        }
    }

    public void ButtonQuestFunction() {
        MapController.mapController.audioButton.Stop();
        MapController.mapController.audioButton.clip = MapController.mapController.audioButtonSelect;
        MapController.mapController.audioButton.Play();

        if (questInformation.activeSelf == false) {
            questInformation.SetActive(true);

            listPlantInformation.SetActive(false);
            locationInformation.SetActive(false);

            checkList = true;
        } else {
            questInformation.SetActive(false);
        }
    }

    public void ButtonChoseLocationFunction() {
        LoadingController.loadingController.ButtonLoadSceneFunction(2);
        MapController.mapController.GetComponent<AudioSource>().Stop();
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
