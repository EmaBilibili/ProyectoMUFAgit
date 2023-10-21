using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 3.0f;
    public float gravity = 9.8f; // Gravedad
    private CharacterController characterController;
    private Transform playerCamera;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        // Movimiento del personaje
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = playerCamera.forward * verticalInput + playerCamera.right * horizontalInput;
        moveDirection.y = 0;

        // Rota al personaje hacia la dirección de la cámara
        Quaternion newRotation = Quaternion.LookRotation(playerCamera.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        // Verifica si el personaje está tocando el suelo
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, characterController.height / 2 + 0.1f);

        // Aplica gravedad si no está en el suelo
        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Aplica movimiento al personaje
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}