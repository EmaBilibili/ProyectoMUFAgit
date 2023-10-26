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
    
    //MENSAJE EN PANTALLA//
    public string interactionMessage = "Pulsa 'E' para abrir la puerta";
    private GUIStyle messageStyle;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
        
        // MENSAJE EN PANTALLA//
        messageStyle = new GUIStyle();
        messageStyle.fontSize = 34; // Tamaño de fuente
        messageStyle.normal.textColor = Color.white; // Color del texto
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
    
    // MENSAJE EN PANTALLA//
    private void OnGUI()
    {
        if (isPlayerNear)
        {
            Vector2 textSize = messageStyle.CalcSize(new GUIContent(interactionMessage));
            float textX = (Screen.width - textSize.x) / 2;
            float textY = Screen.height - textSize.y - 10; // Ajusta la posición vertical según tus necesidades
            GUI.Label(new Rect(textX, textY, textSize.x, textSize.y), interactionMessage, messageStyle);
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