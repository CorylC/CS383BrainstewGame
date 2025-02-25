using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public static bool isPaused; //make global variable so no other inputs during pause
    void Start(){
        PauseMenu.SetActive(false);
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                resumeGame();
            }else{
                pauseGame();
            }
        }
    }

    public void pauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void resumeGame(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void mainMenu(int sceneID){
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void quitGame(){
        Application.Quit();
    }
}
