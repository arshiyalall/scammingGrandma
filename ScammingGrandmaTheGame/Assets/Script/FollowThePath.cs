using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    private int waypointIndex = 0;
    private int direction = 1; // 1 for forward, -1 for backward

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypoints.Length == 0)
            return;

        transform.position = Vector2.MoveTowards(transform.position,
           waypoints[waypointIndex].transform.position,
           moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += direction;

            // Reverse direction at the end points
            if (waypointIndex == waypoints.Length || waypointIndex < 0)
            {
                direction *= -1;
                waypointIndex += direction * 2; // Step back inside the bounds
            }
        }
    }
}
