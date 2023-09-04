using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public Transform llave; // El transform del jugador

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == llave)
        {
            SceneManager.LoadScene("Ganaste");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
