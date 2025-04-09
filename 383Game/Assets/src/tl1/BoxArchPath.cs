using UnityEngine;

public class BoxArchPath : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force = 10f;
    public float upwardForce = 5f;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Direction to player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Add force with both forward and upward components
            Vector2 launchForce = new Vector2(direction.x * force, upwardForce);
            rb.AddForce(launchForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}