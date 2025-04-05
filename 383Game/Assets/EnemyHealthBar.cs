using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{
    // Reference to the green health bar slider (current health)
    [SerializeField] private Slider healthSlider;

    // Reference to the red background slider (delayed effect)
    [SerializeField] private Slider backgroundSlider;

    // Reference to the EnemyStats script to get health data
    private EnemyStats enemyStats;

    void Start()
    {
        // Get the EnemyStats component attached to this enemy
        enemyStats = GetComponent<EnemyStats>();

        // If the health slider is not assigned in the Inspector, try to find it in the enemy's children
        if (healthSlider == null)
        {
            Transform healthBarTransform = transform.Find("EnemyHealthbars/healthSlider"); // Locating the health slider in the enemy canvas
            if (healthBarTransform != null)
                healthSlider = healthBarTransform.GetComponent<Slider>();
            else
                Debug.LogError("HealthSlider not found! Make sure the path is correct.");
        }

        // If the background slider is not assigned, try to find it
        if (backgroundSlider == null)
        {
            Transform backgroundBarTransform = transform.Find("EnemyHealthbars/backgroundSlider"); // Locating the background slider in the enemy canvas
            if (backgroundBarTransform != null)
                backgroundSlider = backgroundBarTransform.GetComponent<Slider>();
            else
                Debug.LogError("BackgroundSlider not found! Make sure the path is correct.");
        }

        // Check if sliders are successfully assigned
        if (healthSlider == null || backgroundSlider == null)
        {
            Debug.LogError("Sliders not assigned! Ensure they exist and the script can find them.");
        }

        // Initialize the health bar with the correct values
        UpdateHealthBar();
    }

    void Update()
    {
        // Continuously update the health bar values to reflect enemy health changes
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Ensure enemyStats is valid before updating health
        if (enemyStats != null)
        {
            // Calculate health percentage (value between 0 and 1)
            float healthPercentage = enemyStats.health / enemyStats.maxHealth;

            // Instantly update the green health bar to reflect current health
            healthSlider.value = healthPercentage;

            // If the red background bar is still higher, start the smooth effect
            if (backgroundSlider.value > healthSlider.value)
            {
                StartCoroutine(SmoothBackgroundUpdate());
            }
        }
    }

    // Coroutine to smoothly reduce the red background bar over time
    private IEnumerator SmoothBackgroundUpdate()
    {
        // Wait 0.2 seconds before reducing the red bar (creates a delayed damage effect)
        yield return new WaitForSeconds(0.2f);

        // Gradually decrease the red bar until it matches the green bar
        while (backgroundSlider.value > healthSlider.value)
        {
            // Smoothly move the red bar closer to the green bar using Lerp (linear interpolation)
            backgroundSlider.value = Mathf.Lerp(backgroundSlider.value, healthSlider.value, Time.deltaTime * 5);

            // Wait until the next frame before continuing
            yield return null;
        }
    }
}




