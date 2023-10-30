using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMainToSotano : MonoBehaviour
{
    public string nombreDeEscena; // Nombre de la escena a la que deseas cambiar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" al tag de tu objeto si es diferente
        {
            SceneManager.LoadScene(nombreDeEscena);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
