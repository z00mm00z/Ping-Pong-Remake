using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject Bot;
    [SerializeField] GameObject audioManager;

    public float speed = 1.0f;
    Vector3 velocity;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    // Update is called once per frame
    void Start() {
        GameManager = GameObject.Find("GameManager");
        Bot = GameObject.Find("Bot");
        audioManager = GameObject.Find("AudioManager");
    }

    void Update()
    {
       if (GameManager.GetComponent<ManageGame>().gameActive == true) {
            UpdateVelocity();
            transform.position += velocity * Time.deltaTime;
            if (Bot != null) { Bot.GetComponent<MovementController>().ballPos = transform.position; }
       }
    }

    void UpdateVelocity() {

        if (right && up) velocity = new Vector3(1f, 1f, 0) * speed;
        else if (right && down) velocity = new Vector3(1f, -1f, 0) * speed;
        else if (left && up) velocity = new Vector3(-1f, 1f, 0) * speed;
        else if (left && down) velocity = new Vector3(-1f, -1f, 0) * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        string side = collision.transform.name;

        if (side == "Top" && up) { up = false; down = true;}
        if (side == "Bottom" && down) { down = false; up = true;}
        if (side == "Player1" && right) { right = false; left = true;}
        if (side == "Player2" && left) { left = false; right = true;}
        if (side == "Bot" && left) { left = false; right = true;}

        if (side == "Right") { GameManager.GetComponent<ResetRound>().runCoroutineReset(2); audioManager.GetComponent<AudioManaager>().playBallDestroy(); Destroy(this.gameObject); } // Player 1 misses ball - Player 2 Wins
        if (side == "Left") { GameManager.GetComponent<ResetRound>().runCoroutineReset(1); audioManager.GetComponent<AudioManaager>().playBallDestroy(); Destroy(this.gameObject); }  // Player 2 misses ball - Player 1 Wins
        
        audioManager.GetComponent<AudioManaager>().playBallHit();
    }
}
