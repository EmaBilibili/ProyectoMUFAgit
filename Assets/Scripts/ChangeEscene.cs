using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeEscene : MonoBehaviour
{
    public string sotano; // Nombre de la siguiente escena

        private void OnMouseDown()
        {
            SceneManager.LoadScene(sotano); // Cargar la siguiente escena
        }

    }
