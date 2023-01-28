using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoupDie : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Trap")){
            Debug.Log("we died");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("dead");
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("soupDead", true);
        //Invoke("RestartLevel", 0.2f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
