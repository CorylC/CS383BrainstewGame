using UnityEngine;
using CodeMonkey.Utils;
using System.Collections.Generic;

public class Shoot_Flash
{

    private static List<Shoot_Flash> shootList = new List<Shoot_Flash>();
    private static float deltaTime;
    private static FunctionUpdater functionUpdater;

    private float timer = .05f;
    private int index;
    private static Vector3 baseSize = new Vector3(20, 20);

    
    public static void AddFlash(Vector3 pos)
    {
        Init();
    }

    private static void Init()
    {
        if (functionUpdater == null)
        {
            functionUpdater = FunctionUpdater.Create(() => {
                Update_Static();
                return false;
            });
        }
    }

    private void Update()
    {
        timer -= deltaTime;
        if (timer < 0)
        {
            
            shootList.Remove(this);
        }
    }

    private static void Update_Static()
    {
        deltaTime = Time.deltaTime;
        for (int i = shootList.Count - 1; i >= 0; i--)
        {
            shootList[i].Update();
        }
    }
}
