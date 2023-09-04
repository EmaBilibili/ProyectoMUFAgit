using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float stopDistance = 2f;
    public float lookThreshold = 0.9f;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calcular el vector de dirección entre el enemigo y el jugador
        Vector3 directionToPlayer = player.position - transform.position;

        // Calcular el ángulo entre la dirección en la que el enemigo está mirando y el vector de dirección al jugador
        float angleToPlayer = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        // Si el jugador está mirando directamente al enemigo, detenerlo
        if (angleToPlayer > lookThreshold)
        {
            navMeshAgent.isStopped = true;
        }
        // Si el jugador no está mirando directamente al enemigo, moverlo hacia él
        else
        {
            // Si el jugador está dentro del rango de detección, mover al enemigo hacia él
            if (distanceToPlayer < detectionRadius)
            {
                navMeshAgent.SetDestination(player.position);

                if (distanceToPlayer < stopDistance)
                {
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    navMeshAgent.isStopped = false;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
