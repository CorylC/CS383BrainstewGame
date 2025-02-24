using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); //prevent duplicates
            return;
        }

        //ensure GameManager is instantiated if missing
        if (Instance == null) 
        {
            GameManager prefab = Resources.Load<GameManager>("GameManager");
            if (prefab != null)
            {
                GameManager newInstance = Instantiate(prefab);
                DontDestroyOnLoad(newInstance.gameObject);
                Instance = newInstance;
                Debug.Log("Instantiated GameManager from Resources.");
            }
            else
            {
                Debug.LogError("GameManager prefab not found in Resources!");
            }
        }
    }
}


//later on we will have player selected settings so it maintains that for all scenes
//as well as a score & state