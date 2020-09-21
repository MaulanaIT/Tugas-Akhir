using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShopItemAmmount : MonoBehaviour {

    [System.Serializable]
    public class CountItem {
        public int
            itemAmmount;

        public Text
            textAmmount;
    }
    public CountItem countItem = new CountItem();

    public float cooldown = 1f;

    public bool
        isBoundIncrease,
        isBoundDecrease;

    void Start() {
        countItem.textAmmount.text = "" + countItem.itemAmmount;
    }

    private void OnDisable() {
        countItem.itemAmmount = 0;
    }

    void Update() {
        if (isBoundIncrease == true) {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0) {
                GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

                countItem.itemAmmount++;
                cooldown = 0.1f;
            }
        }

        if (isBoundDecrease == true) {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0) {
                GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

                countItem.itemAmmount--;
                cooldown = 0.1f;
            }
        }

        CountItemFunction();
    }

    public void CountItemFunction() {
        if (ShopController.shopController.isBuy == true) {
            countItem.itemAmmount = 0;
        }

        countItem.textAmmount.text = "" + countItem.itemAmmount;
    }

    public void ButtonDownIncreaseNumberFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

        countItem.itemAmmount++;
        isBoundIncrease = true;
    }

    public void ButtonUpIncreaseNumberFunction() {
        isBoundIncrease = false;
        cooldown = 1f;
    }

    public void ButtonDownDecreaseNumberFunction() {
        GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

        if (countItem.itemAmmount > 0) {
            countItem.itemAmmount--;
            isBoundDecrease = true;
        }
    }

    public void ButtonUpDecreaseNumberFunction() {
        isBoundDecrease = false;
        cooldown = 1f;
    }
}
