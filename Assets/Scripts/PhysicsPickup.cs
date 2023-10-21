using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private CinemachineVirtualCamera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObject;
    private Vector3 pickupRotation;

    [SerializeField] private Camera mainCamera; // Referencia a la cámara de Unity

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject.constraints = RigidbodyConstraints.None; // Liberar las restricciones
                CurrentObject = null;
                return;
            }

            // Usar la cámara de Unity para el raycast
            Ray CameraRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

            if (Physics.Raycast(CameraRay, out RaycastHit hitInfo, PickupRange, PickupMask))
            {
                CurrentObject = hitInfo.rigidbody;
                CurrentObject.useGravity = false;
                pickupRotation = CurrentObject.transform.rotation.eulerAngles; // Guardar la rotación actual
                CurrentObject.constraints = RigidbodyConstraints.FreezeRotation; // Congelar la rotación
            }
        }
    }

    private void FixedUpdate()
    {
        if (CurrentObject)
        {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;
            CurrentObject.velocity = DirectionToPoint.normalized * 12f * DistanceToPoint;
        }
    }

    private void LateUpdate()
    {
        if (CurrentObject)
        {
            // Restaurar la rotación original después de mover el objeto
            CurrentObject.transform.rotation = Quaternion.Euler(pickupRotation);
        }
    }
}
