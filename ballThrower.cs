// using UnityEngine;

// public class BallThrower : MonoBehaviour
// {
//     public Rigidbody ballRigidbody;
//     public float throwForce = 500f; // Adjust as needed
//     private bool isThrown = false;

//     void Update()
//     {
//         // Aim the ball left or right using the mouse movement
//         float horizontal = Input.GetAxis("Mouse X") * 5f;
//         transform.Rotate(0, horizontal, 0);

//         // Throw the ball when the left mouse button is clicked
//         if (Input.GetMouseButtonDown(0) && !isThrown)
//         {
//             ThrowBall();
//         }
//     }

//     void ThrowBall()
//     {
//         isThrown = true; // Prevents multiple throws
//         ballRigidbody.velocity = Vector3.zero; // Reset velocity
//         ballRigidbody.angularVelocity = Vector3.zero;
//         ballRigidbody.AddForce(transform.forward * throwForce);
//     }
// }
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public float throwForce = 15f; // Reduced for better control
    private bool isThrown = false;

    void Update()
    {
        // Move the ball left/right using Arrow Keys or A/D
        float horizontal = Input.GetAxis("Horizontal") * 5f * Time.deltaTime;
        transform.position += new Vector3(horizontal, 0, 0);

        // Throw the ball when clicking the left mouse button
        if (Input.GetMouseButtonDown(0) && !isThrown)
        {
            ThrowBall();
        }

        // Reset ball if it falls too low or flies too far
        if (transform.position.y < -5 || transform.position.z > 20)
        {
            ResetBall();
        }
    }

    // void ThrowBall()
    // {
    //     isThrown = true;

    //     // Reset velocity to avoid rolling
    //     ballRigidbody.velocity = Vector3.zero;
    //     ballRigidbody.angularVelocity = Vector3.zero;

    //     // Apply a smooth throwing arc (Forward + Up)
    //     Vector3 throwDirection = (transform.forward * 0.8f + Vector3.up * 1.2f).normalized;
    //     ballRigidbody.AddForce(throwDirection * Mathf.Clamp(throwForce, 5f, 20f), ForceMode.Impulse);
    // }
    void ThrowBall()
    {
        isThrown = true;

        // Reset velocity to avoid rolling
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        // Apply a smooth throwing arc with better forward movement
        Vector3 throwDirection = (transform.forward * 1.2f + Vector3.up * 0.8f).normalized;
        ballRigidbody.AddForce(throwDirection * Mathf.Clamp(throwForce, 5f, 15f), ForceMode.Impulse);
    }

    void ResetBall()
    {
        isThrown = false;
        transform.position = new Vector3(0, 1f, -4); // Reset to floating position
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }
}
