using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunAnimation : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    private GameObject myGameObject;
    private Vector3 latestPosition;
    private Animator animator;
    private CharacterController charaController;
    private Vector3 moveDirection;
    private float padXValue;
    private float padYValue;

    void Start()
    {
        this.myGameObject = this.gameObject;
        this.animator = this.GetComponent<Animator>();
        this.charaController = this.GetComponent<CharacterController>();
        this.moveDirection = new Vector3(0, 0, 0);
        this.latestPosition = new Vector3(0, 0, 0);
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

        Vector3 diff = moveDirection - latestPosition;
        latestPosition = moveDirection;

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff * Time.deltaTime);
        }
    }
}
