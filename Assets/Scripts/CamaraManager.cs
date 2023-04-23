using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    
    public KeyCode switchKey = KeyCode.C;
    public KeyCode VolverOjoDominante = KeyCode.C;
    public GameObject[] objectsToDisable;

    
    
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
            
      
          
        }
        if (Input.GetKeyDown(VolverOjoDominante))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
            }

          
        }
        
    }
}
