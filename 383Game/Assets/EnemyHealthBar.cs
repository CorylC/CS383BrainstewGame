using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider healthSlider;
    private Slider backgroundSlider;
    private EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

<<<<<<< Updated upstream
        // Find the healthSlider and backgroundSlider within the child prefab
        Transform healthSliderTransform = transform.Find("EnemyHealthBars/EnemyHealthbar/healthSlider");
        if (healthSliderTransform != null)
        {
            healthSlider = healthSliderTransform.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("healthSlider not found. Check prefab hierarchy.");
        }

        Transform backgroundSliderTransform = transform.Find("EnemyHealthBars/EnemyHealthbar/backgroundSlider");
        if (backgroundSliderTransform != null)
        {
            backgroundSlider = backgroundSliderTransform.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("backgroundSlider not found. Check prefab hierarchy.");
        }

=======
        // These paths are now relative to this GameObject
        if (healthSlider == null)
        {
            Transform healthBarTransform = transform.Find("EnemyHealthbar/healthSlider");
            if (healthBarTransform != null)
            {
                healthSlider = healthBarTransform.GetComponent<Slider>();
                Debug.Log("HealthSlider assigned successfully.");
            }
            else
            {
                Debug.LogError("HealthSlider not found! Check prefab structure.");
            }
        }

        if (backgroundSlider == null)
        {
            Transform backgroundBarTransform = transform.Find("EnemyHealthbar/backgroundSlider");
            if (backgroundBarTransform != null)
            {
                backgroundSlider = backgroundBarTransform.GetComponent<Slider>();
                Debug.Log("BackgroundSlider assigned successfully.");
            }
            else
            {
                Debug.LogError("BackgroundSlider not found! Check prefab structure.");
            }
        }

        if (healthSlider == null || backgroundSlider == null)
        {
            Debug.LogError("Sliders not assigned properly.");
        }

>>>>>>> Stashed changes
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
<<<<<<< Updated upstream
        if (enemyStats != null && healthSlider != null && backgroundSlider != null)
=======
        if (enemyStats != null)
>>>>>>> Stashed changes
        {
            float healthPercentage = enemyStats.health / enemyStats.maxHealth;
            healthSlider.value = healthPercentage;

            if (backgroundSlider.value > healthSlider.value)
            {
                StartCoroutine(SmoothBackgroundUpdate());
            }
        }
    }

    private IEnumerator SmoothBackgroundUpdate()
    {
        yield return new WaitForSeconds(0.2f);

        while (backgroundSlider.value > healthSlider.value)
        {
            backgroundSlider.value = Mathf.Lerp(backgroundSlider.value, healthSlider.value, Time.deltaTime * 5);
            yield return null;
        }
    }
}

<<<<<<< Updated upstream



=======
>>>>>>> Stashed changes
