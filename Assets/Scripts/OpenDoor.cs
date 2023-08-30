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

    [SerializeField]public bool isUnlocked;
    
    private AudioSource openSound; // Agregado: referencia al componente AudioSource
    private AudioSource closeSound; // Agregado: referencia al segundo AudioSource para cerrar
    
    void Start()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
        
        // Agregado: obtener referencia al componente AudioSource
        AudioSource[] audioSources = GetComponents<AudioSource>();
        openSound = audioSources[0];
        closeSound = audioSources[1];
        
    }
    // Funcion principal
    void Update()
    {
        if (open) //puerta abierta
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            // cerrarPuerteAuto();
        }
        else // puerta cerrada
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }
        if (Input.GetKeyDown(KeyCode.F) && enter && isUnlocked==true)
        {
            open = !open;
            if (open)
            {
                openSound.Play();
            }
            else
            {
                closeSound.Play(); // Agregado: reproducir el sonido de cerrar puerta
            }
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

