using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float smooth;
    public float DoorOpenAngle;
    private bool open;
    private bool enter;

    private Vector3 defaultRot;
    private Vector3 openRot;

    
    void Start()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
    }

    // Funcion principal
    void Update()
    {
        if (open) //puerta abierta
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else // puerta cerrada
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }
        if (Input.GetKeyDown(KeyCode.F) && enter)
        {
            open = !open;
        }
    }

    private void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 30), "F Para abrir");
        }
    }

    // Activa la funcion principal cuando el personaje esta cerca de la puerta 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    // Desaciva la funcion principal cuando el personaje se aleja de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }
}

