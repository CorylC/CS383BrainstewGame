using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AudioStress5
{

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Freezer-s2");
    }

    // A UnityTest behaves lik e a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AudioStressTestWithEnumeratorPasses()
    {
        var audioManagerObject = GameObject.FindObjectOfType<AudioManager>();
        Assert.NotNull(audioManagerObject, "AudioManager Not in scene");

        var isdone = false;
        int i = 0;
        float startTime = Time.time;
        float minFrameRate = 10f;


        while (i < 1000000 && 1f / Time.deltaTime > minFrameRate)
        {
            i++;
            AudioManager.playSound(SoundType.STARTGAME);
            Debug.Log($"Played {i} audioclips at {1f / Time.deltaTime > minFrameRate} frames");
        }

        // Let Unity process the audio
        yield return null;
    }
}
