using UnityEngine;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    protected private static int total_points = 0;

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else{
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //reset score on main menu back to 0 or freezer
        if(scene.buildIndex == 0 || scene.buildIndex == 1){
            ResetPoints();
        }
    }
    public void AddPoints(int points)
    {
        total_points += points;
        Debug.Log("Total Points: " + total_points);
    }
    public int GetPoints(){
        return total_points;
    }

    public void ResetPoints(){
        total_points = 0;
    }

    void OnDestroy(){
        if(instance == this){
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}