using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EyesSystem : MonoBehaviour
{
    
    public KeyCode switchKey = KeyCode.C;
    public KeyCode VolverOjoDominante = KeyCode.C;
    public GameObject[] objectsToDisable;
    public GameObject[] HiddenObjects;

    

    
    
    void Start()
    {
        
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
            // Activar objetos ocultos
            foreach (GameObject obj in HiddenObjects)
            {
                obj.SetActive(true);
            }
            
      
          
        }
        if (Input.GetKeyDown(VolverOjoDominante))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in HiddenObjects)
            {
                obj.SetActive(false);
            }
        }
            
    }
}
