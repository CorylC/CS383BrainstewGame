using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    public PointManager pm;
    public Text scoreText;
    private int score = 0;
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        score = pm.GetPoints();
        scoreText.text = "Score: " + score;
    }
}
