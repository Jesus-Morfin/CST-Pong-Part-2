using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Transform ball;
    public float startSpeed = 3f;
    public GoalTrigger leftGoalTrigger;
    public GoalTrigger rightGoalTrigger;
    public TextMeshProUGUI Score;
    public Transform powerUp1;
    public Transform powerUp2;
    public Rigidbody ballBody;
    public AudioSource powerUpCollected;
    public AudioClip collected;
    public AudioSource goalScored;
    public AudioClip cheers;


    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;
    private Vector3 ballStartPos;



    private const int scoreToWin = 11;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = ball.position;
        ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
        
        int x = Random.Range(-6,6);
        int y = 0;
        int z = Random.Range(-4, 4);

        powerUp1.position = new Vector3(x, y, z);

        x = Random.Range(-6,6);
        y = 0;
        z = Random.Range(-4, 4);
        powerUp2.position = new Vector3(x, y, z);


    }

    // If the ball entered the goal area, increment the score, check for win, and reset the ball
    public void OnGoalTrigger(GoalTrigger trigger)
    {
        if (trigger == leftGoalTrigger)
        {
            rightPlayerScore++;

            this.Score.text = "<sprite index=15>" + leftPlayerScore.ToString() + " - " + rightPlayerScore.ToString() +"<sprite index=3>";
            //Debug.Log($"Right player scored: {rightPlayerScore}");
            ball.localScale = new Vector3(0.5f,0.5f,0.5f);

          goalScored.PlayOneShot(cheers);  
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(-1f);
            }
        }
        else if (trigger == rightGoalTrigger)
        {
            
            leftPlayerScore++;
            this.Score.text ="<sprite index=3>" + leftPlayerScore.ToString() + " - " + rightPlayerScore.ToString() +"<sprite index=15>" ;
            //Debug.Log($"Left player scored: {leftPlayerScore}");
            ball.localScale = new Vector3(0.5f,0.5f,0.5f);
            goalScored.PlayOneShot(cheers); 
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(1f);
            }
        }

        UpdateScore();
    }

    void ResetBall(float directionSign)
    {
        ball.position = ballStartPos;

        // Start the ball within 20 degrees off-center toward direction indicated by directionSign
        directionSign = Mathf.Sign(directionSign);
        Vector3 newDirection = new Vector3(directionSign, 0f, 0f) * startSpeed;
        newDirection = Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f) * newDirection;

        var rbody = ball.GetComponent<Rigidbody>();
        rbody.velocity = newDirection;
        rbody.angularVelocity = new Vector3();

        // We are warping the ball to a new location, start the trail over
        ball.GetComponent<TrailRenderer>().Clear();
    }

    public void UpdateScore() {
        //Score.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f,1f),Random.Range(0f, 1f),Random.Range(0f,1f));
        Score.color = new Color32((byte)Random.Range(0, 255), 
        (byte)Random.Range(0, 255),
        (byte)Random.Range(0, 255),
        (byte) 255);
    }

    public void gameChanger()
    {
        powerUpCollected.PlayOneShot(collected);
        ball.localScale = new Vector3(1,1,1);

        if(ball.position.x > 0)
        {
            ballBody.velocity = new Vector3(-1f, 0f, 0f) * startSpeed;
        }
        else
        {
            ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
        }
    }


}
