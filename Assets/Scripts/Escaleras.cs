using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escaleras : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que quieres cambiar
    private bool isPlayerNear = false; // Variable para saber si el jugador est� cerca

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ChangeScene();
        }
    }

    // Si el jugador entra en el �rea del objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // Mostrar mensaje para indicar al jugador que puede presionar "E" para interactuar
        }
    }

    // Si el jugador sale del �rea del objeto
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // Ocultar el mensaje al jugador
        }
    }

    // Funci�n para cambiar de escena
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
