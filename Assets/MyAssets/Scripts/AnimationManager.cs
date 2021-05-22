using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private enum PresenState { BeforeStage, EnterStage, StartPresen, FinishPresen }
    private int presenState = (int)PresenState.BeforeStage;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    void LateUpdate()
    {
        switch (presenState)
        {
            case (int)PresenState.BeforeStage:
                if (isClickEnter())
                {
                    animator.applyRootMotion = true;
                    animator.SetTrigger("EnterStage");
                    presenState = (int)PresenState.EnterStage;
                }
                break;
            case (int)PresenState.EnterStage:
                if (isClickEnter())
                {
                    animator.applyRootMotion = false;
                    animator.SetTrigger("StartPresen");
                    presenState = (int)PresenState.StartPresen;
                }
                break;
            case (int)PresenState.StartPresen:
                if (isClickEnter())
                {
                    animator.applyRootMotion = true;
                    animator.SetTrigger("FinishPresen");
                    presenState = (int)PresenState.FinishPresen;
                }
                break;
            default:
                break;
        }
    }

    bool isClickEnter()
    {
        return Input.GetKeyUp("enter") || Input.GetKeyUp("return");
    }
}
