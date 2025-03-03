using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    private float maxSpeed = 20f;
    private float turnSpeed = 180f;
    private float accel = 10f;
    private float currSpeed;

    private void Start()
    {
        currSpeed = 0f;
    }

    private void Update()
    {
        HandleAccel();
    }

    private void FixedUpdate()
    {
        HandleTurns();
        Move();
    }

    private void HandleTurns()
    {
        float turn = 0;

        if (Input.GetKey(KeyCode.A))
        {
            turn += turnSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            turn -= turnSpeed;
        }

        transform.Rotate(Vector3.forward * turn * Time.fixedDeltaTime);
    }

    private void HandleAccel()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currSpeed += accel * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            currSpeed -= accel * Time.fixedDeltaTime;
        }
    }

    private void Move()
    {
        currSpeed = Mathf.Clamp(currSpeed, 0, maxSpeed);
        transform.position = transform.position + currSpeed * Time.fixedDeltaTime * transform.up;
    }
}
