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

    [SerializeField] public bool isUnlocked;

    private AudioSource audioSource; // Referencia al componente AudioSource
    
    // Define las variables para los sonidos de apertura y cierre
    public AudioClip OpenDoorSound;
    public AudioClip CloseDoorSound;
    
    private bool previousOpenState = false; // Variable para rastrear el estado anterior

    void Start()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);

        // Obtén una referencia al componente AudioSource en este objeto
        audioSource = GetComponent<AudioSource>();
    }

    // Función principal
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && enter && isUnlocked == true)
        {
            open = !open;
        
            if (open) // Si la puerta se abre
            {
                // Reproduce el sonido de apertura solo si la puerta estaba cerrada anteriormente
                if (!previousOpenState)
                {
                    audioSource.PlayOneShot(OpenDoorSound);
                }
            }
            else // Si la puerta se cierra
            {
                // Reproduce el sonido de cierre solo si la puerta estaba abierta anteriormente
                if (previousOpenState)
                {
                    audioSource.PlayOneShot(CloseDoorSound);
                }
            }
            previousOpenState = open; // Actualiza el estado anterior
        }

        if (open) // puerta abierta
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else // puerta cerrada
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }
    }


    private void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 30), "F Para abrir");
        }
    }

    // Activa la función principal cuando el personaje está cerca de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    // Desactiva la función principal cuando el personaje se aleja de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }
}

