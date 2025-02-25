using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Fix diagonal speed doubling issue
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
