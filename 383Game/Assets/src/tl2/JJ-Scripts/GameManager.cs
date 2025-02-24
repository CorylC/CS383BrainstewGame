using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keeps it across scenes
        }
        else
        {
            Destroy(gameObject); //prevents duplicates
        }
    }
}


//later on we will have player selected settings so it maintains that for all scenes
//as well as a score & state