using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TooLoudBoundary
{

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Freezer-s2");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TooLoudBoundaryWithEnumeratorPasses()
    {
        int volume = 10000;
        var audioManagerObject = GameObject.FindObjectOfType<AudioManager>();
        Assert.NotNull(audioManagerObject, "AudioManager Not in scene");


        AudioManager.playSound(SoundType.HURT, volume);
        Debug.Log($"Played audio clip at {volume} volume");
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
