using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    public int range;
    public Camera camera;
    private bool isLightSwitcherHit = false; // Variable para controlar si el raycast ha golpeado el LightSwitcher

    private void Update()
    {
        RaycastHit hit;
        isLightSwitcherHit = false; // Reiniciar la variable en cada actualización
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<LightSwitcher>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.GetComponent<LightSwitcher>().Light == true)
                    {
                        hit.collider.GetComponent<LightSwitcher>().OnOffLight();

                        
                    }
                }
                isLightSwitcherHit = true; // Marcar que el raycast ha golpeado el LightSwitcher
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }

    private void OnGUI()
    {
        if (isLightSwitcherHit)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 34; // Tamaño de fuente más grande

            float messageWidth = 400; // Ancho del área de la etiqueta
            float messageHeight = 100; // Alto del área de la etiqueta

            // Calcula la posición centrada en la parte inferior
            float xPos = (Screen.width - messageWidth) / 2;
            float yPos = Screen.height - messageHeight - 20; // 20 píxeles desde la parte inferior

            Rect messageRect = new Rect(xPos, yPos, messageWidth, messageHeight);

            GUI.Label(messageRect, "Presiona 'E' para interactuar", style);
        }
    }

    
}
