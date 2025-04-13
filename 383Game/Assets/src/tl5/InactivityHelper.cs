using UnityEngine;
using UnityEngine.Video;

public class InactivitySetup : MonoBehaviour
{
    public GameObject videoCanvas;
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (InactivityManager.instance != null)
        {
            InactivityManager.instance.videoCanvas = videoCanvas;
            InactivityManager.instance.videoPlayer = videoPlayer;

            // Stop any lingering demo state
            InactivityManager.instance.ResetInactivity();
        }
    }
}
