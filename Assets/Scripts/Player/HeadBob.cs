using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public Transform head;
    public float bobSpeed = 1.0f;
    public float bobAmount = 0.1f;

    private float timer = 0.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            // Aplica el efecto "Head Bob" solo cuando el personaje se está moviendo.
            float waveSlice = Mathf.Sin(timer);
            timer += bobSpeed * Time.deltaTime;

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }

            if (waveSlice != 0)
            {
                float translateChange = waveSlice * bobAmount;
                float totalAxes = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
                totalAxes = Mathf.Clamp(totalAxes, 0, 1);

                translateChange = totalAxes * translateChange;

                // Aplica la posición de la cabeza en la cámara de Cinemachine.
                head.localPosition = new Vector3(head.localPosition.x, translateChange, head.localPosition.z);
            }
            else
            {
                head.localPosition = new Vector3(head.localPosition.x, 0, head.localPosition.z);
            }
        }
        else
        {
            // Cuando el personaje no se está moviendo, la cabeza vuelve a su posición original.
            head.localPosition = new Vector3(head.localPosition.x, 0, head.localPosition.z);
        }
    }
}
