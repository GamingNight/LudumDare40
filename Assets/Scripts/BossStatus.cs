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
    private AudioSource takeDamageAudioSource;
    private AudioSource deathAudioSource;
    private bool isDying;
    private bool deathAudioHasBeenStarted;

    private void Start() {
        health = maxHealth;
        power = 15;
        nbShoot = 0;
        foreach (AudioSource src in GetComponents<AudioSource>()) {
            if (src.clip.name == "monster-pain") {
                takeDamageAudioSource = src;
            } else if (src.clip.name == "monster-death") {
                deathAudioSource = src;
            }
        }
        isDying = false;
        deathAudioHasBeenStarted = false;
    }

    void Update() {
        PhaseUpdate();
        if (isDying) {
            DestroyRemainingBullets();
            if (!deathAudioSource.isPlaying) {
                if (!deathAudioHasBeenStarted) {
                    deathAudioSource.Play();
                    deathAudioHasBeenStarted = true;
                } else {
                    GameManager.GetInstance().Winner();
                }
            }
        }
    }

    public void PhaseUpdate() {
        // player power must be 1 so maxHealth = the exact number of shoot to kill the boss
        if (phase == 0 && nbShoot > maxHealth / 3) {
            // Pass to phase 1
            incrementPhase();
            // Pass to phase 2
            StartCoroutine(waitAndIncrementPhase(3));
        }
        if (phase == 2 && nbShoot > maxHealth * 2 / 3) {
            // Pass to phase 3
            incrementPhase();
            // Pass to phase 4
            StartCoroutine(waitAndIncrementPhase(3));
        }
    }

    public void incrementPhase() {

        //Debug.Log("Boss change phase "  + phase + " to " + (phase+1));
        phase = phase + 1;
    }

    IEnumerator waitAndIncrementPhase(float timeScale) {
        yield return new WaitForSeconds(timeScale);
        incrementPhase();

    }

    public void TakeDamages(int damages) {
        nbShoot++;
        health = Mathf.Max(health - damages, 0);
        healthUI.rectTransform.localScale = new Vector3((float)health / maxHealth, healthUI.rectTransform.localScale.y, healthUI.rectTransform.localScale.z);
        //Debug.Log("Player takes " + damages + " damages! Remaining life = " + health);
        if (!takeDamageAudioSource.isPlaying)
            takeDamageAudioSource.Play();
        if (health <= 0) {
            isDying = true;
        }
    }

    public void AddPower(int value) {

        power += value;
    }

    public int GetPower() {
        return power;
    }

    void DestroyRemainingBullets() {

        foreach (Transform child in transform) {
            if (child.GetComponent<Bullet>() != null)
                Destroy(child.gameObject);
        }
    }
}
