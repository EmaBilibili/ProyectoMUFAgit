using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class luzParpadeando : MonoBehaviour
{
    public Animator LaLuz;

    private void OnTriggerEnter(Collider other)
    {
        LaLuz.Play("animacion luz parpadeante");
    }

    private void OnTriggerExit(Collider other)
    {
        LaLuz.Play("luz normal");
    }
}

