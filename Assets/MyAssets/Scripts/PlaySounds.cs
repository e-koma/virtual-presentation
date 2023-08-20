using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaySounds : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip clap;

    void Start()
    {
        // AudioSourceがアタッチされていることが前提
        audioSource = this.GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (isInputDown())
        {
            audioSource.Play();
        }
    }

    private bool isInputDown()
    {
        if (audioSource.clip == null) {
            return false;
        }

        return Input.GetKeyUp("space") || (Gamepad.current != null && Gamepad.current.dpad.down.wasReleasedThisFrame);
    }
}
