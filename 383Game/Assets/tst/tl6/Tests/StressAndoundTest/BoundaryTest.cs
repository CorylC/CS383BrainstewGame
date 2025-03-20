using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class SceneBoundaryTest
{
    [UnityTest]
    public IEnumerator TestSceneLoad_InvalidScene_Handled()
    {
        string invalidSceneName = "NonExistentScene";

        Debug.Log($"Attempting to load invalid scene: {invalidSceneName}");

        // Expect Unity to log an error when attempting to load a nonexistent scene
        LogAssert.Expect(LogType.Error, $"Scene '{invalidSceneName}' couldn't be loaded");

        bool loadFailed = false;

        try
        {
            SceneManager.LoadScene(invalidSceneName);
        }
        catch
        {
            Debug.Log($"Scene '{invalidSceneName}' failed to load as expected.");
            loadFailed = true;
        }

        // Ensure the test confirms the scene did NOT load
        Assert.IsTrue(loadFailed, "Scene should have failed to load.");

        yield return null;
    }
}
