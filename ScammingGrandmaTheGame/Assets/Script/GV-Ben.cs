using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class GV : MonoBehaviour
{
    [Header("Vision Settings")]
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 90f;
    public float rotationSpeed = 2f; 
    public float switchDirectionInterval = 3f; 

    [Header("References")]
    public Transform player;
    public LayerMask obstacleMask;
    public ItemPill[] pillScripts;
    
    private LineRenderer visionCone; 
    private bool isFlipped = false; 
    private float timePlayerInSight = 0f; 
    private bool rotatingClockwise = true;

    [Header("Vision Rendering")]
    public int resolution = 30; // Number of rays to generate a smooth cone

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null) player = playerObject.transform;
            else Debug.LogError("❌ Player not found! Make sure the Player has the 'Player' tag.");
        }

        visionCone = GetComponent<LineRenderer>();
        if (visionCone == null)
            visionCone = gameObject.AddComponent<LineRenderer>();

        visionCone.positionCount = resolution + 2; // Start point + FOV points + closing back
        visionCone.startWidth = 0.05f;
        visionCone.endWidth = 0.05f;
        visionCone.useWorldSpace = true;
        visionCone.material = new Material(Shader.Find("Sprites/Default"));
        visionCone.startColor = Color.yellow;
        visionCone.endColor = Color.yellow;

        StartCoroutine(SwitchDirectionRoutine());
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        bool canSeePlayer = CanSeePlayer();
        if (canSeePlayer)
        {
            timePlayerInSight += Time.deltaTime;
            if (timePlayerInSight > 0.01f) 
            {
                for (int i = 0; i < 5; i++) {
                    if (pillScripts[i].pickedUpThisRound) {
                        ItemPill.pickedUpPills[i] = false;
                    }
                }
                SceneManager.LoadScene("CaughtScene");
            }
        }
        else
        {
            timePlayerInSight = 0f;
        }

        FlipGuardDirection();
        DrawVisionCone();
    }

    IEnumerator SwitchDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchDirectionInterval);
            rotatingClockwise = !rotatingClockwise;
        }
    }

    void FlipGuardDirection()
    {
        if (rotatingClockwise && isFlipped)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }
        else if (!rotatingClockwise && !isFlipped)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange) return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(isFlipped ? -transform.right : transform.right, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2f) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleMask);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void DrawVisionCone()
    {
        Vector3 origin = transform.position;
        List<Vector3> points = new List<Vector3> { origin };

        float halfFOV = fieldOfViewAngle / 2f;
        float angleStep = fieldOfViewAngle / resolution;
        float directionMultiplier = isFlipped ? -1f : 1f;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = -halfFOV + (angleStep * i);
            Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * Vector2.right * directionMultiplier;
            Vector3 direction = transform.TransformDirection(rotatedDirection);

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectionRange, obstacleMask);
            Vector3 endPoint = hit.collider ? hit.point : origin + (Vector3)rotatedDirection * detectionRange;

            points.Add(endPoint);
        }

        points.Add(origin); // Close the cone
        visionCone.positionCount = points.Count;
        visionCone.SetPositions(points.ToArray());
    }
}
