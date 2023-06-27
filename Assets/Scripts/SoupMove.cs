using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoupMove : MonoBehaviour
{
    //components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;

    //surface check variables and classes
    [SerializeField] private LayerMask jumpableSurface;
    [SerializeField] private LayerMask wall;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private LayerMask wallFloor;

    //wall sliding variables
    private bool isWallSliding = false;
    private float wallSlideSpeed = 2f;
    private string slideType;

    //wall jumping variables and classes
    [SerializeField] private float wallJumpForce = 18f;
    [SerializeField] private float wallJumpDirection;
    [SerializeField] private Vector2 wallJumpAngle;
    private bool isWallJumping = false;

    //movement variables
    private bool canDoubleJump = false;
    private bool canWallMove = false;
    private bool canMove = true;
    private float dirX;
    private float moveSpeed = 7f;
    private float jumpForce = 14f;
    private bool isJumping = false;

    private bool doubleJumping = false;
    

    private enum SoupMovementStates { idle, running, jumping, falling, slidingRight, slidingLeft, doubleJump};
    
    void Start()
    {
        //getting components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        wallJumpAngle.Normalize();
    }

    //SoupMovementStates currentState;

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        //wall jumping player movement logic(on a timer
        //due to invoking canWallMovemethod
        if (!SceneManager.GetActiveScene().name.Equals("Level 1")) {
            if (groundCheck() || canWallMove)
            {
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
                canWallMove = false;
                canMove = true;
            }
        }

        //non wall jumping player movement logic
        if (canMove)
        {
            
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        //jump logic
        if ((Input.GetKeyDown("space") && groundCheck()))
        {
            rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
            canDoubleJump = true;
            isJumping = true;
            //coll.size = new Vector2(1.84f, 1.65f);
        }



        //double jump logic
        if(Input.GetKeyDown("f") && canDoubleJump && !isWallSliding)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
            doubleJumping = true;
            canDoubleJump = false;
            isJumping = true;
            
        }

        if (!SceneManager.GetActiveScene().name.Equals("Level 1")) {
            wallSlideCheck();
            wallJump();
        }
    
        setAnimation();


    }

    private void canWallMoveMethod()
    {
        canWallMove = true;
    }

    //checking if the player is in contact with a wall
    private bool isWalled()
    {
        if(Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wall | wallFloor))
        {
            slideType = "leftSlide";
            //canDoubleJump = false;
            return true;
        }
        else if(Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wall| wallFloor))
        {
            slideType = "rightSlide";
            //canDoubleJump = false;
            return true;
        }

        else
        {
            return false;
        }
    }

    //allows the player to slide slowly down walls
    private void wallSlideCheck()
    {
        if (isWalled() && !groundCheck())
        {
            canDoubleJump = true;
            if (dirX > 0f && slideType.Equals("rightSlide"))
            {
                wallSlide();
            }

            else if (dirX < 0f && slideType.Equals("leftSlide"))
            {
                wallSlide();
            }

            else
            {
                canMove = true;
            }
        }

        else
        {
            isWallSliding = false;
        }
    }

    //slows player when in contact with a wall
    private void wallSlide()
    {
        isWallSliding = true;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        canMove = false;
    }

    //allows player to jump when wall sliding
    private void wallJump()
    {
        if((isWallSliding || isWalled()) && Input.GetKeyDown("space") && !groundCheck()) {
            isWallJumping = true;
            //smoother wall jump
            //rb.velocity = new Vector2(4f, 16f);
            //cool ledge boost mechanice(like celeste)
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canMove = false;
            Invoke(nameof(canWallMoveMethod), 0.3f);
        }

        else
        {
            isWallJumping = false;
        }
    }

    bool wasFalling;

    //sets the animation state of the player sprite
    private void setAnimation()
    {
        SoupMovementStates currentState = 0;

        //wall jumping animation logic
        //(only happens when player is not in level 1)
        if (!SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            if (isWalled() && dirX >= 0f && !groundCheck() && slideType.Equals("rightSlide"))
            {
                currentState = SoupMovementStates.slidingRight;
                sprite.flipX = false;
                doubleJumping = false;
                wallJumpDirection = -1;
            }

            else if (isWalled() && dirX <= 0f && !groundCheck() && slideType.Equals("leftSlide"))
            {
                currentState = SoupMovementStates.slidingLeft;
                sprite.flipX = false;
                doubleJumping = false;
                wallJumpDirection = 1;
            }
        }

        //running animation logic
        if (dirX > 0f && groundCheck())
        {
            sprite.flipX = false;
            currentState = SoupMovementStates.running;
        }

        else if (dirX < 0f && groundCheck())
        {
            sprite.flipX = true;
            currentState = SoupMovementStates.running;
        }
        
        //idle animation logic
        if (dirX == 0 && rb.velocity.y == 0)
        {
            currentState = SoupMovementStates.idle;
        }

        //jumping and falling animation logic
        if (rb.velocity.y > 0.1f && !doubleJumping && !isWalled())
        {
            currentState = SoupMovementStates.jumping;
        }

        else if (rb.velocity.y < -0.1f && (!isWalled() || isWalled() && dirX == 0))
        {
            doubleJumping = false;
            currentState = SoupMovementStates.falling;
        }

        //double jumping animation logic
        if (doubleJumping && !isWalled())
        {
            currentState = SoupMovementStates.doubleJump;
            wasFalling = false;
            if(dirX < -0.1f)
            {
                sprite.flipX = true;
            } else
            {
                sprite.flipX = false;
            }
            
        }


        Debug.Log(currentState);
        anim.SetInteger("soupState", (int)currentState);
    }

    

    //cheking if the player is on the ground
    private bool groundCheck()
    {
        if(Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface | wallFloor)){
            canDoubleJump = true;
            isJumping = false;
            return true;
        } 
       
        else {
            return false;
        }
        
            
    }
}

