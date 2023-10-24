using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    public GameObject LightObject;
    public GameObject ObjectToDisable;
    public bool LightOnOff;
    public bool Light;
    public float LighttimerDuration; // Duración del temporizador en segundos

    private bool isTimerActive;

    public void OnOffLight()
    {
        LightOnOff = !LightOnOff;
        if (LightOnOff)
        {
            LightObject.SetActive(true);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(true);
            }
            StartTimer();
        }
        else
        {
            LightObject.SetActive(false);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(false);
            }
            StopTimer();
        }
    }

    private void StartTimer()
    {
        isTimerActive = true;
        StartCoroutine(TurnOffLightAfterDelay());
    }

    private void StopTimer()
    {
        isTimerActive = false;
    }

    private IEnumerator TurnOffLightAfterDelay()
    {
        yield return new WaitForSeconds(LighttimerDuration);
        if (isTimerActive)
        {
            LightOnOff = false;
            LightObject.SetActive(false);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(false);
            }
        }
    }
    
}