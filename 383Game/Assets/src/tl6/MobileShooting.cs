using UnityEngine;

public class MobileShootingManager : MonoBehaviour
{
    private Shooting shootingScript;  // Reference to the existing Shooting script
    private Camera mainCam;
    private Vector3 targetPos;

    void Start()
    {
        // Get the Shooting component attached to the same GameObject
        shootingScript = GetComponent<Shooting>();

        // If it's not on the same GameObject, you can find the shooting script like this:
        // shootingScript = FindObjectOfType<Shooting>();

        mainCam = Camera.main; // Reference to the main camera
    }

    void Update()
    {
        if (PauseManager.isPaused) // Prevent shooting during pause
        {
            return;
        }

        // Handle shooting for mobile touch input (you can add more touch conditions if needed)
        if (Input.touchCount > 0 && shootingScript != null)
        {
            Touch touch = Input.GetTouch(0);  // Get the first touch

            // Get the position where the player tapped
            targetPos = mainCam.ScreenToWorldPoint(touch.position);
            targetPos.z = 0;  // Keep the z-axis at 0 for 2D space

            // Calculate rotation to face the target position
            Vector3 rotation = (targetPos - transform.position);
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            // Rotate the player to face the direction of the tap
            transform.localRotation = Quaternion.Euler(0, 0, rotZ);

            // Call the original Shooting script to fire the bullet
            if (touch.phase == TouchPhase.Began && shootingScript.canFire)
            {
                shootingScript.canFire = false;
                Instantiate(shootingScript.bullet, shootingScript.bulletTransform.position, Quaternion.identity);
                AudioManager.playSound(SoundType.SHOOT);
            }
        }
    }
}

