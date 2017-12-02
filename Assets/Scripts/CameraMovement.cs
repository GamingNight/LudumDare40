using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject ThePlayer;
    public GameObject TheBOSS;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (TheBOSS.transform.position - ThePlayer.transform.position) / 2;
	}
}
