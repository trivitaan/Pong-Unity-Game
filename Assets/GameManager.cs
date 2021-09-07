using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isDebugWindowShown = false; //default value is false

    public Trajectory trajectory;
    //player 1
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody2D;

    //player 2
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;

    //Ball
    public BallControl ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    //max score
    public int maxScore;

    // Start is called before the first frame update
    void Start()
    {
        player1Rigidbody2D = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    void OnGUI(){
        Color oldColor = GUI.backgroundColor;

        //player1's score on top left and player2's score on top right
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART")){
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("Restart Game", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //if player1 wins (reached max score), then show "Player 1 Wins"
        if(player1.Score== maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "Player 1 WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if(player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 100, 2000, 1000), "Player 2 WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if(isDebugWindowShown){
            GUI.backgroundColor = Color.red;
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText = 
                "Ball mass = " + ballMass + "\n" + "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" + 
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" + 
                "Last impulse from player 1 = (" + impulsePlayer1X + ", "+ impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", "+ impulsePlayer2Y + ")\n";

            
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }

        if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO")){
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

