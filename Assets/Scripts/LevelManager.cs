using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Hook Straw Tutorial")) {
            levelNumber = 1;
        }
        if(SceneManager.GetActiveScene().name.Equals("Hook Straw Tutorial")) {
            levelNumber = 2;
        }
        if(SceneManager.GetActiveScene().name.Equals("Boots Tutorial")) {
            levelNumber = 3;
        }
        if(SceneManager.GetActiveScene().name.Equals("Red Straw Tutorial")) {
            levelNumber = 4;
        }
    }

    //transition between levels upon item collection
    /*private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Soup Player" || Input.GetKeyDown("t")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }*/
    

    

    //use collect items scripts to collect and keep track of items




}
