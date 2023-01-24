using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupMove : MonoBehaviour
{
    private float dirX;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Animator anim;

    private enum SoupMovementStates {idle, running, jumping, falling};
     

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        


        
    } 

   
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if(Input.GetKeyDown("space")){
            rb.velocity = new Vector2(dirX * 7f, 14f);
        }
        
        setAnimation();
        
        
    }

    private void setAnimation(){
        SoupMovementStates currentState;

        if(dirX > 0f){
            sprite.flipX = false;
            currentState = SoupMovementStates.running;
        }

        else if(dirX < 0f){
            sprite.flipX = true;
            currentState = SoupMovementStates.running;
        } else {
            currentState = SoupMovementStates.idle;
        }

        if(rb.velocity.y > 0.1f){
            currentState = SoupMovementStates.jumping;
        }

        else if(rb.velocity.y < -0.1f){
            currentState = SoupMovementStates.falling;
        }

        anim.SetInteger("soupState", (int)currentState);
            
        

    }
}
