using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static int killedInhabitants;

    public static GameManager GetInstance() {

        return instance;
    }

    void Awake() {

        if (instance == null)
            instance = this;
    }


    public void GameOver() {
        SceneManager.LoadScene("gameover");
    }

    public void Winner() {
        SceneManager.LoadScene("winner");
    }
}
