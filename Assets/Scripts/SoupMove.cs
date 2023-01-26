using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool canJump;
    private bool canMove = false;
    private bool groundMove = true;
    private float dirX;
    private float moveSpeed = 7f;
    private float jumpForce = 14f;

    private enum SoupMovementStates { idle, running, jumping, falling, slidingRight, slidingLeft };
    
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
        //due to invoking canMovemethod
        if (groundCheck() || canMove) {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            canMove = false;
            groundMove = true;
        }

        //non wall jumping player movement logic
        if (groundMove)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }


        //jump logic
        if (Input.GetKeyDown("space") && groundCheck())
        {
            rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
        }

        wallSlide();
        wallJump();
        setAnimation();


    }

    private void canMoveMethod()
    {
        canMove = true;
    }

    //checking if the player is in contact with a wall
    private bool isWalled()
    {
        if(Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wall))
        {
            slideType = "leftSlide";
            return true;
        }
        else if(Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wall))
        {
            slideType = "rightSlide";
            return true;
        }

        else
        {
            return false;
        }
    }

    //allows the player to slide slowly down walls
    private void wallSlide()
    {
        
        if (isWalled() && !groundCheck() && dirX != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            Debug.Log("we sliding");
            groundMove = false;
        }

        else
        {
            isWallSliding = false;
        }
    }

    //allows the player to wall jump
    private void wallJump()
    {
        if((isWallSliding || isWalled()) && Input.GetKeyDown("space") && !groundCheck()) {
            isWallJumping = true;
            Debug.Log("double jump");
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            groundMove = false;
            Invoke(nameof(canMoveMethod), 0.3f);
        }

        else
        {
            isWallJumping = false;
        }
    }

    
    //sets the animation state of the player sprite
    private void setAnimation()
    {
        SoupMovementStates currentState = 0;

        //wall jumping animation logic
        if (isWalled() && dirX >= 0f && !groundCheck() && slideType.Equals("rightSlide"))
        {
            currentState = SoupMovementStates.slidingRight;
            sprite.flipX = false;
            wallJumpDirection = -1;
        }

        else if (isWalled() && dirX <= 0f && !groundCheck() && slideType.Equals("leftSlide"))
        {
            currentState = SoupMovementStates.slidingLeft;
            sprite.flipX = false;
            wallJumpDirection = 1;
        }

        //running animation logic
        if (dirX > 0f && !isWalled())
        {
            sprite.flipX = false;
            currentState = SoupMovementStates.running;
        }

        else if (dirX < 0f && !isWalled())
        {
            sprite.flipX = true;
            currentState = SoupMovementStates.running;
        }

        //jumping and falling animation logic
        if (rb.velocity.y > 0.1f)
        {
            currentState = SoupMovementStates.jumping;
        }

        else if (rb.velocity.y < -0.1f && !isWalled())
        {
            currentState = SoupMovementStates.falling;
        }

        //idle animation logic
        if (dirX == 0)
        {
            currentState = SoupMovementStates.idle;
        }

        anim.SetInteger("soupState", (int)currentState);
    }

    //cheking if the player is on the ground
    private bool groundCheck()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface);
    }
}

