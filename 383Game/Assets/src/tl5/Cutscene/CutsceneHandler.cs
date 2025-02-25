using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    private CutsceneElementBase[] _cutsceneElements;
    private int _index = -1;

    public void Start()
    {
        _cutsceneElements = GetComponents<CutsceneElementBase>();
    }
}
