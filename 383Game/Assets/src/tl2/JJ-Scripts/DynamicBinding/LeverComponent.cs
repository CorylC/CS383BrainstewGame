using UnityEngine;

public class LeverComponent : MonoBehaviour
{
    private LeverWrapper lever;

    void Awake()
    {
        // Initialize non-MonoBehaviour logic with a specific scene ID
        lever = new LeverWrapper(2); // Replace 2 with your desired Scene ID
    }

    public void Interact()
    {
        // Delegate interaction to LeverWrapper
        lever.Interact();
    }
}
