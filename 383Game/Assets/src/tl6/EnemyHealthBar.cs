using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{
    // Drag the green health bar (Slider) into this field via Inspector
    [SerializeField] private Slider healthSlider;

    // Drag the red delayed background bar (Slider) into this field via Inspector
    [SerializeField] private Slider backgroundSlider;

    // Reference to the enemy's stats script (make sure it's on the same GameObject)
    private EnemyStats enemyStats;

    void Start()
    {
        // Get the EnemyStats component on the same GameObject
        enemyStats = GetComponent<EnemyStats>();

        // Log errors if the sliders were not assigned
        if (healthSlider == null)
        {
            Debug.LogError("Health Slider is not assigned in the Inspector.");
        }

        if (backgroundSlider == null)
        {
            Debug.LogError("Background Slider is not assigned in the Inspector.");
        }

        // Update the health bar right away on start
        UpdateHealthBar();
    }

    void Update()
    {
        // Continuously update the health values every frame
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Make sure all references are valid before using them
        if (enemyStats != null && healthSlider != null && backgroundSlider != null)
        {
            // Calculate current health percentage (0 to 1)
            float healthPercentage = enemyStats.health / enemyStats.maxHealth;

            // Instantly update the green bar
            healthSlider.value = healthPercentage;

            // Smoothly reduce the red bar if it's still higher
            if (backgroundSlider.value > healthSlider.value)
            {
                StartCoroutine(SmoothBackgroundUpdate());
            }
        }
    }

    // Coroutine to gradually reduce the red background slider
    private IEnumerator SmoothBackgroundUpdate()
    {
        // Slight delay before red bar starts falling
        yield return new WaitForSeconds(0.2f);

        // Slowly bring the red bar down until it matches the green one
        while (backgroundSlider.value > healthSlider.value)
        {
            backgroundSlider.value = Mathf.Lerp(backgroundSlider.value, healthSlider.value, Time.deltaTime * 5);
            yield return null;
        }
    }
}




