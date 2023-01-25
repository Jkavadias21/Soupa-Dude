using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupMove : MonoBehaviour
{
    private float dirX;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;


    private Animator anim;

    private enum SoupMovementStates {idle, running, jumping, falling, slidingRight, slidingLeft};

    [SerializeField]
    private LayerMask jumpableSurface;

    [SerializeField]
    private Sprite wallSlideLeft;

    [SerializeField]
    private Sprite wallSlideRight;

    [SerializeField] private Transform wallCheck;

    private bool isWallSliding = false;
    private float wallSlideSpeed = 2f;
     

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    SoupMovementStates currentState;

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if(Input.GetKeyDown("space") && groundCheck()){
            rb.velocity = new Vector2(dirX * 7f, 14f);
        }


        //if (Physics2D.OverlapCircle(wallCheck.position, 0.2f, jumpableSurface))
        //{
        //    isWallSliding = true;
        //    if (dirX > 0f)
        //    {
        //        Debug.Log("left wall slide");
        //        currentState = SoupMovementStates.slidingRight;
        //        rb.velocity = new Vector2(dirX * 7f, -0.5f);
        //        if (Input.GetKeyDown("space"))
        //        {
        //            rb.velocity = new Vector2(7f, 14f);
        //        }
        //    }

        //    if (dirX < 0f)
        //    {
        //        Debug.Log("left wall slide");
        //        currentState = SoupMovementStates.slidingLeft;
        //        rb.velocity = new Vector2(dirX * 7f, -0.5f);
        //        if (Input.GetKeyDown("space")) {
        //            rb.velocity = new Vector2(7f, 14f);
        //        }
        //    }

        //    anim.SetInteger("soupState", (int)currentState);

        //}


        wallSlide();
        setAnimation();
        

    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, jumpableSurface);
    }

    private void wallSlide()
    {
        if(isWalled() && !groundCheck() && dirX != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        else
        {
            isWallSliding = false;
        }
    }

    //Method to set the animation state of the player sprite
    private void setAnimation(){
        SoupMovementStates currentState = 0;

        if(isWalled() && dirX > 0f && !groundCheck())
        {
            currentState = SoupMovementStates.slidingRight;
            sprite.flipX = true;
        }

        else if (isWalled() && dirX < 0f && !groundCheck()) {
            currentState = SoupMovementStates.slidingLeft;
            sprite.flipX = false;
        }

        if (dirX > 0f && !isWalled()){
            sprite.flipX = false;
            currentState = SoupMovementStates.running;
        }

        else if(dirX < 0f && !isWalled()){
            sprite.flipX = true;
            currentState = SoupMovementStates.running;
        }

        if(rb.velocity.y > 0.1f){
            currentState = SoupMovementStates.jumping;
        }

        else if(rb.velocity.y < -0.1f && !isWalled()){
            currentState = SoupMovementStates.falling;
        }

        if(dirX == 0){
            currentState = SoupMovementStates.idle;
        }

        anim.SetInteger("soupState", (int)currentState);
    }

    private bool groundCheck()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface);
    }
}
