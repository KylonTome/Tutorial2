using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    private int lives = 3;
    public Text livesText;
    public Text loseText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text = lives.ToString();
        loseText.text = "";

         musicSource.clip = musicClipOne;
        musicSource.Play();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rigidbody2D.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }


    void Update()

  {     if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

          if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

           if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
         if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Jump", true);
        }
         if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
  }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject); 
             if (scoreValue == 8)
            {
            transform.position = new Vector2(77.0f, 0.2f);
            lives = 3;
            livesText.text = lives.ToString();
            }
        }


        if (scoreValue == 15)
        {
            winText.text = "You win! Game created by Kylon Tome!";

            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            musicSource.volume = 0.2f;
            Destroy (this);

        }


        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livesText.text = lives.ToString();
            Destroy(collision.collider.gameObject);
        }


        if (lives <= 0)
        {
            loseText.text = "You lose!";
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rigidbody2D.AddForce(new Vector2(0,3),ForceMode2D.Impulse);
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    
}
