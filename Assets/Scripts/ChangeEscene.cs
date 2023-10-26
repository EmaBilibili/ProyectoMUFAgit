using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeEscene : MonoBehaviour
{
    public string sotano; 

        private void OnMouseDown()
        {
            SceneManager.LoadScene(sotano);
        }

    }
