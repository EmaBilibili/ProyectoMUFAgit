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
    }

    void Update()
    {
        // Si el jugador se mueve, actualizar su última posición conocida
        if (player.position != lastPlayerPosition)
        {
            lastPlayerPosition = player.position;
        }

        // Establecer la posición del objetivo como la última posición conocida del jugador
        navMeshAgent.SetDestination(lastPlayerPosition);
    }
}