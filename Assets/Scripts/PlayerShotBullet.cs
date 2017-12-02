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
            Vector3 bulletDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Debug.Log(bulletDirection + ", " + bulletDirection.sqrMagnitude);
            bulletInstance.GetComponent<Bullet>().direction = bulletDirection;
		}
	}
}
