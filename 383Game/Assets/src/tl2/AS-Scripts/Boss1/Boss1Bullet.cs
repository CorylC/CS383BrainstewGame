using UnityEngine;

public class Boss1Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 4f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
