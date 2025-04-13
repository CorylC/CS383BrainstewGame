using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverBase
{
    public virtual void ChangeScene()
    {
        Debug.Log("Base class method called.");
    }
}

public class LeverDynamic : LeverBase
{
    private int sceneID;

    public LeverDynamic(int sceneID)
    {
        this.sceneID = sceneID;
    }

    public override void ChangeScene()
    {
        Debug.Log("Dynamic class method called. Changing to scene ID: " + sceneID);
        MockChangeScene(sceneID);
    }

    private void MockChangeScene(int sceneID)
    {
        Debug.Log($"Mock changing scene to ID: {sceneID}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
    }
}
