using UnityEngine;

public class tutorialTestBoss : MonoBehaviour
{
    public float speed = 3.0f;

    Transform player;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
    }
}
