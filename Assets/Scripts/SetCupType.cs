using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCupType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private AnimatorOverrider overrider;
    [SerializeField] AbilityManager abilityManagerScript;
    

    public void Set(int value)
    {
        overrider.SetAnimation(overrideControllers[value]);
    }

    public void Update()
    {
        //Debug.Log("level 2");

        if(abilityManagerScript.hasHookStraw) {
            Set(0);
        }
        if(abilityManagerScript.hasBoots) {
            Set(1);
        }
        if(abilityManagerScript.hasRedStraw) {
            Set(2);
        }
        if(abilityManagerScript.hasBelt) {
            Set(2);
        }

    }
}
