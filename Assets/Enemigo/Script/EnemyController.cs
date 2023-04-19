
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints
    public float moveSpeed = 3f; // Velocidad de movimiento
    public float rotationSpeed = 3f; // Velocidad de rotación
    public float minDistance = 0.1f; // Distancia mínima al waypoint para considerarlo alcanzado

    private int currentWaypoint = 0; // Waypoint actual
    private NavMeshAgent navMeshAgent; // Componente NavMeshAgent del enemigo
    private Rigidbody rb; // Componente Rigidbody del enemigo

    public GameObject player; // Referencia al objeto jugador
    public float playerDetectionDistance = 5f; // Distancia de detección del jugador
    bool isChasingPlayer = false; // Variable para controlar si el enemigo está persiguiendo al jugador
    public float chaseSpeed = 5.0f;

    public float fieldOfViewAngle = 5;
    public float viewDistance = 5;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent.enabled = false; // Deshabilitamos el NavMeshAgent para controlar el movimiento con físicas
    
        player = GameObject.FindWithTag("Player"); // Buscamos el objeto jugador
    }
    

    void FixedUpdate()
    {
        Debug.Log(isChasingPlayer);
        // Calculamos la dirección al jugador
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // Verificamos si el jugador está dentro del ángulo de visión y distancia de detección
        if (Vector3.Angle(transform.forward, directionToPlayer) <= fieldOfViewAngle * 0.5f &&
            directionToPlayer.magnitude <= playerDetectionDistance)
            
        {
            // Comenzamos a perseguir al jugador
            isChasingPlayer = true;

            // Detenemos al enemigo
            rb.velocity = Vector3.zero;

            // Dibujamos la línea de detección en el editor
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else
        {
            // Dejamos de perseguir al jugador
            isChasingPlayer = false;

            // Dibujamos la línea de detección en el editor
            Debug.DrawLine(transform.position, transform.position + transform.forward * playerDetectionDistance, Color.red);
        }

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            MoveToWaypoint();
        }
    }
    void MoveToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypoint].position;
        Vector3 currentPosition = transform.position;
        // Calculamos la dirección y la distancia al waypoint
        Vector3 direction = targetPosition - currentPosition;
        float distance = direction.magnitude;
        // Si estamos cerca del waypoint, pasamos al siguiente
        if (distance <= minDistance)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            return;
        }
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    // Comenzamos a perseguir al jugador
                    isChasingPlayer = true;

                    // Detenemos al enemigo
                    rb.velocity = Vector3.zero;
                    return;
                }
            }
        }
        // Calculamos la rotación hacia el waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Rotamos y movemos el enemigo con físicas
        rb.MoveRotation(rotation);
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }
    void ChasePlayer()
    {
        // Calculamos la dirección y la distancia al jugador
        Vector3 direction = player.transform.position - transform.position;

        // Calculamos la rotación hacia el jugador
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Rotamos y movemos el enemigo con físicas
        rb.MoveRotation(rotation);
        rb.MovePosition(transform.position + direction.normalized * chaseSpeed * Time.deltaTime);
    }
    void OnDrawGizmos()
    {
        // Dibujamos una línea que representa la dirección del enemigo
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);

        // Dibujamos un arco que representa el campo de visión del enemigo
        Gizmos.color = Color.red;
        Vector3 leftDir = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDir * viewDistance);
        Gizmos.DrawRay(transform.position, rightDir * viewDistance);
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
        Gizmos.DrawRay(transform.position + leftDir * viewDistance, rightDir * viewDistance - leftDir * viewDistance);
    }
}
