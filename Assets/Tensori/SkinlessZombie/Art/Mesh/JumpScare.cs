using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public Animator anim; // Referencia al componente Animator que controla la animación
    public GameObject objetoConAnimator; // Referencia al objeto que contiene el Animator
    private AudioSource audioSource; // Referencia al componente AudioSource

    
    
    private void Start()
    {
        // Desactiva el objeto con el Animator al inicio
        objetoConAnimator.SetActive(false);
        audioSource = objetoConAnimator.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el otro objeto sea el jugador
        {
            // Activa el objeto con el Animator
            objetoConAnimator.SetActive(true);
            anim.SetTrigger("JumpScare"); // Activa la animación con el nombre de tu trigger
            
            // Reproduce el sonido
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
