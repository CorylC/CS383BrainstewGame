using UnityEngine;
using UnityEngine.SceneManagement;

//THIS SCRIPT IS FOR GAME MANAGER & FOR FUNCTIONS TO CHANGE LEVELS/SCENES
//PERHAPS LATER ON ADD ON FUNCTION TRIGGERS FOR CHECKPOINTS
public class ChangeScene : MonoBehaviour
{

    public void moveToScene(int sceneID) //method to move to specific scene based on ID
    {
        if(sceneID == 1)
        {
            AudioManager.playSound(SoundType.STARTGAME);
        }

        SceneManager.LoadScene(sceneID); //load the scene using its ID
        //find scene ID list via Edit > Build Profiles
    }

    public void quitGame() //method to quit application
    {
        Debug.Log("Quitting the game...");
        Application.Quit(); //when game is built and for the quit button

        #if UNITY_EDITOR //to also quit in editor play mode
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void Update()
    {
        //check for 'Q' key press to quit the game as a shortcut (alt to pressing quit button)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            quitGame();
        }
    }
}
