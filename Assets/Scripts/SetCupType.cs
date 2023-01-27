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

    public void Awake()
    {

        if (SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            Set(0);
        }
        
    }
}
