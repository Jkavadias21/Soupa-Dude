using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCupType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private AnimatorOverrider overrider;
    

    public void Set(int value)
    {
        overrider.SetAnimation(overrideControllers[value]);
    }

    public void Update()
    {
        //Debug.Log("level 2");

        if(SceneManager.GetActiveScene().name.Equals("Level 2")) {
            Set(0);
        }
        if(
            SceneManager.GetActiveScene().name.Equals("Level 3")) {
            Set(1);
        }
        if(
            SceneManager.GetActiveScene().name.Equals("Level 4")) {
            Debug.Log("here");
            Set(2);
        }

    }
}
