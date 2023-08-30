using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioClip sonidoDePuerta;
    private AudioSource audioSource;
    private bool puedeReproducir = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonidoDePuerta;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && puedeReproducir)
        {
            audioSource.Play();
            puedeReproducir = false;
            Invoke("PermitirReproduccion", 180f); // 180 segundos = 3 minutos
        }
    }

    private void PermitirReproduccion()
    {
        puedeReproducir = true;
    }
}
