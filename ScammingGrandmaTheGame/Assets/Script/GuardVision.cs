using UnityEngine;

public class GuardVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float detectionRange = 10f; // How far the guard can see
    public float fieldOfViewAngle = 90f; // Field of view angle in degrees

    [Header("References")]
    public Transform player; // Player reference
    public LayerMask obstacleMask; // Layers to block the raycast (like walls)

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("❌ Player not found! Make sure the Player has the 'Player' tag.");
            }
        }
    }

    private void Update()
    {
        if (player == null) return; // Prevent errors if the player isn't assigned

        if (CanSeePlayer())
        {
            Debug.Log("🔴 Player spotted!");
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        // Step 1: Check if the player is within the detection range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange) return false;

        // Step 2: Check if the player is within the field of view angle
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer); // Use transform.right for 2D
        if (angleToPlayer > fieldOfViewAngle / 2f) return false;

        // Step 3: Perform a raycast to check for obstacles blocking the vision
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true; // Guard can see the player
        }

        return false;
    }

    // Debugging with Gizmos
    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Draw field of view as a cone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector2 leftBoundary = Quaternion.Euler(0, 0, -fieldOfViewAngle / 2) * transform.right;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, fieldOfViewAngle / 2) * transform.right;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + leftBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rightBoundary * detectionRange);

        // Draw the ray for debugging
        if (CanSeePlayer())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
