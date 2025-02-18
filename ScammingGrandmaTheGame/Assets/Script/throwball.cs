


using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject throwObjectPrefab;
    public Transform throwPoint;
    public float throwForce = 10f; // Strength of throw

    private bool hasThrown = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasThrown)
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject newObject = Instantiate(throwObjectPrefab,
            throwPoint.position, Quaternion.identity);

        // Get Rigidbody2D
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();

        // Apply force to throw right
        rb.AddForce(Vector2.right * throwForce, ForceMode2D.Impulse);

        hasThrown = true;
    }
}
