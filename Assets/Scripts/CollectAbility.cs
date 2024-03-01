using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectAbility : MonoBehaviour
{
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
        if(collision.gameObject.name == "Soup Player" && gameObject.tag == "collectable") {
            soup.GetComponent<SoupMove>().enabled = false;
            Destroy(gameObject);
            SceneManager.LoadScene("level 1", LoadSceneMode.Additive);
        }
    }
}
