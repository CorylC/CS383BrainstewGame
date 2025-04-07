using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform posA, posB;
    private MovingPlatformDynamicBindingBC speed;
    Vector2 targetPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = new MovingPlatformDynamicBinding();
        speed.setSpeed(5f);
        targetPos = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, posB.position) < 0.1f) targetPos = posA.position;

        if(Vector2.Distance(transform.position, posA.position) < 0.1f) targetPos = posB.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed.getSpeed() * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(posB.position, posA.position);
    }
}
