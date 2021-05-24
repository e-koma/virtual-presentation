using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyUp("space") && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
