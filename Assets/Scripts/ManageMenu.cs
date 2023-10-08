using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMenu : MonoBehaviour
{
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadCoOp() {
        StartCoroutine("LoadSceneSlow", 1);
    }

    public void LoadSinglePlayer() {
        StartCoroutine("LoadSceneSlow", 2);
    }

    public void quitGame() {
        Application.Quit();
    }

    IEnumerator LoadSceneSlow(int SceneNumber) {
        yield return new WaitForSeconds(0.15f);

        SceneManager.LoadScene(SceneNumber);
    }
}
