using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public Transform target;
    public Walkable walkable;

    private void Update() {
    if (target != null) {
        var directionTowardsTarget = (target.position - this.transform.position).normalized;
        walkable.MoveTo(directionTowardsTarget);

        Debug.Log($"Target Position: {target.position}, Enemy Position: {transform.position}, Direction: {directionTowardsTarget}");
    }
}
}
