using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutController : MonoBehaviour {

    public bool 
        isVertical, 
        isHorizontal;

    void Start() {

    }

    void Update() {
        if (isVertical == true) {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, (gameObject.GetComponent<GridLayoutGroup>().cellSize.y * gameObject.transform.childCount) + gameObject.GetComponent<GridLayoutGroup>().padding.bottom + gameObject.GetComponent<GridLayoutGroup>().padding.top);
        }

        if (isHorizontal == true) {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((gameObject.GetComponent<RectTransform>().sizeDelta.x * gameObject.transform.childCount) + gameObject.GetComponent<GridLayoutGroup>().padding.left + gameObject.GetComponent<GridLayoutGroup>().padding.right, gameObject.GetComponent<GridLayoutGroup>().cellSize.y);
        }

        if (isVertical == false && isHorizontal == false) {
            print("Script is not setting up");
        }
    }
}
