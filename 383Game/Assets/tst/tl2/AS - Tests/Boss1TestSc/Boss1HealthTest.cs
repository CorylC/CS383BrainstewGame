using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Boss1HealthTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossLevel1");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator boss1Test()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        var boss1Obj = GameObject.FindObjectOfType<Boss1Stats>();
        Assert.NotNull(boss1Obj, "Boss1 is missing");

        while (boss1Obj.health >= 10)
        {
            boss1Obj.TakeDamage(10);
            yield return null;
        }

        boss1Obj.TakeDamage(10);
        Assert.Null(boss1Obj, $"Boss Health: {boss1Obj.health}");
        yield return null;

        boss1Obj.TakeDamage(10);
        Assert.Null(boss1Obj, $"Boss Health: {boss1Obj.health}");
        yield return null;

        yield return null;
    }
}
