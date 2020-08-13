using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableController : MonoBehaviour {

    GridLayoutGroup 
        gridLayoutGroup;

    RectTransform 
        rectTransform;

    public GameObject 
        panelScrollable;

    public float 
        width, 
        height,
        totalRows,
        totalChild,
        totalColumns;

    void Start() {
        panelScrollable = gameObject;
        gridLayoutGroup = panelScrollable.GetComponent<GridLayoutGroup>();
        rectTransform = panelScrollable.GetComponent<RectTransform>();

        width = rectTransform.sizeDelta.x - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right;

        totalChild = rectTransform.childCount;
        totalColumns = Mathf.RoundToInt(width / (gridLayoutGroup.spacing.x + gridLayoutGroup.cellSize.x));
        totalRows = Mathf.Ceil(totalChild / totalColumns);

        height = (gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom) + (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) * totalRows;

        if (height >= rectTransform.sizeDelta.y) {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        }
    }

    void Update() {

    }
}
