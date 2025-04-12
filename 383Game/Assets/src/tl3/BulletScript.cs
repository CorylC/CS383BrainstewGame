using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force = 500f;  // Bullet speed
    public float damage = 10f;  // Bullet damage amount

    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        // Get mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Calculate direction from bullet to mouse
        Vector3 direction = (mousePos - transform.position).normalized;

        // Apply velocity to bullet
        rb.linearVelocity = direction * force;  // ? Changed from linearVelocity to velocity

        // Rotate bullet to face movement direction
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Detect collision with ANY object
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);  // ? Debug collision

        // Prevent bullet from hitting the player
        if (collision.CompareTag("Player")) return;

        // If bullet hits an enemy, apply damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();  // Get EnemyStats script
            Boss1Stats boss = collision.GetComponent<Boss1Stats>();  // Get Boss1Stats script
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Apply damage
            }else if(boss != null){ 
            
                boss.TakeDamage(damage);

            }
        }

        // Destroy the bullet when it hits anything except the player
        Destroy(gameObject);
    }
}
