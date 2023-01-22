using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupMove : MonoBehaviour
{
    private float dirX;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Animator anim;

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
        if(dirX > 0f){
            sprite.flipX = false;
            anim.SetBool("running", true);
        }

        else if(dirX < 0f){
            sprite.flipX = true;
            anim.SetBool("running", true);
        } else {
            anim.SetBool("running", false);
        }

        if(rb.velocity.y > 0.1f){
            anim.SetBool("jumping", true);
        }

        else if(rb.velocity.y < -0.1f){
            anim.SetBool("falling", true);
        } else {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
            
        }

    }
}
