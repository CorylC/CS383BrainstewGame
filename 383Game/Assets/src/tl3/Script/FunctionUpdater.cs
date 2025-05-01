using System;
using UnityEngine;

public class FunctionUpdater : MonoBehaviour
{

    private static GameObject globalUpdaterObject;
    private static MonoBehaviourHook globalHook;

    public static FunctionUpdater Create(Func<bool> func)
    {
        if (globalUpdaterObject == null)
        {
            globalUpdaterObject = new GameObject("FunctionUpdater_Global");
            globalHook = globalUpdaterObject.AddComponent<MonoBehaviourHook>();
        }

        return new FunctionUpdater(func, globalHook);
    }

    private class MonoBehaviourHook : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }

    private Func<bool> updateFunc;
    private MonoBehaviourHook hookRef;

    private FunctionUpdater(Func<bool> func, MonoBehaviourHook hook)
    {
        this.updateFunc = func;
        this.hookRef = hook;
        hookRef.OnUpdate += Update;
    }

    private void Update()
    {
        if (updateFunc())
        {
            hookRef.OnUpdate -= Update;
        }
    }

    public void DestroySelf()
    {
        hookRef.OnUpdate -= Update;
    }
}
