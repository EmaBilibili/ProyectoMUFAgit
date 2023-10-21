using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public Transform jugador; // Asigna el objeto del jugador en el inspector.
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Asegúrate de que el jugador y el agente de navegación estén disponibles.
        if (jugador != null && navMeshAgent != null)
        {
            // Establece el destino del agente como la posición actual del jugador.
            navMeshAgent.SetDestination(jugador.position);
        }
    }
}