using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    private bool isPaused = false;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if(isPaused) {
            // Pause the game
            Time.timeScale = 0f;
            menu.SetActive(true);
        }
        else {
            // Resume the game
            Time.timeScale = 1f;
            menu.SetActive(false);
        }
    }

    public void loadTutorial() {
        SceneManager.LoadScene("Tutorial Select");
    }
    public void loadLevel1() {
        SceneManager.LoadScene("Level 1");
    }
    public void loadLevel2() {
        SceneManager.LoadScene("Level 2");
    }
    public void loadLevel3() {
        SceneManager.LoadScene("Level 3");
    }
    public void loadMenu() {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void closeGame() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

}
