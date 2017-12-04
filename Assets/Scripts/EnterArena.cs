using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArena : MonoBehaviour {

	public GameObject myCamera;
	public GameObject invisibleWall;
    public GameObject bossUI;
    public GameObject boss;
    public GameObject gameManager;

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Player") {
			myCamera.GetComponent<CameraFollowPlayer>().enabled = false;
			myCamera.GetComponent<CameraBoss>().enabled = true;
			invisibleWall.SetActive (true);
            bossUI.SetActive(true);

            foreach (AudioSource src in boss.GetComponents<AudioSource>()) {
                if(src.clip.name == "monster-apparition") {
                    src.Play();
                }
            }
            foreach (AudioSource src in gameManager.GetComponents<AudioSource>()) {
                if (src.clip.name == "battle1") {
                    src.Play();
                }
            }

            Destroy(gameObject);
		}
	}
}
