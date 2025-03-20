using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class MenuResolutionBoundaryTest
{
    private GameObject menuObject;
    private Canvas canvas;
    private RectTransform menuPanel;

    [SetUp]
    public void Setup()
    {
        // Create Menu Canvas
        menuObject = new GameObject("Menu");
        canvas = menuObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Ensure Canvas Scales with Resolution
        CanvasScaler scaler = canvas.gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080); // Base resolution
        scaler.matchWidthOrHeight = 0.5f; // Balance width & height scaling

        // Create Menu Panel
        menuPanel = new GameObject("MenuPanel").AddComponent<RectTransform>();
        menuPanel.SetParent(canvas.transform);
        menuPanel.anchorMin = new Vector2(0.5f, 0.5f);
        menuPanel.anchorMax = new Vector2(0.5f, 0.5f);
        menuPanel.pivot = new Vector2(0.5f, 0.5f);
        menuPanel.anchoredPosition = Vector2.zero;  // Center the menu

        // Dynamic size to fit screen properly
        menuPanel.sizeDelta = new Vector2(canvas.pixelRect.width * 0.8f, canvas.pixelRect.height * 0.8f);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(menuObject);
    }

    [UnityTest]
    public IEnumerator MenuRemainsVisibleAtHighResolution()
    {
        Screen.SetResolution(3840, 2160, false);
        yield return new WaitForSeconds(0.5f); // Allow Unity to apply resolution

        Vector3[] corners = new Vector3[4];
        menuPanel.GetWorldCorners(corners);

        // Log all corners of the menu for debugging
        Debug.Log($" Menu Corners at 4K Resolution:");
        Debug.Log($" Top Left: {corners[1]}");
        Debug.Log($" Bottom Right: {corners[3]}");

        // Print individual corner positions
        for (int i = 0; i < 4; i++)
        {
            Debug.Log($"Corner {i}: {corners[i]}");
        }

        bool testFailed = false;

        foreach (Vector3 corner in corners)
        {
            bool withinBoundsX = corner.x >= 0 && corner.x <= Screen.width;
            bool withinBoundsY = corner.y >= 0 && corner.y <= Screen.height;

            if (!withinBoundsX || !withinBoundsY)
            {
                Debug.LogError($" Menu is out of bounds! Corner: {corner}");
                testFailed = true;
            }

            Assert.IsTrue(withinBoundsX, "Menu should be within horizontal screen bounds.");
            Assert.IsTrue(withinBoundsY, "Menu should be within vertical screen bounds.");
        }

        if (testFailed)
        {
            Debug.LogError(" TEST FAILED: Menu is out of screen bounds at 4K resolution.");
        }
        else
        {
            Debug.Log(" TEST PASSED: Menu is within screen bounds.");
        }
    }
}

