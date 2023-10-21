using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    public GameObject LightObject;
    public GameObject ObjectToDisable;
    public bool LightOnOff;
    public bool Light;

    public void OnOffLight()
    {
        LightOnOff = !LightOnOff;
        if (LightOnOff == true)
        {
            LightObject.SetActive(true);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(true); // Activa el objeto cuando la luz está encendida
            }
        }
        else if (LightOnOff == false)
        {
            LightObject.SetActive(false);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(false); // Desactiva el objeto cuando la luz está apagada
            }
        }
    }
}