using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    public CutsceneHandler cutSceneHandler;

    public void Start()
    {
        cutSceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute(){

    }
}
