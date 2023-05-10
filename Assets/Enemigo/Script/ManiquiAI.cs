using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManiquiAI : MonoBehaviour
{
    public Transform player; // Objeto del jugador
    private NavMeshAgent navMeshAgent; // Componente NavMeshAgent
    private Vector3 lastPlayerPosition; // Última posición conocida del jugador
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastPlayerPosition = player.position;
        //Rigidbody rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (ConeCollider.maniquiDetectado)
        {
            navMeshAgent.isStopped = true; // Detener el objeto de inmediato
            navMeshAgent.SetDestination(player.position); // Establecer el destino en la posición actual del jugador
        }

        else
        {
            ConeCollider.maniquiDetectado = false;
            navMeshAgent.isStopped = false; // Permitir que el objeto siga moviéndose
            moveManiqui(); 
        }
        
    }

    private void moveManiqui()
    {
        
        // Establecer la posición del objetivo como la posición actual del jugador
        navMeshAgent.SetDestination(player.position);
    }
}