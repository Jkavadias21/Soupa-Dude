using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    private Animator animator;

    private void Awake() {
        Debug.Log("awake");
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
    }
}
