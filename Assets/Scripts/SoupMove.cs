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
    bool canWallJump = true;

    //movement variables
    private bool canDoubleJump = false;
    private bool canWallMove = false;
    private bool canMove = true;
    private float dirX;
    private float moveSpeed = 7f;
    private float jumpForce = 14f;
    private bool isJumping = false;
    private bool doubleJumping = false;
    bool isDashing = false;
    bool canDash = true;
    Vector2 dashForceRight = new Vector2(15, 0);
    Vector2 dashForceLeft = new Vector2(-15, 0);

    [SerializeField] LevelManager levelManager;

    private enum SoupMovementStates { idle, running, jumping, falling, slidingRight, slidingLeft, doubleJump};
    
    void Start() {
        //getting components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        wallJumpAngle.Normalize();
    }
    
    void Update()
    {
        //player movement
        wallJumpMove();
        walk();
        jump();
        
        setAnimation();

        activateAbilities();
    }
    
    //activate abilities associated with current level 
    public void activateAbilities() {
        if(levelManager.levelNumber >= 0) {
            wallSlideCheck();
            wallJump();
        }
        if(levelManager.levelNumber >= 0) {
            doubleJump();
        }
        if(levelManager.levelNumber >= 0) {
            dash();
        }

    }

    //-----------------------------------------movement--------------------------------------------------
    
    //non wall jumping player movement logic
    public void walk() {
        dirX = Input.GetAxisRaw("Horizontal");

        if(canMove && !isDashing) {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
    }
    
    //wall jumping player movement logic
    public void wallJumpMove(){
        if(groundCheck() || canWallMove) {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            canWallMove = false;
            canMove = true;
        }
        
    }

    //dash logic
    public void dash(){
        if (Input.GetKeyDown("g") && canDash){
            rb.velocity = new Vector2(0, 0);
            
            if (dirX < -0.1f){
                rb.AddForce(dashForceLeft, ForceMode2D.Impulse);
            }

            else if(dirX > 0.1f){
                rb.AddForce(dashForceRight, ForceMode2D.Impulse);
            }

            else{
                rb.AddForce(dashForceRight, ForceMode2D.Impulse);
            }

            rb.gravityScale = 0;
            Invoke("addGravity", 0.25f);
            canDash = false;
            isDashing = true;
        }
    }
    
    //jump logic
    public void jump(){
        if (Input.GetKeyDown("space") && groundCheck()){
            rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
            canDoubleJump = true;
            isJumping = true;
        }
    }

    //double jump logic
    public void doubleJump(){
        
        if(Input.GetKeyDown("f") && canDoubleJump && !isWallSliding && !groundCheck()) {
            rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
            doubleJumping = true;
            canDoubleJump = false;
            isJumping = true;
            //if wall jump is on timer then this removes celeste jump from ground
            //if wall jump is not on timer then this removes celeste jump
            canWallJump = false;
            Invoke("enableWallJump", 0.25f);
        }
    }

    //reapply gravity to the player
    public void addGravity(){
        rb.gravityScale = 4.5f;
        if (dirX > 0.1f) {
            rb.velocity = new Vector2(1, 0);
        }
        else {
            rb.velocity = new Vector2(-1, 0);
        }
        isDashing = false;
    }

    //---------------------------------------wall movement------------------------------------------------
    public void canWallMoveMethod() {
        canWallMove = true;
    }

    //check if the player is in contact with a wall
    public bool isWalled() {
        if(Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wall | wallFloor)){
            slideType = "leftSlide";
            canDash = true;
            return true;
        }
        else if(Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wall| wallFloor)) {
            slideType = "rightSlide";
            canDash = true;
            return true;
        }
        else {
            return false;
        }
    }

    //wall slide logic
    public void wallSlideCheck() {
        if (isWalled() && !groundCheck()) {
            canDoubleJump = true;
            if (dirX > 0f && slideType.Equals("rightSlide")) {
                wallSlide();
                isWallSliding = true;
            }
            else if (dirX < 0f && slideType.Equals("leftSlide")) {
                wallSlide();
                isWallSliding = true;
            }
            else {
                canMove = true;
            }
        }
        else {
            isWallSliding = false;
        }
    }

    //slow walled player
    private void wallSlide() {
        isWallSliding = true;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        canMove = false;
    }

    //wall slide jumping logic
    private void wallJump() {
        if((isWallSliding || isWalled()) && Input.GetKeyDown("space") && !groundCheck() && canWallJump && dirX != 0) {
            isWallJumping = true;
            Debug.Log(wallJumpAngle + "" + wallJumpDirection);
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canMove = false;
            Invoke(nameof(canWallMoveMethod), 0.3f);


            //wall jump delay, lower timer means easier celeste jumping
            //will feel smoother if you delay the time before player can double jump after leaving the wall
            //make sure to specify that as soup approahces the wall after dashing if they then jump when hitting the wall
            //they will get a boost
            //enabling this makes celeste jump possible with double jump delay enabled
            //canWallJump = false;
            //Invoke("enableWallJump", 0.525f);
        }
        else {
            isWallJumping = false;
        }
    }
    
    public void enableWallJump() {
        canWallJump = true;
    }
    
    //set the animation state of the player sprite
    private void setAnimation() {
        SoupMovementStates currentState = 0;

        //wall jumping animation logic
        //(only happens when player is not in level 1)
        if (!SceneManager.GetActiveScene().name.Equals("Level 1")) {
            if (isWalled() && dirX > 0f && !groundCheck() && slideType.Equals("rightSlide")) {
                currentState = SoupMovementStates.slidingRight;
                sprite.flipX = false;
                doubleJumping = false;
                wallJumpDirection = -1;
            }
            else if (isWalled() && dirX < 0f && !groundCheck() && slideType.Equals("leftSlide")) {
                currentState = SoupMovementStates.slidingLeft;
                sprite.flipX = false;
                doubleJumping = false;
                wallJumpDirection = 1;
            }
        }

        //running animation logic
        if (dirX > 0f && groundCheck()) {
            sprite.flipX = false;
            currentState = SoupMovementStates.running;
        }
        else if (dirX < 0f && groundCheck()) {
            sprite.flipX = true;
            currentState = SoupMovementStates.running;
        }
        
        //idle animation logic
        if (dirX == 0 && rb.velocity.y == 0) {
            currentState = SoupMovementStates.idle;
        }

        //jumping and falling animation logic
        if (rb.velocity.y > 0.1f && !doubleJumping && !isWalled()) {
            currentState = SoupMovementStates.jumping;
        }
else if (rb.velocity.y < -0.1f && (!isWalled() || isWalled() && dirX == 0)) {
            doubleJumping = false;
            currentState = SoupMovementStates.falling;
        }

        //double jumping animation logic
        if (doubleJumping && !isWalled()) {
            currentState = SoupMovementStates.doubleJump;
            if(dirX < -0.1f) {
                sprite.flipX = true;
            } 
            else {
                sprite.flipX = false;
            } 
        }

        anim.SetInteger("soupState", (int)currentState);
    }

    //cheking if the player is on the ground
    private bool groundCheck() {
        if(Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface | wallFloor)){
            canDoubleJump = true;
            isJumping = false;
            canDash = true;
            return true;
        } 
        else {
            return false;
        }
    }
}

