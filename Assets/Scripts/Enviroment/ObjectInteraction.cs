using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float raycastDistance = 5f; // Distancia del raycast, editable en el editor
    private bool isGrabbing = false;
    private Transform grabbedObject;
    private Vector3 initialGrabOffset;
    private Rigidbody grabbedRigidbody;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGrabbing)
        {
            TryGrabObject();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isGrabbing)
        {
            ReleaseObject();
        }

        if (isGrabbing && grabbedObject != null)
        {
            UpdateGrabbedObjectPosition();
        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * raycastDistance, Color.blue);
    }

    private void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                isGrabbing = true;
                grabbedObject = hit.collider.transform;
                initialGrabOffset = grabbedObject.position - Camera.main.transform.position;

                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.useGravity = false;
                    grabbedRigidbody.isKinematic = true;
                }
            }
        }
    }

    private void ReleaseObject()
    {
        isGrabbing = false;
        grabbedObject = null;

        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.useGravity = true;
            grabbedRigidbody.isKinematic = false;
        }
    }

    private void UpdateGrabbedObjectPosition()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * initialGrabOffset.magnitude;
        grabbedObject.position = targetPosition;
    }
}
