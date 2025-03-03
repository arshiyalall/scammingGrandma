using UnityEngine;

public class Enemy_Basic : Enemy
{
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
