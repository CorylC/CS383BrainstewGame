using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GunMode upgradeTo; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Gun gun = other.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.currentMode = upgradeTo;
                Debug.Log("Projectile collected: " + upgradeTo.ToString());
            }

            Destroy(gameObject); // Remove the Projectile from scene
        }
    }
}
