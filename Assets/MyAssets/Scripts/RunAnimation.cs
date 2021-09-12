using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunAnimation : MonoBehaviour
{
    public float moveSpeed = 0.4f;
    public GameObject cameraObject;
    public GameObject cameraTarget;

    private Animator animator;
    private CharacterController charaController;
    private Vector3 moveDirection;
    private float leftPadXValue;
    private float leftPadYValue;

    // Camera Parameters
    private Vector3 cameraOffset;
    private Quaternion initialCameraRotation;
    private bool enablePresentationMode;
    private Transform presenCameraTransform;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.charaController = this.GetComponent<CharacterController>();
        this.moveDirection = new Vector3(0, 0, 0);
        enablePresentationMode = false;
        cameraOffset = cameraObject.transform.position - charaController.transform.position;
        initialCameraRotation = cameraObject.transform.rotation;
        presenCameraTransform = GameObject.Find("PresenCameraPosition").transform;
    }

    void LateUpdate()
    {
        if (Gamepad.current == null)
        {
            Debug.Log("Gamepadが接続されていません");
            return;
        }

        getPresentationMode();
        if (enablePresentationMode)
        {
            cameraObject.transform.position = presenCameraTransform.position;
            cameraObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            getInputValue();
            MovePosition();
            RotatePosition();
            runAnimation();
            MoveCamera();
        }
    }

    void getPresentationMode()
    {
        if (Gamepad.current.yButton.isPressed)
        {
            enablePresentationMode = true;
        }
        else if (Gamepad.current.xButton.isPressed)
        {
            enablePresentationMode = false;
        }
    }

    void getInputValue()
    {
        leftPadXValue = Gamepad.current.leftStick.ReadValue().x;
        leftPadYValue = Gamepad.current.leftStick.ReadValue().y;
        moveDirection.x = leftPadXValue * moveSpeed;
        moveDirection.z = leftPadYValue * moveSpeed;
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
            animator.ResetTrigger("GreetTrigger");
            animator.ResetTrigger("StandTrigger");
            animator.SetTrigger("RunTrigger");
        }
        else
        {
            animator.ResetTrigger("RunTrigger");
            animator.SetTrigger("StandTrigger");

            if (Gamepad.current.aButton.isPressed)
            {
                animator.SetTrigger("GreetTrigger");
            }
        }
    }

    void MoveCamera()
    {
        cameraObject.transform.position = charaController.transform.position + cameraOffset;
        cameraObject.transform.RotateAround(cameraTarget.transform.position, Vector3.up, Gamepad.current.rightStick.ReadValue().x * 150 * Time.deltaTime);
        cameraObject.transform.LookAt(cameraTarget.transform.position);
        cameraOffset = cameraObject.transform.position - charaController.transform.position;
    }


    bool isMove()
    {
        return Gamepad.current.leftStick.ReadValue().magnitude > 0;
    }
}
