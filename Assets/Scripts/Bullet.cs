using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public enum Type {
        BOSS, PLAYER
    }

    public Type type;
    public GameObject ExplosionPrefab;
    public float initSpeed;
    public Vector3 direction;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = direction * initSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {

        bool playerHitsBoss = (type == Type.PLAYER && other.gameObject.tag == "Boss");
        bool bossHitsPlayer = (type == Type.BOSS && other.gameObject.tag == "Player");
		bool hitBackground = (other.gameObject.tag == "Wall");
        //bool otherIsBullet = other.gameObject.tag == "Bullet";
        //bool bulletHitsBullet = false;
        //if (otherIsBullet)
        //{
        //    bulletHitsBullet = other.gameObject.GetComponent<Bullet>().type != type;
        //}
		if (playerHitsBoss || bossHitsPlayer || hitBackground) { //|| bulletHitsBullet) { 
            Destroy(gameObject);
            Instantiate<GameObject>(ExplosionPrefab, transform.position, Quaternion.identity);
            if (other.gameObject.tag == "Player") {
                other.gameObject.GetComponent<PlayerStatus>().TakeDamages(transform.parent.GetComponent<BossStatus>().GetPower());
            } else if (other.gameObject.tag == "Boss") {
                other.gameObject.GetComponent<BossStatus>().TakeDamages(transform.parent.GetComponent<PlayerStatus>().GetPower());
            }
        }
    }
}
