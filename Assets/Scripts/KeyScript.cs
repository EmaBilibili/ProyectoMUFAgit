using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public OpenDoor openDoor;
    public DialogueNotes dialogueNotes;
    private bool isInRange = false;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            openDoor.isUnlocked = true;
            Destroy(gameObject);
            Destroy(dialogueNotes);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
