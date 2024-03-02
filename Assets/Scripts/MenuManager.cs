using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] AbilityManager abilityManagerScript;
    [SerializeField] GameObject[] TutorialButtons;
    private bool isPaused = false;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        /*if(PlayerPrefs.GetInt("hasBoots", 0) == 1) {
            TutorialButtons[0].SetActive(true);
        }
        if(PlayerPrefs.GetInt("hasHookStraw", 0) == 1) {
            TutorialButtons[1].SetActive(true);
        }
        if(PlayerPrefs.GetInt("hasRedStraw", 0) == 1) {
            TutorialButtons[2].SetActive(true);
        }*/
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
    public void loadHookStrawTutorial() {
        SceneManager.LoadScene("Hook Straw Tutorial");
    }
    public void loadBootsTutorial() {
        SceneManager.LoadScene("Boots Tutorial");
    }
    public void loadRedStrawTutorial() {
        SceneManager.LoadScene("Red Straw Tutorial");
    }
    public void loadMenu() {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void closeGame() {
        PlayerPrefs.DeleteAll();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

}
