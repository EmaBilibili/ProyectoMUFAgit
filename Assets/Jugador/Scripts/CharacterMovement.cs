using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private NavMeshAgent agent;
    [SerializeField] private bool isRunning;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Verificar si se presiona la tecla Shift para correr
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            agent.speed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            agent.speed = walkSpeed;
        }

        // Obtener las teclas de movimiento (WASD)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento en relación con la cámara
        Vector3 movementDirection = CalculateMovementDirection(horizontalInput, verticalInput);

        // Verificar si hay una dirección de movimiento válida
        if (movementDirection.magnitude >= 0.1f)
        {
            // Calcular la posición de destino en función de la dirección de movimiento
            Vector3 destination = transform.position + movementDirection;

            // Mover al personaje utilizando NavMesh
            agent.SetDestination(destination);
        }
    }

    private Vector3 CalculateMovementDirection(float horizontalInput, float verticalInput)
    {
        // Obtener la rotación de la cámara en el eje Y
        float cameraRotation = playerCamera.transform.rotation.eulerAngles.y;

        // Convertir la rotación de la cámara en un vector de dirección
        Vector3 cameraForward = Quaternion.Euler(0f, cameraRotation, 0f) * Vector3.forward;

        // Calcular la dirección de movimiento basada en la entrada y la rotación de la cámara
        Vector3 movementDirection = (cameraForward * verticalInput) + (playerCamera.transform.right * horizontalInput);
        movementDirection.y = 0f; // Mantener el movimiento en el plano horizontal

        return movementDirection.normalized;
    }
}

