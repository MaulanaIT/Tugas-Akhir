using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour {

    public static QuestController 
        questController;

    public GameObject 
        panelQuest;

    public Text[]
        textQuestName,
        textQuestProgress,
        textQuestGoal;

    [System.Serializable]
    public class Quest {

        public string[] 
            questName, 
            questItem;

        public float[]
            questProgress, 
            questGoal;

    }
    public Quest[] quest;

    public int indexLocation;

    public bool 
        isQuest;

    private void Awake() {
        if (questController == null) {
            questController = this;
        } else if (questController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        indexLocation = PlayerPrefs.GetInt("IndexLocation");
    }

    void Update() {
        for (int i = 0; i < textQuestName.Length; i++) {
            textQuestName[i].text = quest[indexLocation].questName[i];
            textQuestProgress[i].text = Convert.ToString(quest[indexLocation].questProgress[i]);
            textQuestGoal[i].text = Convert.ToString(quest[indexLocation].questGoal[i]);
        }
    }

    public void ButtonShowListQuest() {
        GameController.gameController.audio.audioButton.Stop();
        GameController.gameController.audio.audioButton.clip = GameController.gameController.audio.audioButtonSelect;
        GameController.gameController.audio.audioButton.Play();

        if (isQuest == false) {
            panelQuest.SetActive(true);

            isQuest = true;
        } else {
            panelQuest.SetActive(false);

            isQuest = false;
        }
    }
}
