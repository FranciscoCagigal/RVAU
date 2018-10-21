using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour {

    Vector3 velocity;
    [Range(0,1)]
    public float speed = 0.1f;
    private bool state = false;

	// Use this for initialization
	void Start () {
        StartNewBall();
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
                StartNewBall();
                return;
            case "Bounds South":
                StartNewBall();
                return;
            case "Paddle1":
                Debug.Log("colision " + collision.contacts[0].normal);
                //this.velocity.z *= -1f;
                this.velocity = Reflect(this.velocity, collision.contacts[0].normal);
                this.velocity.y = 0;
                return;
            case "Paddle2":
                this.velocity.z *= -1f;

                return;
            case "CPUPaddle":
                Debug.Log("colision " + collision.contacts[0].normal);
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

