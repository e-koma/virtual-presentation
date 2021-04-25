using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp("enter") || Input.GetKeyUp("return"))
        {
            animator.SetTrigger("ClickEnter");
        }
    }
}
