using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadros : MonoBehaviour
{
    public Rigidbody cuadro; // Solo un cuadro
    public float fuerza = 1000f;

    public Collider cuadroCollider; // Collider que deseas deshabilitar
    
    public AudioSource golpeSound; // Arrastra aquí el componente AudioSource desde el Inspector

    void Start()
    {
        // Desactivar la física inicialmente
        cuadro.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cuadro.isKinematic = false;
        
            // Obtén la dirección local hacia adelante del objeto
            Vector3 localForward = transform.forward;
        
            // Aplica la fuerza en la dirección local
            cuadro.AddForce(localForward * fuerza);

            cuadroCollider.enabled = false;

            if (golpeSound != null)
            {
                golpeSound.Play(); // Reproduce el sonido de golpe si se asignó en el Inspector
            }
        }
    }
}
