using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMain : MonoBehaviour {

    public void Load() {
        GameManager.killedInhabitants = 0;
        SceneManager.LoadScene("main");
    }
}
