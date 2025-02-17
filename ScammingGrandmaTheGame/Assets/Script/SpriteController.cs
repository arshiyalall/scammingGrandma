using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSprite : MonoBehaviour
{
    float moveSpeed = 4;
    private Animator animator;
    public List<Toggle> toggles; // List to hold multiple toggles

    void Start()
    {
        animator = GetComponent<Animator>();
        
        // Ensure all toggles are initially off
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection(Vector2.right, 0, 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection(Vector2.left, 0, -1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection(Vector2.up, 1, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection(Vector2.down, -1, 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void moveDirection(Vector2 vector, float x, float y)
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", x);
        animator.SetFloat("InputY", y);
        transform.Translate(vector * moveSpeed * Time.deltaTime);
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
        }
    }
}
