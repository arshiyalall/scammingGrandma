// using UnityEngine;

// public class GuardPatrol : MonoBehaviour
// {
//     public Transform[] waypoints;  // Array of waypoints for the guard to move between
//     public float speed = 2f;       // Speed of movement
//     private int currentWaypointIndex = 0; // Index to track current waypoint

//     void Update()
//     {
//         if (waypoints.Length == 0) return; // If no waypoints, do nothing

//         // Move guard toward the current waypoint
//         transform.position = Vector2.MoveTowards(transform.position, 
//                                                  waypoints[currentWaypointIndex].position, 
//                                                  speed * Time.deltaTime);

//         // Check if guard has reached the waypoint
//         if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
//         {
//             // Move to the next waypoint
//             currentWaypointIndex++;

//             // If reached the last waypoint, loop back to the first one
//             if (currentWaypointIndex >= waypoints.Length)
//             {
//                 currentWaypointIndex = 0;
//             }
//         }
//     }
// }


using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    // Random Direction Flip Variables
    public float flipChance = 0.3f;  // 30% chance to reverse direction
    public float flipInterval = 5f;  // Time between flip checks

    private int currentWaypointIndex = 0;
    private float nextFlipTime = 0f;
    private bool isFlipped = false;
    private SpriteRenderer spriteRenderer;
    private int direction = 1; // 1 means moving forward, -1 means moving backward

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("ERROR: No SpriteRenderer found on the Guard!");
        }

        nextFlipTime = Time.time + flipInterval;
    }

    void Update()
    {
        Patrol();

        // Randomly flip movement direction at intervals
        if (Time.time > nextFlipTime)
        {
            RandomlyFlipDirection();
            nextFlipTime = Time.time + flipInterval;
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        // Move towards the next waypoint in the current direction
        transform.position = Vector2.MoveTowards(transform.position,
                                                 waypoints[currentWaypointIndex].position,
                                                 speed * Time.deltaTime);

        // When reaching a waypoint, move to the next one
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex += direction;

            // **Reverse direction if at the end or start of waypoints**
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2; // Move back
                FlipDirection();
                direction = -1; // Move backward
            }
            else if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Move forward
                FlipDirection();
                direction = 1; // Move forward
            }
        }
    }

    void FlipDirection()
    {
        Debug.Log("↔️ Flipping at Waypoint!");
        isFlipped = !isFlipped;

        // **Method 1: Flip Using Scale**
        transform.localScale = new Vector3(isFlipped ? -1 : 1, 1, 1);

        // **Method 2: Flip Using SpriteRenderer**
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void RandomlyFlipDirection()
    {
        if (Random.value < flipChance) // 30% chance to flip direction
        {
            Debug.Log(" Randomly Changing Direction!");

            // Reverse direction & Flip
            direction *= -1;
            FlipDirection();

            // Ensure it moves to a valid waypoint after flipping
            currentWaypointIndex += direction;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = waypoints.Length - 1;
            if (currentWaypointIndex < 0) currentWaypointIndex = 0;
        }
    }
}
