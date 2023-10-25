using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBasementLock : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isPlayerNear = false;
    private bool isLocked = true; // Puerta bloqueada inicialmente
    private AudioSource doorAudioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public GameObject key; // GameObject de la llave
    
    public bool hasKey = false;


    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked || hasKey)
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
            else
            {
                // La puerta está bloqueada y el jugador no tiene la llave
                // Puedes mostrar un mensaje de que la puerta está bloqueada
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
