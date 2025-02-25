using System.Collections;
using UnityEngine;

public class CSE_Test : CutsceneElementBase
{
    public override void Execute(){
        Debug.Log("Executing " + name);
    }
 
    protected IEnumerator WaitAndAdvance(){
        yield return new WaitForSeconds(duration);
        cutSceneHandler.PlayNextElement();
    }
}
