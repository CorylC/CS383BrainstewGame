using UnityEngine;

public class Walkable : MonoBehaviour
{

    private const float ForcePower = 10f;

    public new Rigidbody2D rigidbody;

    public float speed = 2f;
    public float force = 2f;

    private Vector2 direction;

    public void moveTo (Vector2 direction) {
        this.direction = direction;
    }

    public void stop() {
        moveTo(Vector2.zero);
    }

    private void FixedUpdate() {
        var desiredVelocity = direction * speed;
        var deltaVelocity = desiredVelocity - rigidbody.linearVelocity;
        Vector3 moveForce = deltaVelocity * (force * ForcePower * Time.fixedDeltaTime);

        rigidbody.AddForce(moveForce);

        Debug.Log($"Direction: {direction}, Velocity: {rigidbody.linearVelocity}");
    }



}


