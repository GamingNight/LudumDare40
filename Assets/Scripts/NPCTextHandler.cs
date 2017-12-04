using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTextHandler : MonoBehaviour {

    public GameObject bubbleText;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {
            bubbleText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            bubbleText.SetActive(false);
        }
    }
}
