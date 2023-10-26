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

    public AudioSource switchOnSound; // Referencia al componente de audio
    public AudioSource switchOffSound;


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
            PlaySwitchOnSound();
            StartTimer();
        }
        else
        {
            LightObject.SetActive(false);
            if (ObjectToDisable != null)
            {
                ObjectToDisable.SetActive(false);
            }
            PlaySwitchOffSound();
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

    private void PlaySwitchOnSound()
    {
        if (switchOnSound != null && switchOnSound.clip != null)
        {
            switchOnSound.Play();
        }
    }

    private void PlaySwitchOffSound()
    {
        if (switchOffSound != null && switchOffSound.clip != null)
        {
            switchOffSound.Play();
        }
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

