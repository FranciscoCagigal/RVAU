using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour {

    public GameManager gameManager;

    Vector3 velocity;
    [Range(0,1)]
    public float speed = 0.1f;
    private bool state = false;

    private string lastColision;

    public Animation anim;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        StartNewBall();
        scoreText.enabled = false;
    }

    void StartNewBall()
    {
        transform.position = new Vector3(0,0.05f,0);
        transform.localPosition = new Vector3(0, 0.05f, 0);
        //to decide which direction does the ball go when it begins
        float random_direction = Random.Range(0, 2) * 2f - 1f;
        float random_degree = (Random.Range(0, 2) * 2f - 1f) * Random.Range(0.2f, 1f);
        velocity = new Vector3(random_degree, 0, random_direction);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (state)
        {
            this.velocity = this.velocity.normalized * speed;
            transform.localPosition += this.velocity;
        }
    }

    public void animationCallback()
    {
        scoreText.enabled = false;
        gameManager.InscrementScore(lastColision);
        StartNewBall(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        switch (collision.transform.name)
        {
            case "Bounds Right":
                this.velocity.x *= -1f;
                return;
            case "Bounds Left":
                this.velocity.x *= -1f;
                return;
            case "Bounds North":
                if (ApplicationModel.gameType == "cpuVSplayer")
                    scoreText.text = "CPU scores!!!";
                else if (ApplicationModel.gameType == "playerVSplayer")
                    scoreText.text = "Player 2 scores!!!";
                scoreText.GetComponent<Animator>().SetTrigger("ScoreAnimation");
                lastColision = collision.transform.name;
                return;
            case "Bounds South":
                scoreText.text = "Player 1 scores!!!";
                scoreText.GetComponent<Animator>().SetTrigger("ScoreAnimation");
                lastColision = collision.transform.name;
                return;
            case "Paddle1":
                this.velocity = Reflect(this.velocity, collision.contacts[0].normal);
                this.velocity.y = 0;
                return;
            case "Paddle2":
                this.velocity = Reflect(this.velocity, collision.contacts[0].normal);
                this.velocity.y = 0;
                return;
            case "CPUPaddle":
                this.velocity.z *= -1f;
                return;
        }
    }

    public void changeState(bool state)
    {
        this.state = state;
    }

    public static Vector3 Reflect(Vector3 vector, Vector3 normal)
    {
        return vector - 2 * Vector3.Dot(vector, normal) * normal;
    }

}

