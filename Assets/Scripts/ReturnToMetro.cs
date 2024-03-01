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
        if(gameObject.tag == "endItem") {
            soup.GetComponent<SoupMove>().enabled = true;
            SceneManager.UnloadSceneAsync("Level 1");
        }
    }
}
