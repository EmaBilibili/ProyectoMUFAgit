using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementOpenDoor : MonoBehaviour
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
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorBasementClose"))
            {
                doorAnimator.SetTrigger("DoorBasementOpen");
                PlaySound(openSound);
            }
            else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorBasementOpen"))
            {
                doorAnimator.SetTrigger("DoorBasementClose");
                PlaySound(closeSound);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("JUGADOR EN PUERTA");
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
