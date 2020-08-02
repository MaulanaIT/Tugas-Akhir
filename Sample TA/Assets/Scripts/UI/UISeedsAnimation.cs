using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISeedsAnimation : MonoBehaviour {

    public GameObject 
        seedsOne, 
        seedsTwo, 
        seedsThree,
        seedsFour, 
        seedsFive, 
        seedsSix;

    private Animator 
        animator;

    public bool 
        isExpand,
        isPlaying;

    public float 
        timeAutoCollapse;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        autoCollapseFunction();
    }

    public void buttonSeedsAnimation() {
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

        seedsOne.SetActive(true);
        seedsTwo.SetActive(true);
        seedsThree.SetActive(true);
        seedsFour.SetActive(true);
        seedsFive.SetActive(true);
        seedsSix.SetActive(true);

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
        yield return new WaitForSeconds(0.6f);

        isExpand = true;
        isPlaying = false;
    }

    IEnumerator isCollapsePlaying() {
        yield return new WaitForSeconds(0.6f);


        seedsOne.SetActive(false);
        seedsTwo.SetActive(false);
        seedsThree.SetActive(false);
        seedsFour.SetActive(false);
        seedsFive.SetActive(false);
        seedsSix.SetActive(false);

        animator.SetBool("Collapse", false);
        animator.SetBool("Idle", true);

        isExpand = false;
        isPlaying = false;

        timeAutoCollapse = 10f;
    }
}
