using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Animator dialog;
    public Controller ctrl;
    public bool Gameoverstarted;

    public List<Enemy> enemys;

    public void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (enemys.Count == 0) {
            if (!Gameoverstarted) {
                Gameoverstarted = true;
                StartCoroutine(WindGame());
            }
        }

        if (ctrl.life >= 8) {
            if (!Gameoverstarted) {
                Gameoverstarted = true;
                StartCoroutine(EndGame());
            }
        }
    }

    public void LateUpdate() {
        enemys.RemoveAll(x => x == null);
    }

    public IEnumerator WindGame() {
        dialog.Play("theend");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("start");
        yield break;
    }

    public IEnumerator EndGame() {
        dialog.Play("gameover");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("start");
        yield break;
    }
}
