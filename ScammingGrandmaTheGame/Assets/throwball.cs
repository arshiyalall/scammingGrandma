
// using UnityEngine;

// public class ThrowBall : MonoBehaviour
// {
//     public GameObject ballObject;  // Existing ball in the scene (not a prefab)
//     public float throwForce = 10f; // Strength of throw

//     private bool hasThrown = false;

//     void Start()
//     {
//         if (ballObject == null)
//         {
//             Debug.LogError("❌ BallObject is NULL! Assign an existing ball from the scene in the Inspector.");
//             return;
//         }

//         // Attach the existing ball to the stick figure
//         ballObject.transform.position = transform.position;  // Set ball's position to stick figure's position
//         ballObject.transform.SetParent(transform);  // Attach it to stickfig
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && !hasThrown)
//         {
//             Throw();
//         }
//     }

//     void Throw()
//     {
//         if (ballObject == null)
//         {
//             Debug.LogError("❌ No ball found! Ensure a ball exists in the scene.");
//             return;
//         }

//         Debug.Log("🚀 Throwing the ball...");

//         // Detach from stick figure
//         ballObject.transform.SetParent(null);

//         // Get Rigidbody2D
//         Rigidbody2D rb = ballObject.GetComponent<Rigidbody2D>();
//         if (rb == null)
//         {
//             rb = ballObject.AddComponent<Rigidbody2D>(); // Add Rigidbody2D if missing
//         }

//         // Set Rigidbody2D properties
//         rb.bodyType = RigidbodyType2D.Dynamic;
//         rb.gravityScale = 0;
//         rb.freezeRotation = true;

//         // Apply force to throw right
//         rb.AddForce(Vector2.right * throwForce, ForceMode2D.Impulse);

//         hasThrown = true;
//     }
// }

using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballObject;  // Existing ball in the scene (not a prefab)
    public float throwForce = 10f; // Strength of throw

    private bool hasThrown = false;
    private Vector3 initialBallPosition;  // Store the ball's starting position

    void Start()
    {
        if (ballObject == null)
        {
            Debug.LogError("❌ BallObject is NULL! Assign an existing ball from the scene in the Inspector.");
            return;
        }

        // Store the ball's starting position
        initialBallPosition = ballObject.transform.position;

        // Attach the ball to the stick figure (but don't change its position)
        ballObject.transform.SetParent(transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasThrown)
        {
            Throw();
        }
    }

    void Throw()
    {
        if (ballObject == null)
        {
            Debug.LogError("❌ No ball found! Ensure a ball exists in the scene.");
            return;
        }

        Debug.Log("🚀 Throwing the ball...");

        // Detach from stick figure
        ballObject.transform.SetParent(null);

        // Restore the ball's original position before throwing
        ballObject.transform.position = initialBallPosition;

        // Get Rigidbody2D
        Rigidbody2D rb = ballObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = ballObject.AddComponent<Rigidbody2D>(); // Add Rigidbody2D if missing
        }

        // Set Rigidbody2D properties
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // Apply force to throw right
        rb.AddForce(Vector2.right * throwForce, ForceMode2D.Impulse);

        hasThrown = true;
    }
}
