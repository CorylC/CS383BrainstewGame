using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class StaticLeverCollision : MonoBehaviour
{
    //[SerializeField] private UnityEvent _collisionEvent;
    [SerializeField] private string collideTag = "Player";

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(collideTag))
        {
            //maybe some lever pushed animation
            ChangeSceneStatic(2); //static bound method call
            gameObject.SetActive(false);
        }
    }

    void ChangeSceneStatic(int sceneID) //this would be called
    {
        Debug.Log("Static method called. Changing to scene ID: " + sceneID);
        SceneManager.LoadScene(sceneID);
    }
}
