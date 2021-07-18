using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunAnimation : MonoBehaviour
{
    public float moveSpeed = 5.0f;

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
    }

    void LateUpdate()
    {
        if (Gamepad.current == null) return;

        padXValue = Gamepad.current.leftStick.ReadValue().x;
        padYValue = Gamepad.current.leftStick.ReadValue().y;

        moveDirection.x = padXValue;
        moveDirection.z = padYValue;
        moveDirection.y = Physics.gravity.y;
        charaController.Move(moveDirection * Time.deltaTime);
    }
}
