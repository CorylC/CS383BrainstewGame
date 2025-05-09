using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public Transform target;
    public Walkable walkable;

    //scene boundary values
    public float minX = -67f;
    public float maxX = 67f;
    private Vector2 movement;

    private void Update() {
    if (target != null) {
        var directionTowardsTarget = (target.position - this.transform.position).normalized;
        walkable.moveTo(directionTowardsTarget);
        movement.x = Input.GetAxis("Horizontal");
        if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        else if (movement.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }


        //Debug.Log($"Target Position: {target.position}, Enemy Position: {transform.position}, Direction: {directionTowardsTarget}");
    }

    // After moving, clamp the enemy's X position within bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
}
}
