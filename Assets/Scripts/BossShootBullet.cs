using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBullet : MonoBehaviour {

	public GameObject bulletPrefab;
	public float timeInterval;

	private float crtTime;

	void Update () {

		crtTime += Time.deltaTime;
		if (crtTime >= timeInterval) {
			crtTime = 0;
			GameObject bulletInstanceUp = Instantiate<GameObject> (bulletPrefab, transform.position, Quaternion.identity);
			bulletInstanceUp.GetComponent<Bullet> ().direction = new Vector3 (0, 1, 0);
            bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
            GameObject bulletInstanceDown = Instantiate<GameObject> (bulletPrefab, transform.position, Quaternion.identity);
			bulletInstanceDown.GetComponent<Bullet> ().direction = new Vector3 (0, -1, 0);
            bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
            GameObject bulletInstanceLeft = Instantiate<GameObject> (bulletPrefab, transform.position, Quaternion.identity);
			bulletInstanceLeft.GetComponent<Bullet> ().direction = new Vector3 (-1, 0, 0);
            bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
            GameObject bulletInstanceRight = Instantiate<GameObject> (bulletPrefab, transform.position, Quaternion.identity);
			bulletInstanceRight.GetComponent<Bullet> ().direction = new Vector3 (1, 0, 0);
            bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;
		}
	}
}
