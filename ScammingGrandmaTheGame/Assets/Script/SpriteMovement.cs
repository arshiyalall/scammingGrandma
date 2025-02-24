using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
    }

    void moveDirection(Vector2 vector, float x, float y)
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", x);
        animator.SetFloat("InputY", y);
        transform.Translate(vector * moveSpeed * Time.deltaTime);

        // Update last movement direction
        //lastDirection = vector;
    }
}
