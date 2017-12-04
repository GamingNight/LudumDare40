using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBullet : MonoBehaviour {

    public GameObject bulletPrefab;
    private AudioSource shootAudioSource;
    private bool active = true;
    private bool activeLockGuard = false;

    private void Start() {
        foreach (AudioSource src in GetComponents<AudioSource>()) {
            if (src.clip.name == "hero-shoot") {
                shootAudioSource = src;
            }
        }
    }

    void Update() {

        // desactive shoot if the boss is in pahse 1
        GameObject boss = GameObject.Find("Boss");
        if (boss && boss.GetComponent<BossStatus>().phase == 1 || boss && boss.GetComponent<BossStatus>().phase == 3)
            active = false;

        //resactive shoot if the boss is in pahse 2
        if ((boss && boss.GetComponent<BossStatus>().phase == 2 && !activeLockGuard) || (boss && boss.GetComponent<BossStatus>().phase == 4 && !activeLockGuard)) {
            activeLockGuard = true;
            StartCoroutine(waitAndReactive(3));
        }

        if (active && Input.GetMouseButtonDown(0)) {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            Vector3 bulletDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            bulletDirection.z = 0f;
            bulletInstance.GetComponent<Bullet>().direction = bulletDirection.normalized;
            bulletInstance.GetComponent<Bullet>().type = Bullet.Type.PLAYER;
            shootAudioSource.Play();
        }
    }

    IEnumerator waitAndReactive(float spendTime) {
        yield return new WaitForSeconds(spendTime);
        active = true;
        activeLockGuard = false;
    }
}

