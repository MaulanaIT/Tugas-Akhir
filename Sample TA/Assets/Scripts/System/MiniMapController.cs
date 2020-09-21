using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {
    public GameObject
        player;

    [System.Serializable]
    public class Map {
        public GameObject
            camera,
            map;

        public bool
            isMap;
    }
    public Map map = new Map();

    [System.Serializable]
    public class MiniMap {
        public GameObject 
            miniMapCamera;
    }
    public MiniMap miniMap = new MiniMap();

    float 
        yPosition;

    void Start() {
        yPosition = miniMap.miniMapCamera.transform.position.y;
    }

    void Update() {
        MiniMapFunction();
    }

    public void MapFunction() {
        GameController.gameController.audio.audioButton.Stop();
        GameController.gameController.audio.audioButton.clip = GameController.gameController.audio.audioButtonSelect;
        GameController.gameController.audio.audioButton.Play();

        if (map.isMap == false) {
            map.isMap = true;
            map.map.SetActive(true);
        } else {
            map.isMap = false;
            map.map.SetActive(false);
        }
    }

    public void MiniMapFunction() {
        miniMap.miniMapCamera.transform.position = new Vector3(player.transform.position.x, yPosition + player.transform.position.y, player.transform.position.z);
    }
}
