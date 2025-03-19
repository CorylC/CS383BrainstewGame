using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ButtonClickStressTest
{
    private Button testButton;
    private int clickCount = 0;
    private const int maxClicks = 10000;
    private string testSceneName = "MainMenuS1";

    [SetUp]
    public void Setup()
    {
        // Load the specific scene before the test runs
        SceneManager.LoadScene(testSceneName, LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator TestButtonClickStress()
    {
        // Wait until the scene is fully loaded
        yield return new WaitUntil(() => SceneManager.GetSceneByName(testSceneName).isLoaded);

        // Find the button in the scene 
        testButton = GameObject.Find("Play Button").GetComponent<Button>();
        Assert.IsNotNull(testButton, "Play button not found in the scene.");

        // Add listener to count button clicks
        testButton.onClick.AddListener(OnButtonClick);

        // Simulate rapid button clicks
        for (int i = 0; i < maxClicks; i++)
        {
            testButton.onClick.Invoke(); // Simulate the button click
            clickCount++; // Increment the click counter
            yield return null; // Wait one frame

            // If the button breaks (i.e., fails to register clicks), exit the loop
            if (testButton == null)
            {
                Debug.LogError("Button broke after " + clickCount + " clicks.");
                break;
            }
        }

        // Log the number of clicks it took to break the button
        if (testButton != null)
        {
            Debug.Log("Button clicked successfully " + clickCount + " times without breaking.");
        }
        else
        {
            Debug.LogError("Button broke after " + clickCount + " clicks.");
        }
    }

    private void OnButtonClick()
    {
        // Button click logic (can be expanded if necessary)
    }
}
