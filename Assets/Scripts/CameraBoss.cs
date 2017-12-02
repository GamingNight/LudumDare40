using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoss : MonoBehaviour {

    public GameObject ThePlayer;
    public GameObject TheBOSS;

	private Vector3 initCameraPosition;
	private Vector3 targetCameraPosition;
	private bool switchingMode;
	private float lerpTime;

    // Use this for initialization
    void Start () {
		initCameraPosition = transform.position;
		switchingMode = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (switchingMode) {
			targetCameraPosition = (TheBOSS.transform.position + ThePlayer.transform.position) / 2;
			Vector3 lerp =  Vector3.Lerp (initCameraPosition, targetCameraPosition, lerpTime * 2);
			transform.position = new Vector3 (lerp.x, lerp.y, transform.position.z);
			lerpTime += Time.deltaTime;
			if (lerpTime >= 1) {
				switchingMode = false;
			}
		} else {
			Vector3 newPosition = (TheBOSS.transform.position + ThePlayer.transform.position) / 2;
			transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
		}
	}
}
