﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float Damp;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

	//void OnTriggerEnter2D(Collider2D other) {
	//	Debug.Log ("touch explosion " + other.gameObject.name);
    //    GetComponent<BoxCollider2D>().isTrigger = false;
    //}

    public void Autodestroy()
    {
        Destroy(gameObject);
    }
}
