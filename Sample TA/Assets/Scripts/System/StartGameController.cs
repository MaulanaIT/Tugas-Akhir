using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour {

    public static StartGameController 
        startGameController;

    public Animator 
        playerAnim;

    public Rigidbody 
        playerRigidbody;

    public GameObject 
        player;

    public Vector3[] 
        movePoint, 
        rotationPoint;

    public int 
        index = 0, 
        speed;

    public bool 
        isPrologue;

    private void Awake() {
        if (startGameController == null) {
            startGameController = this;
        } else if (startGameController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        playerAnim = player.GetComponent<Animator>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void Update() {
        if (isPrologue == true) {
            playerAnim.SetBool("param_idletowalk", true);
            playerAnim.SetBool("param_toidle", false);
            player.transform.LookAt(movePoint[index]);

            player.transform.position = Vector3.MoveTowards(transform.position, movePoint[index], speed);

            if (player.transform.localPosition.x <= movePoint[index].x && player.transform.localPosition.z <= movePoint[index].z &&
                player.transform.localPosition.x >= movePoint[index].x && player.transform.localPosition.z >= movePoint[index].z) {
            }
        }
    }
}
