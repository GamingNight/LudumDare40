using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rgbd;
    private Vector3 movement;

    void Start() {

        rgbd = GetComponent<Rigidbody2D>();
    }

    void Update() {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement.Set(h, v, 0f);

        movement = movement.normalized * speed * Time.deltaTime;

        rgbd.MovePosition(transform.position + movement);
    }
}