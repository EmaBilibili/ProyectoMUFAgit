using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Raycast : MonoBehaviour
{
    public int range;
    public Camera camera;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<LightSwitcher>() == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.GetComponent<LightSwitcher>().Light==true)
                    {
                        hit.collider.GetComponent<LightSwitcher>().OnOffLight();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
}
