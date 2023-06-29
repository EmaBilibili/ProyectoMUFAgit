using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private AudioSource audioSource;
    private Transform playerCamera; // Agrega una variable para almacenar la referencia a la cámara del jugador

    public float verticalOffset = 0.5f; // Ajusta este valor para controlar la altura de la linterna respecto a la cámara

    private void Start()
    {
        flashlight = GetComponentInChildren<Light>();
        audioSource = GetComponentInChildren<AudioSource>();
        playerCamera = Camera.main.transform; // Asigna la referencia a la cámara principal en la escena
    }

    private void Update()
    {
        // Obtiene la rotación de la cámara
        Quaternion cameraRotation = playerCamera.rotation;

        // Asigna la rotación horizontal a la linterna
        transform.rotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        // Calcula la rotación vertical para la linterna
        float cameraPitch = playerCamera.eulerAngles.x;
        float flashlightPitch = cameraPitch > 180f ? cameraPitch - 360f : cameraPitch; // Ajusta el rango del ángulo vertical

        // Aplica la rotación vertical a la linterna
        flashlight.transform.localRotation = Quaternion.Euler(flashlightPitch, 0f, 0f);

        // Aquí puedes añadir lógica para encender/apagar la linterna
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            if (flashlight.enabled)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
