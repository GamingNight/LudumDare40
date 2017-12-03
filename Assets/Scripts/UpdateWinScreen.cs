using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWinScreen : MonoBehaviour {

    void Start() {
        if (GameManager.killedInhabitants > 0) {
            GetComponent<Text>().text = "but also sacrificed " + GameManager.killedInhabitants + " of your people";
        } else {
            GetComponent<Text>().text = "without sacrificing anyone";
        }
    }
}
