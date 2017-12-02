using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour {

    public int maxHealth;

    private int health;
    private int power;

    private void Start() {

        health = maxHealth;
        power = 50;
    }

    public void TakeDamages(int damages) {

        health = Mathf.Max(health - damages, 0);
        Debug.Log("Player takes " + damages + " damages! Remaining life = " + health);
        if (health <= 0) {
            GameManager.GetInstance().Winner();
        }
    }

    public void TakePower(int powerPoint) {

        power += powerPoint;
    }
}
