using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float initSpeed;
    public Vector3 direction;

	void Start () {
		
	}
	
	void Update () {
	
		transform.Translate (direction * initSpeed * Time.deltaTime);
	}
}
