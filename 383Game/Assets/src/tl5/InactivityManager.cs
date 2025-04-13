using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class InactivityManager : MonoBehaviour
{
    public static InactivityManager instance;
    public VideoPlayer videoPlayer; // Assign in the Inspector
    public GameObject videoCanvas; // Assign a UI canvas that holds the VideoPlayer
    private float inactivityTimer = 0f;
    public float inactivityThreshold = 10f; // 0 seconds

    void Awake()
    {
        // If there's no instance yet, set this one
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make it persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicates
        }
    }

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

        // If inactive for 10 seconds, start demo
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

    public void ResetInactivity()
    {
        inactivityTimer = 0f;
        if (videoCanvas != null && videoCanvas.activeSelf)
        {
            StopDemo();
        }
    }
}
