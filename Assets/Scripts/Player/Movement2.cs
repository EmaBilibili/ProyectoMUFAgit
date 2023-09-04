using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float walkSpeed = 5f;             // Velocidad de movimiento caminando
    public float runSpeed = 10f;             // Velocidad de movimiento corriendo
    public float crouchSpeed = 2f;           // Velocidad de movimiento agachado
    public Transform cameraTransform;        // Transform de la cámara principal

    private bool isCrouching = false;        // Indicador de si el jugador está agachado
    private bool isRunning = false;          // Indicador de si el jugador está corriendo

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Obtener la entrada de movimiento horizontal (eje X)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Obtener la entrada de movimiento vertical (eje Z)
        float moveVertical = Input.GetAxis("Vertical");

        // Obtener la dirección de movimiento en relación a la cámara
        Vector3 movementDirection = GetMovementDirection(moveHorizontal, moveVertical);

        // Obtener la velocidad de movimiento según el estado del jugador
        float speed = GetMovementSpeed();

        // Calcular el vector de movimiento
        Vector3 movement = movementDirection * speed;

        // Aplicar el movimiento al Rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private Vector3 GetMovementDirection(float horizontalInput, float verticalInput)
    {
        // Obtener la dirección de movimiento según la rotación de la cámara
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        movementDirection.Normalize();

        return movementDirection;
    }

    private float GetMovementSpeed()
    {
        // Si el jugador está agachado, utiliza la velocidad de movimiento agachado
        if (isCrouching)
        {
            return crouchSpeed;
        }

        // Si el jugador está corriendo, utiliza la velocidad de movimiento corriendo
        if (isRunning)
        {
            return runSpeed;
        }

        // Si no está agachado ni corriendo, utiliza la velocidad de movimiento caminando
        return walkSpeed;
    }

    public void Crouch()
    {
        isCrouching = true;
    }

    public void StandUp()
    {
        isCrouching = false;
    }

    public void Run()
    {
        isRunning = true;
    }

    public void Walk()
    {
        isRunning = false;
    }
}
