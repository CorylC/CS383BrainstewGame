using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Boss1StressTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossLevel1");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Boss1Stress() {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        var boss1Obj = GameObject.FindObjectOfType<Boss1Attack>();
        Assert.NotNull(boss1Obj, "Boss1 is missing");

        bool isDone = false;

        while (!isDone) {
            boss1Obj.fireRate = boss1Obj.fireRate / 2;
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"Fire Rate: {boss1Obj.fireRate}, FPS: {fps}");

            boss1Obj.bossAtk1();

            if(fps < 3){

                isDone = true;

                Assert.Fail("Game too laggy T^T");

                

            }
        }

        yield return null;
    }
}
