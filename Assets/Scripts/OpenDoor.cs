using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isPlayerNear = false;
    private AudioSource doorAudioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                doorAnimator.SetTrigger("Open");
                PlaySound(openSound);
            }
            else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            {
                doorAnimator.SetTrigger("Close");
                PlaySound(closeSound);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            doorAudioSource.clip = clip;
            doorAudioSource.Play();
        }
    }
}