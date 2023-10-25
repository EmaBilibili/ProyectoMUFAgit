using UnityEngine;

public class PickupKey : MonoBehaviour
{
    private bool isInRange = false;
    public DoorLocked doorLocked;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            doorLocked.hasKey = true;
            gameObject.SetActive(false); // O puedes destruirla con Destroy(gameObject)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Puedes mostrar un mensaje al jugador para indicar que presione "E" para recoger la llave
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // Oculta el mensaje al jugador
        }
    }
}