using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetRound : MonoBehaviour
{
    private ManageGame gameManagement;
    public GameObject currentBall;

    public bool canReset = true;

    [Header("Ball Parameters")]
    public GameObject ballPrefab;
    public Vector3 ballSpawnPos = new Vector3(0, 0, 0);
    public bool spawnDirection;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    void Start()
    {
        gameManagement = this.gameObject.GetComponent<ManageGame>();
    }

    public void runCoroutineReset(int input) {
        if (gameManagement.gameActive == true) {StartCoroutine(resetRound(input)); }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0); 
        }
    }

    public IEnumerator resetRound(int winnerNo) {

        if (winnerNo == 1) { player1Win(); }  // Player 1 Wins   
        else if (winnerNo == 2) { player2Win(); } //Player 2 Wins
        else { Debug.LogError("Invalid Player Number"); }

        yield return new WaitForSeconds(0.5f);

        currentBall = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        ballDirection();
    }

    void player1Win() 
    {
        gameManagement.score_Player1++;
        gameManagement.UpdateScore();
        spawnDirection = false;
        IncreaseBotSpeed();
    }

    void player2Win() // or bot
    {
        gameManagement.score_Player2++;
        gameManagement.UpdateScore();
        spawnDirection = true;
        DecreaseBotSpeed();
    }

    void ballDirection() {
        if (spawnDirection == true) {
            currentBall.GetComponent<BallController>().up = true;
            currentBall.GetComponent<BallController>().left = true;
        } else if (spawnDirection == false) {
            currentBall.GetComponent<BallController>().up = true;
            currentBall.GetComponent<BallController>().right = true;
        }
    }

    void DecreaseBotSpeed() {
        if (GameObject.Find("Bot") != null)        
        {
            GameObject.Find("Bot").GetComponent<MovementController>().DecreaseSpeedMultiplier();
        }
    }

    void IncreaseBotSpeed() {
        if (GameObject.Find("Bot") != null)        
        {
            GameObject.Find("Bot").GetComponent<MovementController>().IncreaseSpeedMultiplier();
        }
    }
}
