using UnityEngine;

public class PlnteractLever : MonoBehaviour
{
    private LeverComponent currentLever; // Reference to the current lever in range

    void Update()
    {
        // Check for interaction input (e.g., "E" key)
        if (Input.GetKeyDown(KeyCode.E) && currentLever != null)
        {
            Debug.Log("Interacting with lever...");
            currentLever.Interact(); // Call Interact() on the current lever
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a LeverComponent
        var lever = collision.GetComponent<LeverComponent>();
        if (lever != null)
        {
            currentLever = lever; // Store reference to the lever
            Debug.Log("Press 'E' to interact.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Clear reference when leaving the interactable's range
        var lever = collision.GetComponent<LeverComponent>();
        if (lever != null && currentLever == lever)
        {
            currentLever = null;
            Debug.Log("You are out of range.");
        }
    }
}
