using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadros : MonoBehaviour
{
    public Rigidbody cuadro; // Solo un cuadro
    public float fuerza = 1000f;

    public Collider cuadroCollider; // Collider que deseas deshabilitar

    void Start()
    {
        // Desactivar la física inicialmente
        cuadro.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Otra condición de activación
        {
            cuadro.isKinematic = false; // Permitir que las físicas afecten al cuadro
            cuadro.AddForce(Vector3.right * fuerza); // Aplicar fuerza hacia la derecha
            cuadroCollider.enabled = false;
        }
    }
}
