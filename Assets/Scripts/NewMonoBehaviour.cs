//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SoupMove : MonoBehaviour {
//    private float dirX;

//    private Rigidbody2D rb;
//    private SpriteRenderer sprite;
//    private BoxCollider2D coll;


//    private Animator anim;

//    private enum SoupMovementStates { idle, running, jumping, falling, slidingRight, slidingLeft };

//    [SerializeField]
//    private LayerMask jumpableSurface;
//    [SerializeField] private LayerMask wall;
//    [SerializeField]
//    private Sprite wallSlideLeft;

//    [SerializeField]
//    private Sprite wallSlideRight;

//    [SerializeField] private Transform wallCheckLeft;
//    [SerializeField] private Transform wallCheckRight;

//    private bool isWallSliding = false;
//    private float wallSlideSpeed = 2f;

//    private bool isWallJumping;
//    private float wallJumpingDirection;
//    private float wallJumpingTime = 0.4f;
//    private float wallJumpingCounter;
//    private float wallJumpingDuration = 0.4f;
//    private Vector2 wallJumpPower = new Vector2(20f, 16f);





//    void Start() {
//        rb = GetComponent<Rigidbody2D>();
//        sprite = GetComponent<SpriteRenderer>();
//        anim = GetComponent<Animator>();
//        coll = GetComponent<BoxCollider2D>();
//    }

//    SoupMovementStates currentState;

//    void Update() {
//        dirX = Input.GetAxisRaw("Horizontal");

//        if (!isWallJumping) {
//            Debug.Log(rb.velocity.x);
//            rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
//        }

//        if (Input.GetKeyDown("space") && groundCheck()) {
//            rb.velocity = new Vector2(dirX * 7f, 14f);
//        }

//        wallSlide();
//        wallJump();
//        setAnimation();


//    }

//    private bool isWalled() {
//        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wall)
//            || Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wall);
//    }

//    //private bool isWalled()
//    //{
//    //    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, wall) || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, wall);
//    //}

//    private void wallSlide() {
//        if (isWalled() && !groundCheck() && dirX != 0) {
//            isWallSliding = true;
//            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
//        }

//        else {
//            isWallSliding = false;
//        }
//    }

//    private void wallJump() {
//        if (isWallSliding) {
//            //isWallJumping = false;
//            wallJumpingCounter = wallJumpingDuration;
//            //CancelInvoke(nameof(stopWallJumping));
//        }

//        else {
//            wallJumpingCounter -= Time.deltaTime;
//        }

//        if (Input.GetKeyDown("space") && wallJumpingCounter > 0f) {
//            isWallJumping = true;

//            rb.velocity = new Vector2(-dirX * 10f, wallJumpPower.y);
//            Debug.Log(rb.velocity.x);
//            wallJumpingCounter = 0;
//            Invoke(nameof(stopWallJumping), 0.2f);

//        }



//    }

//    private void stopWallJumping() {
//        isWallJumping = false;
//    }
//    //Method to set the animation state of the player sprite
//    private void setAnimation() {
//        SoupMovementStates currentState = 0;

//        if (isWalled() && dirX > 0f && !groundCheck()) {
//            currentState = SoupMovementStates.slidingRight;
//            sprite.flipX = false;
//            wallJumpingDirection = 1;
//        }

//        else if (isWalled() && dirX < 0f && !groundCheck()) {
//            currentState = SoupMovementStates.slidingLeft;
//            sprite.flipX = false;
//            wallJumpingDirection = -1;
//        }

//        if (dirX > 0f && !isWalled()) {
//            sprite.flipX = false;
//            currentState = SoupMovementStates.running;
//        }

//        else if (dirX < 0f && !isWalled()) {
//            sprite.flipX = true;
//            currentState = SoupMovementStates.running;
//        }

//        if (rb.velocity.y > 0.1f) {
//            currentState = SoupMovementStates.jumping;
//        }

//        else if (rb.velocity.y < -0.1f && !isWalled()) {
//            currentState = SoupMovementStates.falling;
//        }

//        if (dirX == 0) {
//            currentState = SoupMovementStates.idle;
//        }

//        anim.SetInteger("soupState", (int)currentState);
//    }

//    private bool groundCheck() {
//        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface);
//    }
//}
