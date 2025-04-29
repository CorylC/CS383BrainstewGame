using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    public int total_points = 0;

    public void AddPoints(int points)
    {
        total_points += points;
        Debug.Log("Total Points: " + total_points);
    }
}
