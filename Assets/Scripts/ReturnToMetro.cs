using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMetro : MonoBehaviour
{
    private GameObject soup;
    // Start is called before the first frame update
    void Start()
    {
        soup = GameObject.FindGameObjectWithTag("original soup");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(gameObject.name == "Hook Straw") {
            soup.GetComponent<SoupMove>().inTutorial = false;
            SceneManager.UnloadSceneAsync("Hook Straw Tutorial");
        }

        if(gameObject.name == "Boots") {
            soup.GetComponent<SoupMove>().inTutorial = false;
            SceneManager.UnloadSceneAsync("Boots Tutorial");
        }
        
        if(gameObject.name == "Red Straw") {
            soup.GetComponent<SoupMove>().inTutorial = false;
            SceneManager.UnloadSceneAsync("Red Straw Tutorial");
        }
        
        if(gameObject.name == "Belt") {
            soup.GetComponent<SoupMove>().inTutorial = false;
            SceneManager.UnloadSceneAsync("Belt Tutorial");
        }


    }
}
