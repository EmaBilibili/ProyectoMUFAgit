using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nombreDeEscena; // Nombre de la escena a la que deseas cambiar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" al tag de tu objeto si es diferente
        {
            SceneManager.LoadScene(nombreDeEscena);
        }
    }
}
