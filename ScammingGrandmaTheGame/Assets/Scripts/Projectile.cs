using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 5f;

    void Start()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = target - transform.position;
        dir.Normalize();

        transform.up = dir;
        GetComponent<Rigidbody2D>().velocity = dir * speed;

        StartCoroutine(DeleteAfter());
    }

    IEnumerator DeleteAfter()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
