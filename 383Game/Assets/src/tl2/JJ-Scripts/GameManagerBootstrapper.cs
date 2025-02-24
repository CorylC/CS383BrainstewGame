using UnityEngine;

public class GameManagerBootstrapper : MonoBehaviour
{
     private void Awake()
    {
        if (GameManager.Instance == null) //if GameManager doesn't exist, create it
        {
            GameManager prefab = Resources.Load<GameManager>("GameManager"); //load from Resources
            if (prefab != null)
            {
                Instantiate(prefab);
                Debug.Log("GameManager instantiated from Bootstrapper.");
            }
            else
            {
                Debug.LogError("GameManager prefab NOT found in Resources!");
            }
        }
    }
}
