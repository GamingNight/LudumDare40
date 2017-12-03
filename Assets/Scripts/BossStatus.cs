using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour {

    public int maxHealth;

	// 0 init = start shoot
	// 1 display message
	// 2 shoot again
	public int phase = 0;

    private int health;
    private int power;

    private void Start() {
        health = maxHealth;
        power = 50;
    }

	void Update() {
		PhaseUpdate ();
	}

	public void PhaseUpdate() {
        if (phase == 0 && health < maxHealth / 3 * 2) {
            // Pass to phase 1
            incrementPhase();
            // Pass to phase 2
            StartCoroutine(waitAndIncrementPhase(3));
        }
            if (phase == 2 && health < maxHealth / 3)
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

        health = Mathf.Max(health - damages, 0);
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
