using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunAnimation : MonoBehaviour
{
    public float moveSpeed = 0.4f;

    private Animator animator;
    private CharacterController charaController;
    private Vector3 moveDirection;
    private float padXValue;
    private float padYValue;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.charaController = this.GetComponent<CharacterController>();
        this.moveDirection = new Vector3(0, 0, 0);
        animator.applyRootMotion = false;
    }

    void LateUpdate()
    {
        if (Gamepad.current == null) return;
        getInputValue();
        MovePosition();
        RotatePosition();
        runAnimation();
    }

    void getInputValue()
    {
        padXValue = Gamepad.current.leftStick.ReadValue().x;
        padYValue = Gamepad.current.leftStick.ReadValue().y;
        moveDirection.x = padXValue * moveSpeed;
        moveDirection.z = padYValue * moveSpeed;
        moveDirection.y = Physics.gravity.y;
    }

    void MovePosition()
    {
        charaController.Move(moveDirection * Time.deltaTime);
    }

    void RotatePosition()
    {

        if (isMove())
        {
            Vector3 diff = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (diff == Vector3.zero)
            {
                charaController.transform.rotation = Quaternion.identity;
            }
            else
            {
                charaController.transform.rotation = Quaternion.LookRotation(diff);
            }
        }
    }

    void runAnimation()
    {
        if (isMove())
        {
            animator.ResetTrigger("StandTrigger");
            animator.SetTrigger("RunTrigger");
        }
        else
        {
            animator.ResetTrigger("RunTrigger");
            animator.SetTrigger("StandTrigger");
        }
    }

    bool isMove()
    {
        return Gamepad.current.leftStick.ReadValue().magnitude > 0;
    }
}
