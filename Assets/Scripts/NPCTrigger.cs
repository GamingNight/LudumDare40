using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	public GameObject deadBubbleGameObject;
	public PlayerStatus playerStatus;
	public bool isActivated = false;
	public int powerUp;
	private DialogBubble crtBubble;

	private Animator animator;

	// Use this for initialization
	void Start () {
		crtBubble = GetComponent<DialogBubble> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.A) && (crtBubble.IsTalking || crtBubble.vBubble.vMessage == "")){
			crtBubble.vCurrentBubble.GetComponent<Appear> ().Disable ();
			animator.SetBool ("dead", true);
			isActivated = true;
		}
	}

	public void ActivateDeadBubble(){
		if(deadBubbleGameObject != null)
			deadBubbleGameObject.SetActive (true);
	}

	public void Autodestroy(){
		Destroy (gameObject);
	}

	public void PowerUpPlayer(){
		playerStatus.PowerUp (powerUp);
	}
}
