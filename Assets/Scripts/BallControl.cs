using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public Vector2 trajectoryOrigin;
    public float xInitialForce;
    public float yInitialForce;

    void ResetBall(){
        transform.position = Vector2.zero;

        rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall(){
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        float randomDirection = Random.Range(0, 2);
        //if value is less than 1, move left, if not then right
        if(randomDirection < 1.0f){
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    void RestartGame(){
        ResetBall();
        Invoke("PushBall", 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody2D.velocity.magnitude != speed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }
    }
}
