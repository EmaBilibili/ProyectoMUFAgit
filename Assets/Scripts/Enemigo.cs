using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public Transform jugador; // Asigna el objeto del jugador en el inspector.
    public float maxListenDistance = 2f; // Distancia máxima a la que se pueden escuchar los sonidos.
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    public AudioSource movimientoSound1Source;
    public AudioSource movimientoSound2Source;

    public AudioClip movimientoSound1;
    public AudioClip movimientoSound2;
    public AudioClip screamSound;

    public Cinemachine.CinemachineVirtualCamera VCamPlayer;
    public Cinemachine.CinemachineVirtualCamera VCamEnemy;

    private bool EnemyMoving = true;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Asegúrate de que los AudioSource estén configurados correctamente.
        // movimientoSound1Source = gameObject.AddComponent<AudioSource>();
        // movimientoSound2Source = gameObject.AddComponent<AudioSource>();
        movimientoSound1Source.clip = movimientoSound1;
        movimientoSound2Source.clip = movimientoSound2;

        // Configura los AudioSource para que sean 3D.
        movimientoSound1Source.spatialBlend = 1.0f;
        movimientoSound2Source.spatialBlend = 1.0f;

        // Configura la distancia máxima de audición para los sonidos.
        movimientoSound1Source.maxDistance = maxListenDistance;
        movimientoSound2Source.maxDistance = maxListenDistance;
    }

    void Update()
    {
        if (jugador != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(jugador.position);

            // Reproducir ambos sonidos de movimiento cuando el enemigo se mueve.
            PlayMovementSounds();
        }
    }

    private void PlayMovementSounds()
    {
        if (!movimientoSound1Source.isPlaying && EnemyMoving == true)
        {
            movimientoSound1Source.Play();
        }
        if (!movimientoSound2Source.isPlaying && EnemyMoving == true)
        {
            movimientoSound2Source.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == jugador.gameObject)
        {
            Debug.Log("JugadorAtacado");

            // Detener los sonidos de movimiento cuando el enemigo toca al jugador.
            movimientoSound1Source.Stop();
            movimientoSound2Source.Stop();

            // Cambiar la prioridad de las cámaras de Cinemachine.
            VCamPlayer.enabled = false;
            VCamEnemy.Priority = 11;

            // Reproducir el sonido del screamer cuando el enemigo toca al jugador.
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1.0f; // Configura el nuevo AudioSource como 3D.
            audioSource.maxDistance = maxListenDistance; // Configura la distancia máxima de audición.
            audioSource.PlayOneShot(screamSound);

            // Realizar acciones cuando el enemigo toca al jugador.
            // Por ejemplo, reproducir una animación.
            anim.SetTrigger("ScreamAnimation");

            EnemyMoving = false;
        }
    }
}
