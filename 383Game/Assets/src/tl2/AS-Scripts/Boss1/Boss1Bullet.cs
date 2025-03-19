using UnityEngine;

public class Boss1Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 4f;

    public GameObject playerPos;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        //    Destroy(gameObject, lifeTime);

        Vector3 direction = playerPos.transform.position - transform.position;
        rb.linearVelocity = new Vector2 (direction.x, direction.y).normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.position += -transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
