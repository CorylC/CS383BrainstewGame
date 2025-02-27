using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public static bool isPaused; //make global variable so no other inputs during pause
    void Start()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame(){
        Debug.Log("pauseGame() triggered by: " + (new System.Diagnostics.StackTrace()).ToString());
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void resumeGame(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void mainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void quitGame(){
        Debug.Log("Quitting the game...");
        Application.Quit(); //when game is built and for the quit button

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
