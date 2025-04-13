using UnityEngine;

/*
This script is attached to the MovingPlatform game object, as well as the moving traps in the game.
The platform moves between two points (posA and posB) at a specified speed.
Then the player can take the platform to move between the two points.
*/

public class MovingPlatform : MonoBehaviour
{

    public Transform posA, posB; // The two positions the platform will move between
    private MovingPlatformDynamicBindingBC speed; // The speed of the platform
    Vector2 targetPos; // The target position the platform is moving towards


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = new MovingPlatformDynamicBinding(); // Initialize the speed object
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
