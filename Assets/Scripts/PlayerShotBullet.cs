using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotBullet : MonoBehaviour {

	public GameObject bulletPrefab;

	void Start () {
		
	}
	
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			GameObject bulletInstance = Instantiate<GameObject> (bulletPrefab, transform.position, Quaternion.identity);
		}
	}
}
