using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    public Text scoreText;

     void Start()
    {
        if (PointManager.instance != null)
            scoreText.text = "Final Score: " + PointManager.instance.GetPoints();
        else
            scoreText.text = "Score not available";
    }
}

