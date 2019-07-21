using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Transform playertf; //player's transform
    private int score;
    private float halfHeight; //half character's height
    private bool canJump;
    public float speed;
    public float jumpForce;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public float stopwatch;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playertf = GetComponent<Transform>();
        halfHeight = 0.5f;
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        scoreText.text = "Coins: 0 :(";
        winText.text = "";
        livesText.text = "Lives: 3";    
        canJump = true;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", rb2d.velocity.x);
    }

    void FixedUpdate()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(movementHorizontal, 0);

        if(rb2d.velocity.x < 50){
            if(canJump){rb2d.AddForce(movement * speed);}
            if(!canJump){rb2d.AddForce(movement * speed *0.5f);}
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<Collider2D>().tag == "PickUp"){
            other.gameObject.SetActive(false);
            UpdateScore();
        }
    }

    void OnCollisionStay2D(Collision2D other){
        Debug.DrawLine(playertf.position, other.GetContact(0).point, Color.green, 1.0f,false);

        if(rb2d.velocity.y == 0)
        {
            canJump = true;
            animator.SetBool("IsJumping", false);

        }
        if(Input.GetKey("space"))
        {
            if(canJump == true)
            {
                canJump = false;
                animator.SetBool("IsJumping", true);
                Jump();
            }
        }
    }

    void UpdateScore()
    {
        score++;
        scoreText.text = "Coins: " + score.ToString();
        if(score >= 5){
            winText.text = "You got some coins... you win, I guess...";
        }
    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        return;
    }

}
