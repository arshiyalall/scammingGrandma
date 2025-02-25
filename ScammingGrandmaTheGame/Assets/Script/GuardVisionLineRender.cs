using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class GuardVisionLineRenderer : MonoBehaviour
{
    [Header("Vision Settings")]
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 90f;
    public int resolution = 30; // Number of lines used to generate the cone

    [Header("References")]
    public Transform guard; // Reference to the Guard GameObject
    public LayerMask obstacleMask;

    private LineRenderer lineRenderer;
    private bool isFlipped = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution + 3; // Start point + resolution points + end point + closing back to guard
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true;

        // Set the line color to yellow
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }

    private void Update()
    {
        if (guard == null) return;

        // Check if the guard's direction has been flipped and update line renderer accordingly
        FlipDirection();
        
        DrawVisionCone();
    }

    private void FlipDirection()
    {
        // Detect if the guard's x-scale has changed (flipped)
        bool flipped = guard.localScale.x < 0;

        if (flipped != isFlipped)
        {
            isFlipped = flipped;
            DrawVisionCone();  // Redraw vision cone on direction flip
        }
    }

    private void DrawVisionCone()
    {
        List<Vector3> points = new List<Vector3>();

        Vector2 origin = guard.position;
        points.Add(origin); // Start at guard position (center of vision)

        float halfFOV = fieldOfViewAngle / 2f;
        float angleStep = fieldOfViewAngle / resolution;

        // Flip the direction for the cone based on guard's flipped state
        float directionMultiplier = isFlipped ? -1f : 1f;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = -halfFOV + (angleStep * i);

            // Apply the guard's rotation and flip the direction when necessary
            Vector2 rotatedDirection = guard.TransformDirection(Quaternion.Euler(0, 0, angle) * Vector2.right);
            rotatedDirection *= directionMultiplier;

            // Check if an obstacle is blocking the view
            RaycastHit2D hit = Physics2D.Raycast(origin, rotatedDirection, detectionRange, obstacleMask);
            Vector3 endPoint = hit.collider ? hit.point : (Vector2)origin + rotatedDirection * detectionRange;

            points.Add(endPoint);
        }

        points.Add(origin); // **Close the vision cone by adding the guard's position again**

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
