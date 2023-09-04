using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCollider : MonoBehaviour
{
    public static bool maniquiDetectado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Realiza una acción cuando se detecta la presencia de un enemigo
            maniquiDetectado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Realiza una acción cuando el objeto colisionado ha salido del área del collider
            maniquiDetectado = false;
        }
    }
}