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
    public float restartLevelDelay = 1f;
    public AudioClip eatSound;

    private Animator animator;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        
    }

    private void CheckIfGameOver()
    {
        if (health <= 0f)
        {
            GameManager.instance.GameOver();
        }
    }


	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        
        foodText.text = "Food: " + health;

        health = 100f;

        Invoke("ScaleFish", 1f);
	}

    void ScaleFish()
    {
        transform.localScale = new Vector3(health / 100f, health / 100f, 0);
        Invoke("ScaleFish", 1f);
    }


    private void Restart()
    {
        Debug.Log("Restarting");
     //   Application.LoadLevel(Application.loadedLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene("baseScene");
    }


    void Update()
    {
        health = health - healthLossRate * Time.fixedTime;
        foodText.text = "Food:" + (int)health;
        CheckIfGameOver();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit")
        {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("Restart", restartLevelDelay);

            //Disable the player object since level is over.
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            score += pointsPerFood;
            health += 10;
            transform.localScale = new Vector3(health / 100f, health / 100f, 0);
            SoundManager.instance.RandomizeSfx(eatSound);

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

        if (horizontal > 0)
        {
            animator.SetBool("movingRight", true);
            animator.SetBool("movingLeft", false);
        }
        else
        {
            animator.SetBool("movingRight", false);
            animator.SetBool("movingLeft", true);
        }

        if(horizontal != 0)
        {
        //    vertical = 0;
        }

        velocity.x = horizontal;
        velocity.y = vertical;

        if (horizontal != 0 || vertical != 0)
            playerRigidBody.AddForce(new Vector2(horizontal*2f, vertical*2f));
        //    playerRigidBody.MovePosition(playerRigidBody.position + velocity * Time.fixedDeltaTime);
     //   if(horizontal !=0 || vertical != 0)
       //     transform.position = new Vector3(transform.position.x + horizontal* playerSpeed * Time.deltaTime, transform.position.y + vertical * playerSpeed * Time.deltaTime, 0f);
	}
}
