using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float playerSpeed = 1.0f;
    Rigidbody2D playerRigidBody;
    int score;
    public int pointsPerFood;
    public float health = 100f;
    public float healthLossRate = 1.0f;
    public Text foodText;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }


	// Use this for initialization
	void Start ()
    {
        foodText.text = "Food: " + health;
	}

    void Update()
    {
        health = health - healthLossRate * Time.fixedTime;
        foodText.text = "Food:" + health;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            score += pointsPerFood;
            health += 10;

            transform.localScale = transform.localScale * 1.1f;
            Debug.Log("Other game object is " + other.gameObject);
            other.gameObject.SetActive(false);
        }
    }


	// Update is called once per frame
	void FixedUpdate ()
    {
        int horizontal = 0;
        int vertical = 0;
        Vector2 velocity;

        horizontal = (int)Input.GetAxis("Horizontal");

        vertical = (int)Input.GetAxis("Vertical");

        if(horizontal != 0)
        {
        //    vertical = 0;
        }

        velocity.x = horizontal;
        velocity.y = vertical;

        if (horizontal != 0 || vertical != 0)
            playerRigidBody.MovePosition(playerRigidBody.position + velocity * Time.fixedDeltaTime);
     //   if(horizontal !=0 || vertical != 0)
       //     transform.position = new Vector3(transform.position.x + horizontal* playerSpeed * Time.deltaTime, transform.position.y + vertical * playerSpeed * Time.deltaTime, 0f);
	}
}
