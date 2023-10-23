using UnityEngine;
using UnityEngine.UI;

public class SanityScript : MonoBehaviour
{
    public Image madnessImage;
    public float initialMadness = 1.0f;
    public float madnessReductionRate = 0.2f;
    public float madnessIncreaseRate = 0.1f;
    public float madnessActivationThreshold = 0.8f;
    public GameObject enemy;
    public AudioSource externalAudioSource; // Referencia al AudioSource de otro objeto

    private float currentMadness;
    private bool isTouchingLight;
    private bool enemyActivated;
    private bool isPlayingSound; // Controla si el sonido está reproduciéndose

    private void Start()
    {
        currentMadness = initialMadness;
        enemyActivated = false;
        isPlayingSound = false; // Inicialmente, el sonido no se está reproduciendo
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
        if (other.CompareTag("Light"))
        {
            isTouchingLight = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            isTouchingLight = false;
        }
    }

    void IncreaseMadness()
    {
        currentMadness += madnessIncreaseRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);
        UpdateMadnessVisibility();

        // Comprueba si la locura ha alcanzado el umbral y el sonido no se está reproduciendo
        if (currentMadness >= madnessActivationThreshold && !enemyActivated && !isPlayingSound)
        {
            Debug.Log("reproduciendo respiración");
            externalAudioSource.loop = true; // Reproduce el sonido en loop desde el AudioSource externo
            externalAudioSource.Play();
            isPlayingSound = true;
        }
    }

    void ReduceMadness()
    {
        currentMadness -= madnessReductionRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);
        UpdateMadnessVisibility();

        // Si la locura disminuye a un nivel en el que debes detener el sonido
        if (currentMadness < madnessActivationThreshold && isPlayingSound)
        {
            Debug.Log("quitando respiración");
            externalAudioSource.Stop(); // Detén el sonido en el AudioSource externo
            isPlayingSound = false;
        }
    }

    void UpdateMadnessVisibility()
    {
        Color imageColor = madnessImage.color;
        imageColor.a = currentMadness;
        madnessImage.color = imageColor;

        if (currentMadness >= madnessActivationThreshold && !enemyActivated)
        {
            enemy.SetActive(true);
            enemyActivated = true;
        }
        else if (currentMadness == 0)
        {
            enemy.SetActive(false);
            enemyActivated = false;
        }
    }
}
