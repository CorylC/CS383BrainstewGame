using UnityEngine;
using UnityEngine.SceneManagement;

//THIS SCRIPT IS FOR GAME MANAGER & FOR FUNCTIONS TO CHANGE LEVELS/SCENES
//PERHAPS LATER ON ADD ON FUNCTION TRIGGERS FOR CHECKPOINTS
public class ChangeScene : MonoBehaviour
{
    public void moveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID); 
        //append this function to buttons for example
        //find scene ID list via Edit > Build Profiles
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
