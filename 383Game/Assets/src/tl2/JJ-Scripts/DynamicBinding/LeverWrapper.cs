using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverWrapper
{
    private int sceneID;

    public LeverWrapper(int sceneID)
    {
        this.sceneID = sceneID;
    }

    public void Interact()
    {
        Debug.Log($"Lever activated! Switching to scene ID: {sceneID}");
        SceneManager.LoadScene(sceneID);
    }
}
