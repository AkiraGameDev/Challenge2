using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    public float speed;
    public float jumpForce;

    Rigidbody2D rb2d;
    Transform playertf; //player's transform
    bool canJumpAgain;
    bool hasWallJumped;
    bool canJump;
    float stopwatch;
    public float dashCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playertf = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        canJump = true;
        canJumpAgain = false;
        dashCooldown = 0;
    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        if(!hasWallJumped && !canJump && canJumpAgain && Input.GetKeyDown("space"))
        {
            Jump();
            canJumpAgain = false;
            hasWallJumped = true;   
        }
        animationUpdates();
    }

    void FixedUpdate()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(movementHorizontal, 0);

        if(Mathf.Abs(rb2d.velocity.x) < speed/2)
        {
            if(canJump){rb2d.AddForce(movement * speed);}
            if(!canJump){rb2d.AddForce(movement * speed *0.5f);}        
        }
        if(Input.GetKey("s") && !canJump && dashCooldown <= 0)
        {
            animator.SetBool("IsSprint", true);
            rb2d.AddForce(movement * speed * 30);
            dashCooldown = 3;
        }
        if(Input.GetKey("left shift") && canJump)
        {
            speed = 20;
        }
        else
        {
            speed = 10;
        }

        if(Mathf.Abs(rb2d.velocity.y) > 3 && !animator.GetBool("IsJumping") )
        {
            animator.SetBool("IsJumping", true);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(rb2d.velocity.y == 0)
        {
            canJumpAgain = false;
            hasWallJumped = false;
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
        animator.SetBool("IsFalling", false);
    }

    void OnCollision2D(Collision2D other)
    {
        if(Mathf.Abs(other.GetContact(0).normal.x) > 0.95)
        {
            canJumpAgain = true;
        }
    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        return;
    }

    void animationUpdates()
    {
    //animator.SetFloat("Speed", rb2d.velocity.x);
        if(rb2d.velocity.x > 0.01 && !animator.GetBool("IsEPress"))
        {
            animator.SetBool("IsEPress", true);
            animator.SetBool("IsAPress", false);
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(rb2d.velocity.x < -0.01 && !animator.GetBool("IsAPress"))
        {
            animator.SetBool("IsAPress", true);
            animator.SetBool("IsEPress", false);
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(rb2d.velocity.x < 0.01 && rb2d.velocity.x > -0.01 )
        {
            animator.SetBool("IsAPress", false);
            animator.SetBool("IsEPress", false);
        }
    }

    public void ResetJump()
    {
        canJumpAgain = true;
    }
}
