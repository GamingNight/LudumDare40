using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBullet : MonoBehaviour {

    public GameObject bulletPrefab;

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 bulletDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            bulletDirection.z = 0f;
            bulletInstance.GetComponent<Bullet>().direction = bulletDirection.normalized;
        }
    }
}
