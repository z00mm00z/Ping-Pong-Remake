using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public GameObject ball;
    public bool gameActive = true;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Player Scores")]
    public int score_Player1;
    public int score_Player2;

    [Header("Reference To Score Displays")]
    public Text scoreText_Player1;
    public Text scoreText_Player2;

    [Header("Stuff that changes on Win")]
    public GameObject audioManager;
    public Text winText;
    public Text continuePromptText;
    public string continuePromptMessage = "Press Any Key To Continue";
    private string winnerName;

    void Start() {
        StartCoroutine("startLate");
    }

    public void UpdateScore() {
        findCurrentBall();
        scoreText_Player1.text = score_Player1.ToString();
        scoreText_Player2.text = score_Player2.ToString();
        checkForWin();
    }

    void checkForWin()
    {
        if (score_Player1 >= 10) {
            stopGame();
            winnerName = "Player 1";
            StartCoroutine("winSequence");
        } else if (score_Player2 >= 10) {
            stopGame();
            if (GameObject.Find("Bot") != null) { winnerName = "The Bot"; } else {  winnerName = "Player 2"; }
            StartCoroutine("winSequence");
        }
    }

    IEnumerator winSequence() {
        bool done = false;
        winText.text = "" + winnerName + " Won!";
        winText.transform.localScale = new Vector3(0.1f, 0.1f, 1);

        for (int i = 0; i <= 30; i++) {
            winText.transform.localScale += new Vector3(0.01f, 0.01f, 1);
            yield return new WaitForSeconds(0.005f);
        }

        yield return new WaitForSeconds(1.0f);

        foreach (char letter in continuePromptMessage.ToCharArray()) { // Animates The Text
            continuePromptText.text += letter;
            audioManager.GetComponent<AudioManaager>().playRandomKey();
            yield return new WaitForSeconds (0.05f);
        }

        while(!done) // essentially a "while true", but with a bool to break out naturally
    {
        if(Input.anyKeyDown)
        {   
            SceneManager.LoadScene(0);
            done = true; // breaks the loop
        }
        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }
        

    }

    void findCurrentBall() {

        if (this.GetComponent<ResetRound>().currentBall != null)
        {
            ball = this.GetComponent<ResetRound>().currentBall;
            //Debug.Log(ball.gameObject.name);
        }
    }

    void stopGame() { // Freezes ball and barrier movemnt
        gameActive = false;
    }

    IEnumerator startLate() {
        gameActive = false;

        yield return new WaitForSeconds(2.0f);

        gameActive = true;
    }
}
