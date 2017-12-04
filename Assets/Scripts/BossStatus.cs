using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour {

    public int maxHealth;
    public Image healthUI;

	// 0 init = start shoot
	// 1 display message
	// 2 shoot again
	public int phase = 0;

    private int health;
    private int power;
	private int nbShoot;

    private void Start() {
        health = maxHealth;
        power = 30;
		nbShoot = 0;
    }

	void Update() {
		PhaseUpdate ();
	}

	public void PhaseUpdate() {
		// player power must be 1 so maxHealth = the exact number of shoot to kill the boss
		if (phase == 0 && nbShoot > maxHealth / 3) {
            // Pass to phase 1
            incrementPhase();
            // Pass to phase 2
            StartCoroutine(waitAndIncrementPhase(3));
        }
		if (phase == 2 && nbShoot > maxHealth * 2 / 3)
            {
                // Pass to phase 3
                incrementPhase();
                // Pass to phase 4
                StartCoroutine(waitAndIncrementPhase(3));
            }
	}

	public void incrementPhase() {

		Debug.Log("Boss change phase "  + phase + " to " + (phase+1));
		phase = phase + 1;
	}

	IEnumerator waitAndIncrementPhase(float timeScale) {
		yield return new WaitForSeconds(timeScale);
		incrementPhase ();
			
	}
		
    public void TakeDamages(int damages) {
		nbShoot++;
        health = Mathf.Max(health - damages, 0);
        healthUI.rectTransform.localScale = new Vector3((float)health / maxHealth, healthUI.rectTransform.localScale.y, healthUI.rectTransform.localScale.z);
        Debug.Log("Player takes " + damages + " damages! Remaining life = " + health);
        if (health <= 0) {
            GameManager.GetInstance().Winner();
        }
    }

    public void AddPower(int value) {

        power += value;
    }

    public int GetPower() {
        return power;
    }
}
