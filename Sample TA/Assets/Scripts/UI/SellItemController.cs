using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemController : MonoBehaviour {

    [System.Serializable]
    public class CountItem {
        public int
            itemAmmount;

        public Text
            textAmmount;
    }
    public CountItem countItem = new CountItem();

    public Text
        textName,
        textPrice;

    public string
        name;

    public int
        price,
        total;

    public float cooldown = 1f;

    public bool
        isBoundIncrease,
        isBoundDecrease;

    void Start() {
        textName.text = name;
        textPrice.text = price.ToString();

        countItem.textAmmount.text = "" + countItem.itemAmmount;
    }

    private void OnDisable() {
        countItem.itemAmmount = 0;
    }

    void Update() {
        if (ShopController.shopController.isShopItem == true) {
            for (int i = 0; i < InventoryItemController.inventoryItemController.itemObtained.name.Length; i++) {
                if (name == InventoryItemController.inventoryItemController.itemObtained.name[i]) {
                    total = InventoryItemController.inventoryItemController.itemObtained.count[i];
                    price = InventoryItemController.inventoryItemController.listItem.price[i];
                    break;
                }
            }
        } else if (ShopController.shopController.isShopSeed == true) {
            for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
                if (name == InventorySeedsController.inventorySeedsController.seedsObtained.name[i]) {
                    total = InventorySeedsController.inventorySeedsController.seedsObtained.count[i];
                    price = InventorySeedsController.inventorySeedsController.listSeeds.price[i];
                    break;
                }
            }
        }
        if (isBoundIncrease == true) {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0 && countItem.itemAmmount < total) {
                GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

                countItem.itemAmmount++;

                cooldown = 0.1f;
            }
        }

        if (isBoundDecrease == true) {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0 && countItem.itemAmmount > 0) {
                GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

                countItem.itemAmmount--;

                cooldown = 0.1f;
            }
        }

        CountItemFunction();
    }

    public void CountItemFunction() {
        if (ShopController.shopController.isSell == true) {
            countItem.itemAmmount = 0;
        }

        countItem.textAmmount.text = "" + countItem.itemAmmount;
    }

    public void ButtonDownIncreaseNumberFunction() {
        if (countItem.itemAmmount < total) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

            countItem.itemAmmount++;
            isBoundIncrease = true;
        }
    }

    public void ButtonUpIncreaseNumberFunction() {
        isBoundIncrease = false;
        cooldown = 1f;
    }

    public void ButtonDownDecreaseNumberFunction() {
        if (countItem.itemAmmount > 0) {
            GameController.gameController.AudioButtonFunction(GameController.gameController.audio.audioButtonClick);

            countItem.itemAmmount--;
            isBoundDecrease = true;
        }
    }

    public void ButtonUpDecreaseNumberFunction() {
        isBoundDecrease = false;
        cooldown = 1f;
    }
}
