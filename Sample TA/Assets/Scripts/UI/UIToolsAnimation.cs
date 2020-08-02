using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolsAnimation : MonoBehaviour {

    public static UIToolsAnimation 
        uiToolsAnimation;

    public GameObject 
        toolsOne, 
        toolsTwo, 
        toolsThree, 
        toolsFour;

    private Animator 
        animator;

    public bool 
        isExpand, 
        isPlaying;

    public float 
        timeAutoCollapse;

    private void Awake() {
        if (uiToolsAnimation == null) {
            uiToolsAnimation = this;
        } else if (uiToolsAnimation != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        autoCollapseFunction();
    }

    public void buttonToolsAnimation() {
        if (isExpand == false && isPlaying == false) {
            expandFunction();
        } else if (isExpand == true && isPlaying == false) {
            collapseFunction();
        }
    }

    public void autoCollapseFunction() {
        if (isExpand == true) {
            timeAutoCollapse -= Time.deltaTime;

            if (timeAutoCollapse <= 0) {
                collapseFunction();
            }
        }
    }

    public void expandFunction() {
        isPlaying = true;

        toolsOne.SetActive(true);
        toolsTwo.SetActive(true);
        toolsThree.SetActive(true);
        toolsFour.SetActive(true);

        animator.SetBool("Expand", true);
        animator.SetBool("Idle", false);

        StartCoroutine(isExpandPlaying());
    }

    public void collapseFunction() {
        isPlaying = true;

        animator.SetBool("Expand", false);
        animator.SetBool("Collapse", true);

        StartCoroutine(isCollapsePlaying());
    }

    IEnumerator isExpandPlaying() {
        yield return new WaitForSeconds(0.3f);

        isExpand = true;
        isPlaying = false;
    }

    IEnumerator isCollapsePlaying() {
        yield return new WaitForSeconds(0.4f);

        toolsOne.SetActive(false);
        toolsTwo.SetActive(false);
        toolsThree.SetActive(false);
        toolsFour.SetActive(false);

        animator.SetBool("Collapse", false);
        animator.SetBool("Idle", true);

        isExpand = false;
        isPlaying = false;

        timeAutoCollapse = 10f;
    }
}
