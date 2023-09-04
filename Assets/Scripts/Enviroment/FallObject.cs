using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public GameObject objectToThrow;
    public float throwForce = 10f; // Ajusta la fuerza del lanzamiento

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();
            rb.useGravity = true;

            // Calcula la dirección del lanzamiento (hacia adelante desde la pared)
            Vector3 throwDirection = transform.right *-1 * 0.5f; // Puedes ajustar los valores

            // Aplica la fuerza al objeto para lanzarlo
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            
        }
    }
}


