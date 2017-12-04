using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    public int maxHealth;
    public int maxPower;
    public Image healthUI;
    public Image powerUI;

    private int health;
    private int power;
    private Animator animator;

    private void Start() {

        health = maxHealth;
        power = 1;
        animator = GetComponent<Animator>();
    }

    public void TakeDamages(int damages) {

        health = Mathf.Max(health - damages, 0);
        healthUI.rectTransform.localScale = new Vector3((float)health / maxHealth, healthUI.rectTransform.localScale.y, healthUI.rectTransform.localScale.z);
        Debug.Log("Player takes " + damages + " damages! Remaining life = " + health);
        if (health <= 0) {
            Die();
        }
    }

    public void PowerUp(int value) {
        GameManager.killedInhabitants++;
        power = Mathf.Min(value + power, maxPower);
        powerUI.rectTransform.localScale = new Vector3((float)power / maxPower, powerUI.rectTransform.localScale.y, powerUI.rectTransform.localScale.z);
        Debug.Log("player has been powered up of " + value + " points! Total points = " + power + " from " + GameManager.killedInhabitants + "Friends!");
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetTrigger("death");
    }

    public void GameOverTrigger() {

        GameManager.GetInstance().GameOver();
    }
}
