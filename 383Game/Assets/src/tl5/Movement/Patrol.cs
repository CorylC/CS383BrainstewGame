using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] targetPoints;
    [SerializeField] int currentPoint;
    [SerializeField] float moveSpeed;

    [Space]
    [Header("Timers")]
    [SerializeField] float maxWaitTime;
    [SerializeField] float minWaitTime;

    private float waitTime;
    private float waitTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = Random.Range(0, targetPoints.Length);

        waitTimeCounter = SetWaitTime();
    }

    // Update is called once per frame
    void Update()
{
    Vector2 targetPosition = new Vector2(targetPoints[currentPoint].position.x, transform.position.y);

    if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
    {
        if (waitTimeCounter <= 0)
        {
            IncreaseCurrentPoint();
            waitTimeCounter = SetWaitTime();
        }
        else
        {
            waitTimeCounter -= Time.deltaTime;
        }
    }

    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
}

    private void IncreaseCurrentPoint()
    {
        currentPoint++;
        if(currentPoint >= targetPoints.Length)
        {
            currentPoint = 0;
        }
    }

    private float SetWaitTime()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        return waitTime;
    }
}