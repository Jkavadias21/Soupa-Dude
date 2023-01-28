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
        Debug.Log("level 2");

        if (!SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            Set(0);
        }
        
    }
}
