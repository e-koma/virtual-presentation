﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunAnimation : MonoBehaviour
{
    public float moveSpeed = 0.4f;
    public GameObject cameraObject;
    public GameObject cameraTarget;
    public AudioClip[] jumpAudioClips;

    private Animator animator;
    private CharacterController charaController;
    private Vector3 cameraForward;
    private Vector3 moveDirection;
    private float leftPadXValue;
    private float leftPadYValue;

    // Camera Parameters
    private Vector3 cameraOffset;
    private Quaternion initialCameraRotation;
    private bool enablePresentationMode;
    private Transform presenCameraTransform;

    // Audio
    private AudioSource jumpAudioSource;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.charaController = this.GetComponent<CharacterController>();
        this.moveDirection = new Vector3(0, 0, 0);
        enablePresentationMode = false;
        cameraOffset = cameraObject.transform.position - charaController.transform.position;
        initialCameraRotation = cameraObject.transform.rotation;
        presenCameraTransform = GameObject.Find("PresenCameraPosition").transform;

        jumpAudioSource = this.transform.Find("JumpAudio").gameObject.GetComponent<AudioSource>();

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

    void MovePosition()
    {
        // leftPadXValue = Gamepad.current.leftStick.ReadValue().x;
        // leftPadYValue = Gamepad.current.leftStick.ReadValue().y;

        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveDirection = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
        moveDirection.x *= moveSpeed;
        moveDirection.z *= moveSpeed;
        moveDirection.y = Physics.gravity.y;

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
        animator.ResetTrigger("GreetTrigger");
        animator.ResetTrigger("JumpTrigger");

        if (isJump())
        {
            animator.SetTrigger("JumpTrigger");

            if (!jumpAudioSource.isPlaying)
            {
                jumpAudioSource.PlayOneShot(jumpAudioClips[Random.Range(1, jumpAudioClips.Length)]);
            }
        }
        else if (isMove())
        {
            animator.ResetTrigger("StandTrigger");
            animator.SetTrigger("RunTrigger");
        }
        else
        {
            animator.ResetTrigger("RunTrigger");
            animator.SetTrigger("StandTrigger");

            if (Gamepad.current.bButton.isPressed)
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

    bool isJump()
    {
        if (Gamepad.current.aButton.isPressed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
