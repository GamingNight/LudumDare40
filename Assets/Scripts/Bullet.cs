using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject ExplosionPrefab;
    public float initSpeed;
    public Vector3 direction;

	void Start () {
		
	}
	
	void Update () {
	
		transform.Translate (direction * initSpeed * Time.deltaTime);
	
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("touch " + other.gameObject.name);
		Destroy(gameObject);
		GameObject ExplosionInstance = Instantiate<GameObject> (ExplosionPrefab, transform.position, Quaternion.identity);
	}
}
