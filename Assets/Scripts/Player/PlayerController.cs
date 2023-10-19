using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 3.0f;
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

        // Aplica movimiento al personaje
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}