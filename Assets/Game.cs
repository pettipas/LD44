using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public Controller ctrl;
    public bool Gameoverstarted;

    public void Update() {
        if (ctrl.life >= 8) {
            if (!Gameoverstarted) {
                Gameoverstarted = true;
                StartCoroutine(EndGame());
            }
        }
    }

    public IEnumerator EndGame() {
        
        SceneManager.LoadScene("start");
        yield break;
    }
}
