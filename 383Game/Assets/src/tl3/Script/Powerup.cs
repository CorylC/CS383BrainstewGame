using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;
    private Vector3 startPos;

    private float glowSpeed = 2f;
    private float glowAmount = 0.2f;
    private Vector3 originalScale;

    public float spinSpeed = 45f; // degrees per second

    void Awake()
    {
        startPos = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Floating up and down
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Pulsing (scaling) effect
        float pulse = 1f + Mathf.Sin(Time.time * glowSpeed) * glowAmount;
        transform.localScale = originalScale * pulse;

        // Spinning (optional)
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Gun gun = collision.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.UpgradeGun();
            }

            Destroy(gameObject); // Remove the powerup
        }
    }
}
