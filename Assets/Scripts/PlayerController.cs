﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rgbd;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector3 movement;

    private float maxDashDistance;
    private bool isDashing;
    private float dashDistance;
    private Vector3 prevPosition;
    private float relativeMaxDashDistance;
    private bool lockMovement;

    void Start() {

        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDashing = false;
        dashDistance = 0;
        maxDashDistance = 2f;
        lockMovement = false;
    }

    void Update() {


        //Handle regular walking
        Move();

        //Handle dash (interrupt walking)
        Dash();

    }

    private void Move() {

        if (lockMovement)//is dashing => lock movement
            return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            movement.Set(horizontal, vertical, 0f);
            movement = movement.normalized * speed * Time.deltaTime;
            rgbd.MovePosition(transform.position + movement);
            animator.SetBool("walking", true);
            spriteRenderer.flipX = horizontal > 0;
        } else {
            animator.SetBool("walking", false);
        }
    }

    private void Dash() {

        bool dash = Input.GetMouseButtonDown(1);
        if (dash) {
            Vector3 mouseToPlayerDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            mouseToPlayerDirection.z = 0;
            rgbd.velocity = new Vector2(mouseToPlayerDirection.x, mouseToPlayerDirection.y).normalized * 5;
            isDashing = true;
            dashDistance = 0;
            prevPosition = transform.position;
            relativeMaxDashDistance = Mathf.Min(maxDashDistance, mouseToPlayerDirection.magnitude);
            lockMovement = true;
            spriteRenderer.flipX = mouseToPlayerDirection.x > 0;
        }

        if (isDashing) {
            if (dashDistance < relativeMaxDashDistance) {
                Vector3 diff = transform.position - prevPosition;
                diff.z = 0;
                dashDistance += diff.magnitude;
                prevPosition = transform.position;
            } else {
                rgbd.velocity = Vector2.zero;
                isDashing = false;
                dashDistance = 0;
                lockMovement = false;
            }
        }
    }
}