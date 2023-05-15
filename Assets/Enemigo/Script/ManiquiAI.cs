using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManiquiAI : MonoBehaviour
{
    public Transform player; // Objeto del jugador
    private NavMeshAgent navMeshAgent; // Componente NavMeshAgent
    private Vector3 lastPlayerPosition; // Última posición conocida del jugador
    private Transform playerTransform; // El transform del jugador
    public float viewRadius; // El radio de visión del enemigo
    public float viewAngle; // El ángulo de visión del enemigo

    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastPlayerPosition = player.position;
        playerTransform = player.GetComponent<Transform>();
        //Rigidbody rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (ConeCollider.maniquiDetectado)
        {
            navMeshAgent.isStopped = true; // Detener el objeto de inmediato
            
        }

        else if (ConeCollider.maniquiDetectado==false && CanSeePlayer())
        {
            ConeCollider.maniquiDetectado = false;
            navMeshAgent.isStopped = false; // Permitir que el objeto siga moviéndose
            moveManiqui(); 
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
                    if (hitInfo.collider.gameObject.CompareTag("Player")) // Si el objeto detectado es el jugador
                    {
                        
                        return true; // El jugador es visible
                    }
                }
            }
        }
        return false; // El jugador no es visible
    }

    private void moveManiqui()
    {
        
        // Establecer la posición del objetivo como la posición actual del jugador
        navMeshAgent.SetDestination(player.position);
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

        
    }
}