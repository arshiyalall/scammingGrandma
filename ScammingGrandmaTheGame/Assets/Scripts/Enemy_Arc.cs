using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Arc : Enemy
{
    public override void Move()
    {
        transform.RotateAround(target, Vector3.forward, 45f * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
