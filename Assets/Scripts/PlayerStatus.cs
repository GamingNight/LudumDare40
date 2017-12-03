using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public int maxHealth;
    public int maxPower;

    private int health;
    private int power;
    private Animator animator;

    private void Start() {

        health = maxHealth;
        power = 10;
        animator = GetComponent<Animator>();
    }

    public void TakeDamages(int damages) {

        health = Mathf.Max(health - damages, 0);
        Debug.Log("Player takes " + damages + " damages! Remaining life = " + health);
        if (health <= 0) {
            Die();
        }
    }

    public void PowerUp(int value) {

        power = Mathf.Min(value + power, maxPower);
		Debug.Log ("player has been powered up of " + value + " points! Total points = " + power);
    }

    public int GetPower() {

        return power;
    }

    private void Die() {

        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShootBullet>().enabled = false;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D c in colliders) {
            c.enabled = false;
        }
        animator.SetTrigger("death");
    }

    public void GameOverTrigger() {

        GameManager.GetInstance().GameOver();
    }
}
