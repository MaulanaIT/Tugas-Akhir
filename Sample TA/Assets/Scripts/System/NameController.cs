using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameController : MonoBehaviour {

    public InputField 
        inputName, 
        inputNameFamily;

    void Start() {

    }

    void Update() {
        
    }

    public void ButtonCreateNameFunction() {
        PlayerPrefs.SetString("PlayerName", inputName.text);
        PlayerPrefs.SetString("PlayerFamilyName", inputNameFamily.text);

        SceneManager.LoadScene(2);
    }
}
