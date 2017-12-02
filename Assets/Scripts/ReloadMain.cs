using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMain : MonoBehaviour {

    public void Retry() {

        SceneManager.LoadScene("main");
    }
}
