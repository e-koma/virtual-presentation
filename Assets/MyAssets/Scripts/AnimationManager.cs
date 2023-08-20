using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private enum PresenState { BeforeStage, EnterStage, StartPresen, FinishPresen }
    private int presenState = (int)PresenState.BeforeStage;

    // Camera Parameters
    private GameObject cameraObject;
    private Vector3 mainCameraPosition;
    private Vector3 presenCameraPosition;
    private bool enablePresentationMode;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        cameraObject = GameObject.Find("Main Camera");
        mainCameraPosition = cameraObject.transform.position;
        presenCameraPosition = GameObject.Find("PresenCameraPosition").transform.position;
    }

    void LateUpdate()
    {
        getPresentationMode();
        if (enablePresentationMode)
        {
            cameraObject.transform.position = presenCameraPosition;
            return;
        }
        else
        {
            cameraObject.transform.position = mainCameraPosition;
        }

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
        if (Gamepad.current == null)
        {
            ;
        }

        return Input.GetKeyUp("enter") || Input.GetKeyUp("return") || Gamepad.current.bButton.wasReleasedThisFrame;
    }

    void getPresentationMode()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        if (Gamepad.current.yButton.isPressed)
        {
            enablePresentationMode = true;
        }
        else if (Gamepad.current.xButton.isPressed)
        {
            enablePresentationMode = false;
        }
    }
}
