using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour {
    
    public static ProfileController 
        profileController;

    public GameObject 
        panelProfle;

    public Text 
        textName, 
        textMoney;

    public string
        name;

    public int 
        money;

    public bool 
        isProfile;

    private void Awake() {
        if (profileController == null) {
            profileController = this;
        } else if (profileController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        name = PlayerPrefs.GetString("PlayerName");

        textName.text = name;
    }

    void Update() {
        textMoney.text = money.ToString();
    }

    public void ButtonProfileFunction() {
        GameController.gameController.audio.audioButton.Stop();
        GameController.gameController.audio.audioButton.clip = GameController.gameController.audio.audioButtonSelect;
        GameController.gameController.audio.audioButton.Play();

        if (isProfile == false) {
            panelProfle.SetActive(true);
            isProfile = true;
        } else {
            panelProfle.SetActive(false);
            isProfile = false;
        }
    }
}
