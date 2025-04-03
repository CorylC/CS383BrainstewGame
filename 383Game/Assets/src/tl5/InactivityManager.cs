using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class InactivityManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign in the Inspector
    public GameObject videoCanvas; // Assign a UI canvas that holds the VideoPlayer
    private float inactivityTimer = 0f;
    public float inactivityThreshold = 30f; // 30 seconds

    void Start()
    {
        videoCanvas.SetActive(false); // Hide video UI initially
    }

    void Update()
    {
        // Detect input (keyboard/mouse movement/click)
        if (Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            inactivityTimer = 0f; // Reset timer if there's activity

            if (videoCanvas.activeSelf)
            {
                StopDemo();
            }
        }
        else
        {
            inactivityTimer += Time.deltaTime;
        }

        // If inactive for 30 seconds, start demo
        if (inactivityTimer >= inactivityThreshold)
        {
            StartDemo();
        }
    }

    void StartDemo()
    {
        Time.timeScale = 0; // Pause game
        videoCanvas.SetActive(true);
        videoPlayer.Play();
    }

    void StopDemo()
    {
        Time.timeScale = 1; // Resume game
        videoCanvas.SetActive(false);
        videoPlayer.Stop();
        inactivityTimer = 0f; // Reset timer
    }
}
