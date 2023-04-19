using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode switchKey = KeyCode.C;
    public KeyCode VolverOjoDominante = KeyCode.C;
    public GameObject[] objectsToDisable;

    private int currentCameraIndex = 0;

    void Start()
    {
        // desactivar todas las cámaras excepto la primera
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // cambiar de cámara al presionar la tecla switchKey
        if (Input.GetKeyDown(switchKey))
        {
            // desactivar objetos
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
            
      
            // cambiar de cámara
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(VolverOjoDominante))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
            }

            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
