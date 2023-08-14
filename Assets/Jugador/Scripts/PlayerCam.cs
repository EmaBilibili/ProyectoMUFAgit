using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Variables para el balanceo de la cámara
    public float bobAmount = 0.05f;
    public float bobSpeed = 1.5f;
    private float timer = 0.0f;
    private Vector3 originalCameraPosition; // La posición original de la cámara
    
    //Referencia al codigo PlayerMovement//
    public PlayerMovement playerMovement;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        originalCameraPosition = transform.localPosition;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        //Obteniendo el estado del player//
        PlayerMovement.Movementstate playerState = playerMovement.state;

        // Simular el balanceo de la cámara
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if ((Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f) && playerState != PlayerMovement.Movementstate.crouching)
        {
            if (playerState == PlayerMovement.Movementstate.sprinting)
            {
                timer += bobSpeed * 1.5f * Time.deltaTime; // Ajusta la velocidad de balanceo durante el sprint
            }
            else
            {
                timer += bobSpeed * Time.deltaTime;
            }

            transform.localPosition = originalCameraPosition + new Vector3(
                Mathf.Sin(timer) * bobAmount,
                Mathf.Cos(timer * 2) * bobAmount * 0.5f,
                0f
            );
        }
        else
        {
            timer = 0f;
            transform.localPosition = originalCameraPosition;
        }
    }
    
}