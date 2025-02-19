using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSprite : MonoBehaviour
{
    float moveSpeed = 4;
    private Animator animator;
    public List<Toggle> toggles;
    public GameObject ballObject;
    public float throwForce = 10f;
    private bool hasThrown = false;
    private Vector3 initialBallPosition;
    private Vector2 lastDirection = Vector2.right; // Default direction

    void Start()
    {
        animator = GetComponent<Animator>();
        
        // Ensure all toggles are initially off
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }

        if (ballObject != null)
        {
            initialBallPosition = ballObject.transform.position;
            ballObject.SetActive(false); // Hide the ball at the start
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection(Vector2.right, 1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection(Vector2.left, -1, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection(Vector2.up, 0, 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection(Vector2.down, 0, -1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !hasThrown && ballObject != null && ballObject.activeSelf)
        {
            Throw();
        }

        // Move ball with player if not thrown
        if (ballObject != null && ballObject.activeSelf && !hasThrown)
        {
            ballObject.transform.position = transform.position + (Vector3)lastDirection * 0.5f;
        }

    }

    void moveDirection(Vector2 vector, float x, float y)
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", x);
        animator.SetFloat("InputY", y);
        transform.Translate(vector * moveSpeed * Time.deltaTime);

        // Update last movement direction
        lastDirection = vector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            animator.SetBool("isHolding", true);
            
            // Set all toggles to on
            if (toggles.Count > 0)
            {
                toggles[0].isOn = true;
            }

            // Show the ball when item is picked up
            if (ballObject != null)
            {
                ballObject.SetActive(true);
                ballObject.transform.position = transform.position + (Vector3)lastDirection * 0.5f;
                hasThrown = false; // Reset throw state
            }
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

        // Detach from player
        ballObject.transform.SetParent(null);

        // Restore original position before throwing
        ballObject.transform.position = transform.position + (Vector3)lastDirection * 0.5f;

        // Get Rigidbody2D
        Rigidbody2D rb = ballObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = ballObject.AddComponent<Rigidbody2D>();
        }

        // Set Rigidbody2D properties
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // Apply force in the last movement direction
        rb.velocity = lastDirection * throwForce;

        hasThrown = true;

        // Disable the ball after a short delay instead of destroying it
        StartCoroutine(DisableBallAfterTime(2f)); // Disables ball after 2 seconds
    }

    IEnumerator DisableBallAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        if (ballObject != null)
        {
            ballObject.SetActive(false);
        }
    }
}
