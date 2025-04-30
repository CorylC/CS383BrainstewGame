using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    protected private static int total_points = 0;

    public void AddPoints(int points)
    {
        total_points += points;
        Debug.Log("Total Points: " + total_points);
    }
    public int GetPoints(){
        return total_points;
    }
}