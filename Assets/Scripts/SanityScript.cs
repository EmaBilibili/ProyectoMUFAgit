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

    public float lightRadius = 5.0f;
    public LayerMask lightLayer;

    private float currentMadness;
    private bool enemyActivated;
    
    public AudioSource audioSourceSanity;
    

    private void Start()
    {
        currentMadness = initialMadness;
        enemyActivated = false;
        UpdateMadnessVisibility();
    }

    private void Update()
    {
        if (IsInLight())
        {
            ReduceMadness();
        }
        else
        {
            IncreaseMadness();
        }
    }

    bool IsInLight()
    {
        // Realiza un raycast de esfera para detectar si el personaje está dentro del radio de una luz
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lightRadius, lightLayer);

        // Visualiza el raycast
        Color rayColor = hitColliders.Length > 0 ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.up * lightRadius, rayColor);

        return hitColliders.Length > 0;
    }

    void IncreaseMadness()
    {
        currentMadness += madnessIncreaseRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);
        UpdateMadnessVisibility();
    }

    void ReduceMadness()
    {
        currentMadness -= madnessReductionRate * Time.deltaTime;
        currentMadness = Mathf.Clamp01(currentMadness);
        UpdateMadnessVisibility();
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
            audioSourceSanity.Play();
        }
        else if (currentMadness == 0)
        {
            enemy.SetActive(false);
            enemyActivated = false;
            audioSourceSanity.Stop();
        }
    }
}
