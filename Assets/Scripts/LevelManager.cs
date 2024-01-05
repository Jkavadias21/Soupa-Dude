using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Level 1")) {
            levelNumber = 1;
        }
        if(SceneManager.GetActiveScene().name.Equals("Level 2")) {
            levelNumber = 2;
        }
        if(SceneManager.GetActiveScene().name.Equals("Level 3")) {
            levelNumber = 3;
        }
    }

    //transition between levels upon item collection
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Soup Player" || Input.GetKeyDown("t")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
