using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{

    //TODO Change bot speeds randomly/ Make Bot slower over time
    

    GameObject GameManager;

    public float speed = 10.0f;
    public float botSpeed = 0.01f;
    private float currentSpeed;
    [SerializeField] string state;
    private float speedMultiplier;

    [SerializeField] Text speedMultiplierText;
    [SerializeField] Text stateDisplayText;
    [SerializeField] int TimesLost;

    public Vector3 ballPos;

    public Vector3 ballDiff;

    [Header("Differentiates between inputs")]
    public bool player1;
    public bool player2; 
    public bool bot;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        speedMultiplier = 2.5f;
        TimesLost = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.GetComponent<ManageGame>().gameActive == true) {
            move();
        }
    }

    void move() {

        if (player1 == true) {
            float yMove = Input.GetAxis("Vertical2") * Time.deltaTime * speed;

            transform.Translate(0f, yMove, 0f);

            Vector3 clampedPosition = transform.position;
            // Now we can manipulte it to clamp the y element
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.1f, 4.1f);
            // re-assigning the transform's position will clamp it
            transform.position = clampedPosition;
        }

        if (player2 == true) {
            float yMove = Input.GetAxis("Vertical1") * Time.deltaTime * speed;

            transform.Translate(0f, yMove, 0f);

            Vector3 clampedPosition = transform.position;
            // Now we can manipulte it to clamp the y element
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.1f, 4.1f);
            // re-assigning the transform's position will clamp it
            transform.position = clampedPosition;
        }

        if (bot == true) { 


            speedMultiplierText.text = speedMultiplier.ToString();
            stateDisplayText.text = "State: " + state;

            if ((ballPos.y - transform.position.y < 2 || ballPos.y - transform.position.y > -2) && ballPos.x - transform.position.x < 3f) {
                currentSpeed = botSpeed * speedMultiplier;
                state = "Fast";
            } else {
                currentSpeed = botSpeed;
                state = "Slow";
            }

            if (ballPos.x < 0)
            {
                if (ballPos.y > transform.position.y) {
                    transform.Translate(Vector3.up * currentSpeed);
                } else if (ballPos.y < transform.position.y) {
                    transform.Translate(Vector3.down * currentSpeed);
                }
            } else {
                
                if (transform.position.y > 0) {
                    transform.Translate(Vector3.down * currentSpeed);
                } else if (transform.position.y < 0) {
                    transform.Translate(Vector3.up * currentSpeed);
                }
                
            }

            Vector3 clampedPosition = transform.position;
            // Now we can manipulte it to clamp the y element
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.1f, 4.1f);
            // re-assigning the transform's position will clamp it
            transform.position = clampedPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (bot == true) {
            if(other.gameObject.name == "Ball" || other.gameObject.name == "Ball(Clone)")
            {
                speedMultiplier -= 0.1f;
            }
        }
    }
    
    public void DecreaseSpeedMultiplier() { // Called by Game Manager in ResetRound.cs
        if (speedMultiplier > 0)
        {
            speedMultiplier -= 0.3f;    
        }
        TimesLost = 0;
    }

    public void IncreaseSpeedMultiplier() {
        if (TimesLost >= 3) {
            speedMultiplier += 0.6f;
        } else {
            speedMultiplier += 0.4f;        
        }
        TimesLost++;
    }
    
}
