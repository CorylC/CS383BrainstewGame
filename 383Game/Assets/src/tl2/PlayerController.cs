using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallSpeedMultiplier = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    [SerializeField] private KeyCode jump = KeyCode.W;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (rb == null) return;

        //raycast for grounded check
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        //jump logic
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }

        //quick fall mechanic - STILL A WIP LOL
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fallSpeedMultiplier); 
        }
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}