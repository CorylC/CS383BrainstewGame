using UnityEngine;
using UnityEngine.SceneManagement;  
public class SettingsMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void mainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
}
}