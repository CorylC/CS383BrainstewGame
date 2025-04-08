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

        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (enemyStats != null && healthSlider != null && backgroundSlider != null)
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




