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

    private int testCounter = 0;

    private Collision lastColision;

    public Text scoreText;
    public GameObject paddle1, paddle2;

	// Use this for initialization
	void Start () {
        scoreText.enabled = false;
        gameManager.init();
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
            testCounter++;
        }
    }


    //depois de animação de ponto terminar, começa um novo jogo chamando esta função
    public void animationCallback()
    {
        scoreText.enabled = false;
        if (!gameManager.InscrementScore(lastColision.transform.name))//incrementa o score no UI
            return;
        Physics.IgnoreCollision(lastColision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false); //"designorar" a colisão pra sabermos quando volta a haver ponto
        StartNewBall(); //posiciona a bola e começa um novo jogo
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        switch (collision.transform.name)
        {
            case "Bounds Right": //contactos laterais
                this.velocity.x *= -1f;
                return;
            case "Bounds Left": //contactos laterais
                this.velocity.x *= -1f;
                return;
            case "Bounds North": //CPU ou player 2 marcou - depende do tipo de jogo
                if (ApplicationModel.gameType == "cpuVSplayer")
                    scoreText.text = "CPU scores!!!";
                else if (ApplicationModel.gameType == "playerVSplayer")
                    scoreText.text = "Player 2 scores!!!";
                scoreText.GetComponent<Animator>().SetTrigger("ScoreAnimation"); //trigger animacao
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>()); //ignorar a colisao com a barreira para q a bola continue o caminho - sugestao do professor
                lastColision = collision;
                return;
            case "Bounds South":  //Player1 marcou
                scoreText.text = "Player 1 scores!!!";
                scoreText.GetComponent<Animator>().SetTrigger("ScoreAnimation");
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>()); //ignorar a colisao com a barreira para q a bola continue o caminho - sugestao do professor
                lastColision = collision;
                return;
            case "Paddle1": //contacto entre paddle do jogador e bola
                this.velocity = Reflect(this.velocity.normalized, new Vector3(collision.contacts[0].normal.z*-1,0, collision.contacts[0].normal.x)) * speed; //aqui esta o bug
                this.velocity.y = 0;
                return;
            case "Paddle2": //contacto entre paddle do jogador e bola
                this.velocity = Reflect(this.velocity, new Vector3(collision.contacts[0].normal.z * -1, 0, collision.contacts[0].normal.x)); //aqui esta o bug
                this.velocity.y = 0;
                return;
            case "CPUPaddle": //contacto entre paddle do cpu e bola
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

    public void switcheroo()
    {
        var rendererComponents = gameObject.transform.parent.gameObject.GetComponentsInChildren<Renderer>(true);
        paddle2.transform.localPosition = new Vector3(paddle2.transform.localPosition.x, paddle2.transform.localPosition.y, paddle2.transform.localPosition.z * -1);
        paddle1.transform.localPosition = new Vector3(paddle1.transform.localPosition.x, paddle1.transform.localPosition.y, paddle1.transform.localPosition.z * -1);
        foreach (var component in rendererComponents)
        {
            if(component.name == "Bounds North" || component.name == "Bounds South")
            {
                component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, component.transform.localPosition.z * -1);
            }
        }
    }

    public void rotatePaddle(int paddle)
    {
        GameObject paddleObj = null;

        if(paddle == 1)
        {
            paddleObj = paddle2;
        }
        else if(paddle == 2)
        {
            paddleObj = paddle1;
        }

        paddleObj.transform.Rotate(new Vector3(0, 90, 0));

        StartCoroutine(restorePaddle(paddleObj, 2));
    }

    private IEnumerator restorePaddle(GameObject paddle,int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        paddle.transform.Rotate(new Vector3(0, -90, 0));
    }

}

