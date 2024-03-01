using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectAbility : MonoBehaviour
{
    [SerializeField] Rigidbody2D soupRb;
    [SerializeField] AbilityManager abilityManagerScript;

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
            soup.GetComponent<SoupMove>().inTutorial = true;
            abilityManagerScript.hasBoots = true;
            Destroy(gameObject);
            SceneManager.LoadScene("Boots Tutorial", LoadSceneMode.Additive);
        }
        if(collision.gameObject.name == "Soup Player" && gameObject.name == "Hook Straw") {
            soupRb.velocity = new Vector2(0, 0);
            soup.GetComponent<SoupMove>().inTutorial = true;
            abilityManagerScript.hasHookStraw = true;
            Destroy(gameObject);
            SceneManager.LoadScene("Hook Straw Tutorial", LoadSceneMode.Additive);
        }
        if(collision.gameObject.name == "Soup Player" && gameObject.name == "Red Straw") {
            soupRb.velocity = new Vector2(0, 0);
            soup.GetComponent<SoupMove>().inTutorial = true;
            abilityManagerScript.hasRedStraw = true;
            Destroy(gameObject);
            SceneManager.LoadScene("Red Straw Tutorial", LoadSceneMode.Additive);
        }

    }
}
