using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Sin : Enemy
{
    public override void Move()
    {
        float sinInput = 3 * Time.realtimeSinceStartup * Mathf.PI;
        float newX = transform.position.x - moveSpeed * Time.deltaTime;
        transform.position = new Vector3(newX, Mathf.Sin(sinInput), 0f);
    }
}
