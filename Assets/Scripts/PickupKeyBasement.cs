using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKeyBasement : MonoBehaviour
{
    private bool isInRange = false;
    public DoorBasementLock doorBasementLock;
    public AudioSource keyPickupSound; // Agrega esta variable

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            doorBasementLock.hasKey = true;
            PlayKeyPickupSound(); // Llama a la función para reproducir el sonido
            gameObject.SetActive(false); // O puedes destruirla con Destroy(gameObject)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Puedes mostrar un mensaje al jugador para indicar que presione "E" para recoger la llave
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // Oculta el mensaje al jugador
        }
    }
    private void PlayKeyPickupSound()
    {
        if (keyPickupSound != null)
        {
            keyPickupSound.Play();
        }
    }
}
