using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuardVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 90f;
    public float rotationSpeed = 2f; // Speed of rotation
    public float switchDirectionInterval = 3f; // Time between direction switches

    [Header("References")]
    public Transform player;
    public LayerMask obstacleMask;
    public LineRenderer visionCone; // Reference to the LineRenderer

    private float currentRotationAngle = 0f;
    private bool rotatingClockwise = true;

    private bool isFlipped = false; // Track if the guard is flipped

    private float timePlayerInSight = 0f; // Track the time the player has been in sight

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

        if (visionCone == null)
        {
            visionCone = GetComponent<LineRenderer>();
        }

        if (visionCone != null)
        {
            visionCone.positionCount = 3; // Triangle shape
            visionCone.startWidth = 0.05f;
            visionCone.endWidth = 0.05f;
            visionCone.useWorldSpace = true;
            visionCone.material = new Material(Shader.Find("Sprites/Default"));
            visionCone.startColor = Color.yellow;
            visionCone.endColor = Color.yellow;
        }

        StartCoroutine(SwitchDirectionRoutine()); // Start periodic direction flip
    }

    private void Update()
    {
        if (player == null) return;

        bool canSeePlayer = CanSeePlayer();
        Debug.Log("Can see player: " + canSeePlayer); // Debug to check vision status

        if (canSeePlayer)
        {
            timePlayerInSight += Time.deltaTime; // Increment time when player is in sight
            Debug.Log("Player in sight for: " + timePlayerInSight + " seconds"); // Debug the timer
        }
        else
        {
            timePlayerInSight = 0f; // Reset if the player is out of sight
        }

        if (timePlayerInSight > 1f) // Check if the player has been in sight for more than 1 second
        {
            SceneManager.LoadScene("CaughtScene");
            Debug.Log("dead"); 
            // Print "dead" if the player has been in sight for 1 second
        }

        FlipGuardDirection(); // Flip vision cone direction
        UpdateVisionCone();
    }

    IEnumerator SwitchDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchDirectionInterval);
            rotatingClockwise = !rotatingClockwise; // Flip rotation direction
        }
    }

    void FlipGuardDirection()
    {
        // Flip the guard's scale along the x-axis to reverse the direction
        if (rotatingClockwise)
        {
            if (isFlipped)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFlipped = false;
            }
        }
        else
        {
            if (!isFlipped)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFlipped = true;
            }
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange) return false;

        // Flip the detection direction when the guard is flipped
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(isFlipped ? -transform.right : transform.right, directionToPlayer); // Use flipped direction if needed
        if (angleToPlayer > fieldOfViewAngle / 2f) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    void UpdateVisionCone()
    {
        if (visionCone == null) return;

        Vector2 origin = transform.position;
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -fieldOfViewAngle / 2) * (isFlipped ? -transform.right : transform.right) * detectionRange;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, fieldOfViewAngle / 2) * (isFlipped ? -transform.right : transform.right) * detectionRange;

        visionCone.SetPosition(0, origin); // Guard position
        visionCone.SetPosition(1, (Vector2)origin + leftBoundary); // Left vision boundary
        visionCone.SetPosition(2, (Vector2)origin + rightBoundary); // Right vision boundary
    }
}
