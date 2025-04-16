using UnityEngine;
using CodeMonkey.Utils;

public class WeaponTracer
{
    public static void Create(Vector3 fromPosition, Vector3 targetPosition)
    {
        if (GameAssets.i == null)
        {
            Debug.LogError("GameAssets instance is missing in the scene!");
            return;
        }

        if (GameAssets.i.m_WeaponTracer == null)
        {
            Debug.LogError("Weapon tracer material is not assigned in GameAssets!");
            return;
        }

        Vector3 shootDir = (targetPosition - fromPosition).normalized;
        float distance = Vector3.Distance(fromPosition, targetPosition);
        float shootAngle = UtilsClass.GetAngleFromVectorFloat(shootDir);
        Vector3 spawnTracerPosition = fromPosition + shootDir * distance * 0.5f;

        // Setup tracer material
        Material tracerMaterial = new Material(GameAssets.i.m_WeaponTracer);
        tracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 256f));

        // Create the tracer mesh
        World_Mesh worldMesh = new World_Mesh(
            null,
            spawnTracerPosition,
            new Vector3(1, 1),
            shootAngle - 90f,
            6f,                     // meshWidth
            distance,               // meshHeight
            tracerMaterial,
            null,
            10000
        );

        int frame = 0;
        int frameBase = 0;
        float framerate = 0.016f;
        float timer = framerate;

        worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame + 64 * frameBase, 0, 16, 256));

        FunctionUpdater.Create(() => {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer += framerate;
                frame++;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true; // Stop updater
                }

                worldMesh.AddPosition(shootDir * 2f);
                worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame + 64 * frameBase, 0, 16, 256));
            }
            return false;
        });
    }
}
