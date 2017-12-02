using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

	public GameObject myCamera;
	public GameObject invisibleWall;

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Player") {
			myCamera.GetComponent<CameraFollowPlayer>().enabled = false;
			myCamera.GetComponent<CameraBoss>().enabled = true;
			invisibleWall.SetActive (true);
		}
	}
}
