using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    public float viewRadius; // El radio de visión del enemigo
    public float viewAngle; // El ángulo de visión del enemigo
    //public float speed; // La velocidad de movimiento del enemigo
    public Transform[] waypoints; // Los puntos de patrulla
    private int currentWaypointIndex = 0; // El índice del punto de patrulla actual
    private NavMeshAgent agent; // El componente NavMeshAgent
    public Transform playerTransform; // El transform del jugador

    // [SerializeField] private PlayerData playerData;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtenemos el componente NavMeshAgent del objeto
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Buscamos el objeto del jugador en la escena
        //agent.SetDestination(waypoints[currentWaypointIndex].position); // Establecemos el primer punto de patrulla como destino
    }

    void Update()
    {
        if (CanSeePlayer()) // Si el jugador está dentro del radio y ángulo de visión del enemigo
        {
            agent.SetDestination(playerTransform.position); // Establecemos la posición del jugador como destino
        }
        else if (agent.remainingDistance < 0.5f) // Si el enemigo ha llegado a su destino
        {
            currentWaypointIndex++; // Avanzamos al siguiente punto de patrulla
            if (currentWaypointIndex >= waypoints.Length) // Si hemos llegado al final de la lista de puntos de patrulla
            {
                // playerData.lifes += 1;
                currentWaypointIndex = 0; // Volvemos al primer punto de patrulla
            }
            agent.SetDestination(waypoints[currentWaypointIndex].position); // Establecemos el siguiente punto de patrulla como destino
        }
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < viewRadius) // Si el jugador está dentro del radio de visión del enemigo
        {
            Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized; // Obtenemos la dirección hacia el jugador
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer); // Calculamos el ángulo hacia el jugador
            if (angleToPlayer < viewAngle / 2f) // Si el jugador está dentro del ángulo de visión del enemigo
            {
                if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hitInfo, viewRadius)) // Si no hay obstáculos entre el enemigo y el jugador
                {
                    if(hitInfo.collider.gameObject.transform == playerTransform) // Si el objeto detectado es el jugador
                    {
                        
                        return true; // El jugador es visible
                    }
                }
            }
        }
        return false; // El jugador no es visible
    }
    void OnDrawGizmosSelected()
    {
        // Dibuja el radio de visión del enemigo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Dibuja el cono de visión del enemigo
        Gizmos.color = Color.red;
        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2f, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2f, transform.up) * transform.forward * viewRadius;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        // Dibuja los puntos de patrulla del enemigo
        Gizmos.color = Color.blue;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.2f);
        }
    }
}




