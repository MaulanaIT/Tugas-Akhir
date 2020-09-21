using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SaveController : MonoBehaviour {

    public static SaveController 
        saveController;

    [System.Serializable]
    public class Farm {
        public GameObject[] 
            area, 
            plant;
    }
    public Farm farm = new Farm();

    public float 
        cooldown = 1f;

    public bool
        isAction,
        isSaveEnvironment,
        isSaveInventory,
        isSavePlant,
        isSaveQuest,
        isSaveSetting,
        isSaveState,
        isSaveTime;

    [System.Serializable]
    public class Load {
        public bool[]
            isLoaded;

        public bool
            isLoadedCompleted;
    }
    public Load load = new Load();

    private void Awake() {
        if (saveController == null) {
            saveController = this;
        } else if (saveController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (PlayerPrefs.GetInt("SaveGame") == 1) {
            PlayerPrefs.SetInt("LoadGame", 1);
        } else {
            farm.area = new GameObject[0];
            farm.plant = new GameObject[0];
            PlayerPrefs.SetInt("SaveGame", 1);
        }
    }

    void Update() {
        if (isAction == true) {
            farm.area = GameObject.FindGameObjectsWithTag("Area");
            farm.plant = GameObject.FindGameObjectsWithTag("Plant");

            cooldown -= Time.deltaTime;

            if (cooldown <= 0) {
                isAction = false;
                cooldown = 1f;
                isSaveState = true;
            }
        }

        if (PlayerPrefs.GetInt("LoadGame") == 1) {
            LoadGame();
        } else {
            SaveGame();
        }
    }

    public void SaveGame() {
        //Fungsi menyimpan data setting
        PlayerPrefs.SetInt("TextureQuality", QualitySettings.masterTextureLimit);
        PlayerPrefs.SetInt("AntiAliasing", QualitySettings.antiAliasing);

        if (QualitySettings.shadows == ShadowQuality.Disable) {
            PlayerPrefs.SetInt("ShadowQuality", 0);
        } else if (QualitySettings.shadows == ShadowQuality.HardOnly) {
            PlayerPrefs.SetInt("ShadowQuality", 1);
        } else if (QualitySettings.shadows == ShadowQuality.All) {
            PlayerPrefs.SetInt("ShadowQuality", 2);
        }

        if (QualitySettings.shadowResolution == ShadowResolution.Low) {
            PlayerPrefs.SetInt("ShadowResolution", 0);
        } else if (QualitySettings.shadowResolution == ShadowResolution.Medium) {
            PlayerPrefs.SetInt("ShadowResolution", 1);
        } else if (QualitySettings.shadowResolution == ShadowResolution.High) {
            PlayerPrefs.SetInt("ShadowResolution", 2);
        } else if (QualitySettings.shadowResolution == ShadowResolution.VeryHigh) {
            PlayerPrefs.SetInt("ShadowResolution", 3);
        }

        PlayerPrefs.SetInt("IndexQuality", SettingController.settingController.quality.indexQuality);
        PlayerPrefs.SetInt("CameraSensitivity", Convert.ToInt32(SettingController.settingController.cameraSensitivity.cameraSensitivity.value));
        PlayerPrefs.SetInt("SoundVolume", Convert.ToInt32(SettingController.settingController.audioVolume.soundVolume.value));
        PlayerPrefs.SetInt("MusicVolume", Convert.ToInt32(SettingController.settingController.audioVolume.musicVolume.value));

        //Fungsi menyimpan data waktu
        PlayerPrefs.SetFloat("TimeCooldown", ClockController.clockController.timeCooldown);
        PlayerPrefs.SetInt("Minute", ClockController.clockController.minute);
        PlayerPrefs.SetInt("Hour", ClockController.clockController.hour);
        PlayerPrefs.SetInt("Date", ClockController.clockController.date);
        PlayerPrefs.SetString("Day", ClockController.clockController.day);
        PlayerPrefs.SetString("Month", ClockController.clockController.month);

        //Fungsi menyimpan data matahari
        PlayerPrefs.SetFloat("Sun", GameController.gameController.sunEuler);
        PlayerPrefs.SetFloat("Moon", GameController.gameController.moonEuler);

        //Fungsi menyimpan data inventory
        for (int i = 0; i < InventoryItemController.inventoryItemController.itemObtained.name.Length; i++) {
            PlayerPrefs.SetString("ItemObtainedName" + i, InventoryItemController.inventoryItemController.itemObtained.name[i]);
            PlayerPrefs.SetInt("ItemObtainedCount" + i, InventoryItemController.inventoryItemController.itemObtained.count[i]);
        }

        for (int i = 0; i < InventoryToolsController.inventoryToolsController.toolsObtained.name.Length; i++) {
            PlayerPrefs.SetString("ToolObtainedName" + i, InventoryToolsController.inventoryToolsController.toolsObtained.name[i]);
        }

        for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
            PlayerPrefs.SetString("SeedObtainedName" + i, InventorySeedsController.inventorySeedsController.seedsObtained.name[i]);
            PlayerPrefs.SetInt("SeedObtainedCount" + i, InventorySeedsController.inventorySeedsController.seedsObtained.count[i]);
        }

        for (int i = 0; i < GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.totalSlot; i++) {
            PlayerPrefs.SetString("ItemStorageName" + i, GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.name[i]);
            PlayerPrefs.SetInt("ItemStorageCount" + i, GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.count[i]);
        }

        for (int i = 0; i < GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.totalSlot; i++) {
            PlayerPrefs.SetString("ToolStorageName" + i, GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.name[i]);
            PlayerPrefs.SetInt("ToolStorageCount" + i, GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.count[i]);
        }

        for (int i = 0; i < GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.totalSlot; i++) {
            PlayerPrefs.SetString("SeedStorageName" + i, GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.name[i]);
            PlayerPrefs.SetInt("SeedStorageCount" + i, GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.count[i]);
        }

        //Fungsi menyimpan data quest
        for (int i = 0; i < QuestController.questController.quest.Length; i++) {
            for (int j = 0; j < QuestController.questController.quest[i].questProgress.Length; j++) {
                PlayerPrefs.SetFloat("QuestProgress" + i + j, QuestController.questController.quest[i].questProgress[j]);
            }
        }

        //Fungsi menyimpan data player
        if (ActionController.actionController.conversationFunction.isConversation == false) {
            SaveState("Player", GameObject.FindGameObjectWithTag("Player"));
        }
        PlayerPrefs.SetInt("Money", ProfileController.profileController.money);

        //Fungsi menyimpan data tumbuhan
        for (int i = 0; i < farm.area.Length; i++) {
            if (farm.area[i] != null) {
                SaveState("Area" + i, farm.area[i]);
                if (farm.area[i].GetComponent<DigController>().isSeed == true) {
                    PlayerPrefs.SetInt("AreaSeedStatus" + i, 1);
                } else {
                    PlayerPrefs.SetInt("AreaSeedStatus" + i, 0);
                }
            }
        }

        for (int i = 0; i < farm.plant.Length; i++) {
            if (farm.plant[i] != null) {
                SaveState("Plant" + i, farm.plant[i]);
            }
        }

        PlayerPrefs.SetInt("TotalArea", farm.area.Length);
        PlayerPrefs.SetInt("TotalPlant", farm.plant.Length);

        if (farm.plant.Length >= 1) {
            for (int i = 0; i < farm.plant.Length; i++) {
                PlayerPrefs.SetString("Plant" + i + "Name", farm.plant[i].GetComponent<SeedController>().seedName);
                PlayerPrefs.SetString("Plant" + i + "HarvestName", farm.plant[i].GetComponent<SeedController>().harvestName);
                PlayerPrefs.SetFloat("Plant" + i + "FirstPhase", farm.plant[i].GetComponent<SeedController>().firstPhase);
                PlayerPrefs.SetFloat("Plant" + i + "SecondPhase", farm.plant[i].GetComponent<SeedController>().secondPhase);
                PlayerPrefs.SetFloat("Plant" + i + "ThirdPhase", farm.plant[i].GetComponent<SeedController>().thirdPhase);
                PlayerPrefs.SetFloat("Plant" + i + "RepeatableHarvestTime", farm.plant[i].GetComponent<SeedController>().repeatableHarvestTime);
                PlayerPrefs.SetFloat("Plant" + i + "MinHarvestItem", farm.plant[i].GetComponent<SeedController>().minHarvestItem);
                PlayerPrefs.SetFloat("Plant" + i + "MaxHarvestItem", farm.plant[i].GetComponent<SeedController>().maxHarvestItem);
                PlayerPrefs.SetFloat("Plant" + i + "TimeCooldown", farm.plant[i].GetComponent<SeedController>().timeCooldown);
            }
        }
    }

    public void LoadGame() {
        if (load.isLoadedCompleted == false) {
            Time.timeScale = 0;

            //Load object
            if (load.isLoaded[0] == false) {
                LoadState("Player", GameObject.FindGameObjectWithTag("Player"), true, true, true);

                ProfileController.profileController.money = PlayerPrefs.GetInt("Money");

                GameController.gameController.sunEuler = PlayerPrefs.GetFloat("Sun");
                GameController.gameController.moonEuler = PlayerPrefs.GetFloat("Moon");

                load.isLoaded[0] = true;
            }

            if (load.isLoaded[0] == true && load.isLoaded[1] == false) {
                farm.area = new GameObject[PlayerPrefs.GetInt("TotalArea")];
                farm.plant = new GameObject[PlayerPrefs.GetInt("TotalPlant")];

                load.isLoaded[1] = true;
            }

            if (load.isLoaded[1] == true && load.isLoaded[2] == false) {
                for (int i = 0; i < farm.area.Length; i++) {
                    farm.area[i] = Instantiate(ActionController.actionController.prefebDigObject);

                    LoadState("Area" + i, farm.area[i], true, true, true);

                    if (PlayerPrefs.GetInt("AreaSeedStatus" + i) == 1) {
                        farm.area[i].GetComponent<DigController>().isSeed = true;
                    } else {
                        farm.area[i].GetComponent<DigController>().isSeed = false;
                    }
                }
                load.isLoaded[2] = true;
            }

            //Load kebun
            if (load.isLoaded[2] == true && load.isLoaded[3] == false) {
                for (int i = 0; i < farm.plant.Length; i++) {
                    farm.plant[i] = Instantiate(ActionController.actionController.prefebSeedObject);

                    LoadState("Plant" + i, farm.plant[i], true, true, true);
                    farm.plant[i].GetComponent<SeedController>().seedName = PlayerPrefs.GetString("Plant" + i + "Name");
                    farm.plant[i].GetComponent<SeedController>().harvestName = PlayerPrefs.GetString("Plant" + i + "HarvestName");
                    farm.plant[i].GetComponent<SeedController>().firstPhase = PlayerPrefs.GetFloat("Plant" + i + "FirstPhase");
                    farm.plant[i].GetComponent<SeedController>().secondPhase = PlayerPrefs.GetFloat("Plant" + i + "SecondPhase");
                    farm.plant[i].GetComponent<SeedController>().thirdPhase = PlayerPrefs.GetFloat("Plant" + i + "ThirdPhase");
                    farm.plant[i].GetComponent<SeedController>().repeatableHarvestTime = PlayerPrefs.GetFloat("Plant" + i + "RepeatableHarvestTime");
                    farm.plant[i].GetComponent<SeedController>().minHarvestItem = PlayerPrefs.GetFloat("Plant" + i + "MinHarvestItem");
                    farm.plant[i].GetComponent<SeedController>().maxHarvestItem = PlayerPrefs.GetFloat("Plant" + i + "MaxHarvestItem");
                    farm.plant[i].GetComponent<SeedController>().timeCooldown = PlayerPrefs.GetFloat("Plant" + i + "TimeCooldown");
                }

                load.isLoaded[3] = true;
            }

            //Load inventory
            if (load.isLoaded[3] == true && load.isLoaded[4] == false) {
                for (int i = 0; i < InventoryItemController.inventoryItemController.itemObtained.name.Length; i++) {
                    InventoryItemController.inventoryItemController.itemObtained.name[i] = PlayerPrefs.GetString("ItemObtainedName" + i);
                    InventoryItemController.inventoryItemController.itemObtained.count[i] = PlayerPrefs.GetInt("ItemObtainedCount" + i);
                }

                for (int i = 0; i < InventoryToolsController.inventoryToolsController.toolsObtained.name.Length; i++) {
                    InventoryToolsController.inventoryToolsController.toolsObtained.name[i] = PlayerPrefs.GetString("ToolObtainedName" + i);
                }

                for (int i = 0; i < InventorySeedsController.inventorySeedsController.seedsObtained.name.Length; i++) {
                    InventorySeedsController.inventorySeedsController.seedsObtained.name[i] = PlayerPrefs.GetString("SeedObtainedName" + i);
                    InventorySeedsController.inventorySeedsController.seedsObtained.count[i] = PlayerPrefs.GetInt("SeedObtainedCount" + i);
                }

                for (int i = 0; i < GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.totalSlot; i++) {
                    GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.name[i] = PlayerPrefs.GetString("ItemStorageName" + i);
                    GameObject.FindGameObjectWithTag("ItemStorage").GetComponent<ContainerController>().itemContainer.count[i] = PlayerPrefs.GetInt("ItemStorageCount" + i);
                }

                for (int i = 0; i < GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.totalSlot; i++) {
                    GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.name[i] = PlayerPrefs.GetString("ToolStorageName" + i);
                    GameObject.FindGameObjectWithTag("ToolStorage").GetComponent<ContainerController>().toolContainer.count[i] = PlayerPrefs.GetInt("ToolStorageCount" + i);
                }

                for (int i = 0; i < GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.totalSlot; i++) {
                    GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.name[i] = PlayerPrefs.GetString("SeedStorageName" + i);
                    GameObject.FindGameObjectWithTag("SeedStorage").GetComponent<ContainerController>().seedContainer.count[i] = PlayerPrefs.GetInt("SeedStorageCount" + i);
                }

                load.isLoaded[4] = true;
            }

            //Load Waktu
            if (load.isLoaded[4] == true && load.isLoaded[5] == false) {
                ClockController.clockController.timeCooldown = PlayerPrefs.GetFloat("TimeCooldown");
                ClockController.clockController.minute = PlayerPrefs.GetInt("Minute");
                ClockController.clockController.hour = PlayerPrefs.GetInt("Hour");
                ClockController.clockController.date = PlayerPrefs.GetInt("Date");
                ClockController.clockController.day = PlayerPrefs.GetString("Day");
                ClockController.clockController.month = PlayerPrefs.GetString("Month");

                load.isLoaded[5] = true;
            }

            //Load setting
            if (load.isLoaded[5] == true && load.isLoaded[6] == false) {
                QualitySettings.masterTextureLimit = PlayerPrefs.GetInt("TextureQuality");
                QualitySettings.antiAliasing = PlayerPrefs.GetInt("AntiAliasing");

                if (PlayerPrefs.GetInt("ShadowQuality") == 0) {
                    QualitySettings.shadows = ShadowQuality.Disable;
                } else if (PlayerPrefs.GetInt("ShadowQuality") == 1) {
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                } else if (PlayerPrefs.GetInt("ShadowQuality") == 2) {
                    QualitySettings.shadows = ShadowQuality.All;
                }

                if (PlayerPrefs.GetInt("ShadowResolution") == 0) {
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                } else if (PlayerPrefs.GetInt("ShadowResolution") == 1) {
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                } else if (PlayerPrefs.GetInt("ShadowResolution") == 2) {
                    QualitySettings.shadowResolution = ShadowResolution.High;
                } else if (PlayerPrefs.GetInt("ShadowResolution") == 3) {
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                }

                SettingController.settingController.cameraSensitivity.cameraSensitivity.value = PlayerPrefs.GetInt("CameraSensitivity");
                SettingController.settingController.audioVolume.soundVolume.value = PlayerPrefs.GetInt("SoundVolume");
                SettingController.settingController.audioVolume.musicVolume.value = PlayerPrefs.GetInt("MusicVolume");
                SettingController.settingController.quality.indexQuality = PlayerPrefs.GetInt("IndexQuality");

                load.isLoaded[6] = true;
                load.isLoadedCompleted = true;
            }
        } else {
            Time.timeScale = 1;
            PlayerPrefs.SetInt("LoadGame", 0);
        }
    }

    public void SaveState(string SaveName, GameObject Object) {
        PlayerPrefs.SetFloat("PositionX" + SaveName, Object.transform.localPosition.x);
        PlayerPrefs.SetFloat("PositionY" + SaveName, Object.transform.localPosition.y);
        PlayerPrefs.SetFloat("PositionZ" + SaveName, Object.transform.localPosition.z);
        PlayerPrefs.SetFloat("RotationX" + SaveName, Object.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("RotationY" + SaveName, Object.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("RotationZ" + SaveName, Object.transform.eulerAngles.z);
        PlayerPrefs.SetFloat("ScaleX" + SaveName, Object.transform.localScale.x);
        PlayerPrefs.SetFloat("ScaleY" + SaveName, Object.transform.localScale.y);
        PlayerPrefs.SetFloat("ScaleZ" + SaveName, Object.transform.localScale.z);
    }

    public void LoadState(string SaveName, GameObject Object, bool position, bool rotation, bool scale) {
        if (position == true) {
            Object.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("PositionX" + SaveName), PlayerPrefs.GetFloat("PositionY" + SaveName), PlayerPrefs.GetFloat("PositionZ" + SaveName));
        }

        if (rotation == true) {
            Object.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("RotationX" + SaveName), PlayerPrefs.GetFloat("RotationY" + SaveName), PlayerPrefs.GetFloat("RotationZ" + SaveName));
        }

        if (scale == true) {
            Object.transform.localScale = new Vector3(PlayerPrefs.GetFloat("ScaleX" + SaveName), PlayerPrefs.GetFloat("ScaleY" + SaveName), PlayerPrefs.GetFloat("ScaleZ" + SaveName));
        }
    }
}
