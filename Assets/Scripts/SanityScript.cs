using UnityEngine;
using UnityEngine.UI;

public class SanityScript : MonoBehaviour
{
    public Image madnessImage;
    public float initialMadness = 1.0f;  // Valor de locura inicial (1 representa locura alta)
    public float madnessReductionRate = 0.2f;  // Tasa de reducción de locura por segundo cuando está en la luz
    public float madnessIncreaseRate = 0.1f; // Tasa de aumento de locura por segundo cuando no está en la luz
    public float madnessActivationThreshold = 0.8f;  // Umbral de locura para activar el enemigo
    public GameObject enemy;  // El enemigo que quieres activar

    private float currentMadness;
    private bool isTouchingLight;
    private bool enemyActivated;

    private void Start()
    {
        currentMadness = initialMadness;
        enemyActivated = false;
        UpdateMadnessVisibility();
    }

    private void Update()
    {
        if (isTouchingLight)
        {
            ReduceMadness();
        }
        else
        {
            IncreaseMadness();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light")) // Ajusta la etiqueta según tu juego
        {
            isTouchingLight = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light")) // Ajusta la etiqueta según tu juego
        {
            isTouchingLight = false;
        }
    }

    void IncreaseMadness()
    {
        currentMadness += madnessIncreaseRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);  // Asegura que la locura esté entre 0 y 1
        UpdateMadnessVisibility();
    }

    void ReduceMadness()
    {
        currentMadness -= madnessReductionRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);  // Asegura que la locura esté entre 0 y 1
        UpdateMadnessVisibility();
    }

    void UpdateMadnessVisibility()
    {
        // Actualiza la visibilidad de la imagen según la locura
        Color imageColor = madnessImage.color;
        imageColor.a = currentMadness; // Hace que la imagen sea más visible a medida que la locura aumenta
        madnessImage.color = imageColor;

        if (currentMadness >= madnessActivationThreshold && !enemyActivated)
        {
            // La locura ha alcanzado el umbral, activa el enemigo
            enemy.SetActive(true);
            enemyActivated = true;
        }
        else if (currentMadness == 0)
        {
            // La locura ha llegado a 0, desactiva el enemigo
            enemy.SetActive(false);
            enemyActivated = false;
        }
    }
}
