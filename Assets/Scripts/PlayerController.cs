using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Transform playertf; //player's transform
    private float halfHeight; //half character's height
    private bool canJump;
    public float speed;
    public float jumpForce;
    public float stopwatch;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playertf = GetComponent<Transform>();
        halfHeight = 0.5f;
        rb2d = GetComponent<Rigidbody2D>();  
        canJump = true;

    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
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

    void OnCollisionStay2D(Collision2D other){

        if(rb2d.velocity.y == 0)
        {
            canJump = true;
        }
        if(Input.GetKey("space"))
        {
            if(canJump == true)
            {
                canJump = false;
                Jump();
            }
        }
    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        return;
    }

}
