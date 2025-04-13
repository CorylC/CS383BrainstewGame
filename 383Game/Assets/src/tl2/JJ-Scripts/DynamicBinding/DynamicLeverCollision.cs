using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class DynamicLeverCollision : MonoBehaviour
{
    private LeverBase lever;
    //[SerializeField] private UnityEvent _collisionEvent;
    [SerializeField] private string collideTag = "Player";

    void Start()
    {
        lever = new LeverDynamic(2); //init with scene ID 2
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(collideTag))
        {
            lever.ChangeScene(); //dynamically bound method call
            //SceneManager.LoadScene(2);
            gameObject.SetActive(false);
        }
    }
}
