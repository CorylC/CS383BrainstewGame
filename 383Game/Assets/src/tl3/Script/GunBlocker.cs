using UnityEngine;

public class GunBlocker : MonoBehaviour
{
    public Transform gunVisual; // the part of the gun that stretches forward (maybe the gun sprite?)
    public Transform firePoint; // the shooting point (at the tip of the gun)
    public float maxGunLength = 1.5f; // max stretch distance
    public LayerMask obstacleMask; // what layers to stop against

    void Update()
    {
        Vector2 direction = transform.right; // facing direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxGunLength, obstacleMask);

        if (hit.collider != null)
        {
            // If we hit something, stretch the gun only up to the obstacle
            float distanceToObstacle = hit.distance;
            gunVisual.localScale = new Vector3(distanceToObstacle, 1, 1);
        }
        else
        {
            // No obstacle? Full stretch
            gunVisual.localScale = new Vector3(maxGunLength, 1, 1);
        }
    }
}

