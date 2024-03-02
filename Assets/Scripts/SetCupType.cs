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
        abilityManagerScript = GameObject.FindWithTag("original soup").GetComponent<SoupMove>().abilityManagerScript;

        Debug.Log(abilityManagerScript);

        if(SceneManager.GetActiveScene().name == "Hook Straw Tutorial" || abilityManagerScript.hasHookStraw) {
            Set(0);
        }
        if(SceneManager.GetActiveScene().name == "Boots Tutorial" || abilityManagerScript.hasBoots) {
            Set(1);
        }
        if(SceneManager.GetActiveScene().name == "Red Straw Tutorial" || abilityManagerScript.hasRedStraw) {
            Set(2);
        }
        if(SceneManager.GetActiveScene().name == "Belt Tutorial" || abilityManagerScript.hasBelt) {
            Set(3);
        }

    }
}
