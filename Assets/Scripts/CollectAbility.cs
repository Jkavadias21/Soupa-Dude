using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectAbility : MonoBehaviour
{
    [SerializeField] Rigidbody2D soupRb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] GameObject soup;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Soup Player" && gameObject.name == "Boots") {
            soupRb.velocity = new Vector2(0, 0);
            soup.GetComponent<SoupMove>().enabled = false;
            Destroy(gameObject);
            SceneManager.LoadScene("Boots Tutorial", LoadSceneMode.Additive);
        }
        if(collision.gameObject.name == "Soup Player" && gameObject.name == "Hook Straw") {
            soup.GetComponent<SoupMove>().enabled = false;
            Destroy(gameObject);
            SceneManager.LoadScene("Hook Straw Tutorial", LoadSceneMode.Additive);
        }
    }
}
