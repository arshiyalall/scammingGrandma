using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    float moveSpeed = 4;
    private Animator animator;
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
        //Sprite is idle
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

    //collision refers to the game object which hits this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Remove
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            animator.SetBool("isHolding", true);
        }
    }
}