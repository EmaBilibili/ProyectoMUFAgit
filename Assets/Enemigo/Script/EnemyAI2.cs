using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI2 : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float maxDistance = 10.0f;
    public float visionAngle = 45.0f;
    public float visionDistance = 20.0f;
    public float idleTimeMin = 1.0f;
    public float idleTimeMax = 5.0f;
    public float rotateSpeed = 120.0f;

    private Rigidbody rb;
    private Transform target;

    private bool isChasing = false;
    private float idleTime = 0.0f;
    private Vector3 randomDirection;
    private float targetAngle;
    private bool isLook = false;
    private Vector3 isLookPosition;

    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        randomDirection = GetRandomDirection();

        
    }

    void FixedUpdate()
    {
        Vector3 directionToTarget = target.position - transform.position;

        // Comprueba si el objetivo está dentro del campo de visión del enemigo
        if (Vector3.Angle(directionToTarget, transform.forward) < visionAngle && 
            directionToTarget.magnitude < visionDistance)
        {
            
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, directionToTarget, visionDistance);
            
            RaycastHit hit = hits[0];
            if (!hits[0].transform.GetComponent<Muro>())
            {
                isChasing = false;
                if (hit.transform.CompareTag("Player"))
                {
                    // Mueve al enemigo hacia el objetivo
                    rb.MovePosition(transform.position + transform.forward * moveSpeed);

                    // Rota al enemigo hacia el objetivo
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed));

                    // Establece isChasing en verdadero cuando el objetivo está dentro del campo de visión y alcance
                    isChasing = true;

                    // Reinicia el tiempo de espera
                    idleTime = 0.0f;
                    
                    isLook = true;
                    isLookPosition = hit.transform.position;

                }
            }
            else
            {
                isChasing = false;
                MoviendoRandom();
            }
           
            // Dibuja el raycast en el editor
            Debug.DrawRay(transform.position, directionToTarget, Color.red);
        }
        else if (isChasing && isLook)
        {
            // Si el enemigo estaba persiguiendo pero perdió de vista al objetivo, continúa moviéndote en la última dirección conocida
            rb.MovePosition(transform.position + isLookPosition);
            Debug.Log("perdio de vista");
            
        }
        else
        {
            isLook = false;
            MoviendoRandom();
        }
        
    }

    private void MoviendoRandom()
    {
        // Si no está persiguiendo, muévete en una dirección aleatoria durante un tiempo aleatorio
        rb.MovePosition(transform.position + randomDirection * moveSpeed);
        
        idleTime += Time.deltaTime;
        if (idleTime > GetRandomIdleTime())
        {
            idleTime = 0.0f;
            randomDirection = GetRandomDirection();
            targetAngle = Random.Range(0.0f, 360.0f);
        }
        Quaternion targetRotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * rotateSpeed));
        Debug.Log("moviendo random");
    }
    private Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0.0f;
        randomDirection.Normalize();

        return randomDirection;
    }

    private float GetRandomIdleTime()
    {
        return Random.Range(idleTimeMin, idleTimeMax);
    }

    // Draw the field of view gizmo in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        Vector3 leftVisionDirection = Quaternion.Euler(0, -visionAngle, 0) * transform.forward;
        Vector3 rightVisionDirection = Quaternion.Euler(0, visionAngle, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftVisionDirection * visionDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightVisionDirection * visionDistance);
    }
}


