using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWinScreen : MonoBehaviour {

    public Text sacrificiedText;
    public GameObject proverbGO;

    void Start() {
        if (GameManager.killedInhabitants > 0) {
            sacrificiedText.text = "but also sacrificed " + GameManager.killedInhabitants + " of your people.";
            proverbGO.SetActive(true);
        } else {
            sacrificiedText.text = "without sacrificing anyone. Congratulations!";
            proverbGO.SetActive(false);
        }
    }
}
