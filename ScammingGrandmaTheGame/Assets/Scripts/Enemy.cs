using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 target;
    private int health;

    private void Start()
    {
        health = 3;
        moveSpeed = 3;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        Move();    
    }

    public abstract void Move();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Destroy(other);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Projectile"))
        {
            Destroy(other);

            health--;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
