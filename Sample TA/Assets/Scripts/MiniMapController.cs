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

    void Start() {
        
    }

    void Update() {
        MiniMapFunction();
    }

    public void MapFunction() {
        if (map.isMap == false) {
            map.isMap = true;
            map.map.SetActive(true);
        } else {
            map.isMap = false;
            map.map.SetActive(false);
        }
    }

    public void MiniMapFunction() {
        miniMap.miniMapCamera.transform.position = new Vector3(player.transform.position.x, miniMap.miniMapCamera.transform.position.y, player.transform.position.z);
    }
}
