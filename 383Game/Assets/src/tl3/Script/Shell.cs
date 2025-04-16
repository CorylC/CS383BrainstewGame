using UnityEngine;

public class Shell : MonoBehaviour
{
    public float ejectForce = 5f;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 ejectDir = new Vector2(Random.Range(-1f, -0.3f), 1f).normalized;
        rb.AddForce(ejectDir * ejectForce, ForceMode2D.Impulse);
        Destroy(gameObject, 2f); // auto cleanup
    }
}
