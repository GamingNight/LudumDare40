using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float DashSpeed;
    public float maxDashDistance;

    private Rigidbody2D rgbd;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector3 movement;
    private int lastWalkingAnimationState;

    private bool isDashing;
    private float dashDistance;
    private Vector3 prevPosition;
    private float relativeMaxDashDistance;
    private bool lockMovements;
	private bool breakDash;

    void Start() {

        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDashing = false;
        dashDistance = 0;
		lockMovements = false;
		breakDash = false;
        lastWalkingAnimationState = 1;
    }

    void Update() {


        //Handle regular walking
        Move();

        //Handle dash (interrupt walking)
        Dash();

    }

    private void Move() {

        if (lockMovements)//is dashing => lock movement
            return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            movement.Set(horizontal, vertical, 0f);
            movement = movement.normalized * speed * Time.deltaTime;
            rgbd.MovePosition(transform.position + movement);
            if (vertical < 0) {
                animator.SetInteger("walking", 1);
                lastWalkingAnimationState = 1;
            } else if (vertical > 0) {
                animator.SetInteger("walking", -1);
                lastWalkingAnimationState = -1;
            } else {
                animator.SetInteger("walking", lastWalkingAnimationState);
            }
            spriteRenderer.flipX = horizontal < 0;
        } else {
            animator.SetInteger("walking", 0);
        }
    }

    private void Dash() {

		bool dash = false;
		if(!lockMovements)
        	dash = Input.GetMouseButtonDown(1);
		
        if (dash) {
            Vector3 mouseToPlayerDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            mouseToPlayerDirection.z = 0;
            rgbd.velocity = new Vector2(mouseToPlayerDirection.x, mouseToPlayerDirection.y).normalized * DashSpeed;
            isDashing = true;
            dashDistance = 0;
            prevPosition = transform.position;
            relativeMaxDashDistance = Mathf.Min(maxDashDistance, mouseToPlayerDirection.magnitude);
            lockMovements = true;

			//Dash animation
			if (mouseToPlayerDirection.y > 0) {
				animator.SetTrigger ("dashRightTop");
			} else if (mouseToPlayerDirection.y < 0) {
				animator.SetTrigger ("dashLeftBottom");
			}
			spriteRenderer.flipX = mouseToPlayerDirection.x < 0;
        }

        if (isDashing) {
			if (dashDistance < relativeMaxDashDistance && !breakDash) {
                Vector3 diff = transform.position - prevPosition;
                diff.z = 0;
                dashDistance += diff.magnitude;
                prevPosition = transform.position;
            } else {
                rgbd.velocity = Vector2.zero;
                isDashing = false;
                dashDistance = 0;
                lockMovements = false;
				breakDash = false;
            }
        }
    }

	void OnCollisionStay2D(Collision2D other)
	{
		if (isDashing)
			breakDash = true;
	}
}