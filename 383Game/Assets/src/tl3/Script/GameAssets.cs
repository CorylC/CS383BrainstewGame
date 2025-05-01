using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }

    public Material m_WeaponTracer; // Drag your yellow streak material here
}
