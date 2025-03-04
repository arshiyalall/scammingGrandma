// using UnityEngine;
// using System.Collections;

// public class MoneySpawner : MonoBehaviour
// {
//     public GameObject moneyPrefab; // Assign the Money prefab in the Inspector
//     public int moneyCount = 20; // Adjust how many bills you want
//     public float spawnInterval = 0.2f; // Time between each spawn
//     public Vector2 spawnArea = new Vector2(800, 600); // Width and height of spawn area

//     void Start()
//     {
//         StartCoroutine(SpawnMoney());
//     }

//     IEnumerator SpawnMoney()
//     {
//         for (int i = 0; i < moneyCount; i++)
//         {
//             Vector3 spawnPosition = new Vector3(
//                 Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
//                 spawnArea.y / 2, // Start from the top
//                 0
//             );

//             GameObject money = Instantiate(moneyPrefab, spawnPosition, Quaternion.identity);
//             money.transform.SetParent(transform, false); // Keep it inside UI canvas if needed

//             Rigidbody2D rb = money.GetComponent<Rigidbody2D>();
//             if (rb != null)
//             {
//                 rb.gravityScale = Random.Range(0.5f, 1.5f); // Add a slight variation
//             }

//             yield return new WaitForSeconds(spawnInterval);
//         }
//     }
// }

// using UnityEngine;

// public class MoneyFall : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     public float minRotationSpeed = -200f; // Minimum rotation speed (counterclockwise)
//     public float maxRotationSpeed = 200f;  // Maximum rotation speed (clockwise)
//     public float fallSpeed = 2f; // Speed at which the money falls

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         // Apply random rotation speed
//         float randomRotation = Random.Range(minRotationSpeed, maxRotationSpeed);
//         rb.angularVelocity = randomRotation;

//         // Set the initial downward velocity
//         rb.velocity = new Vector2(0, -fallSpeed);
//     }
// }

using UnityEngine;
using System.Collections;

public class MoneySpawner : MonoBehaviour
{
    public GameObject moneyPrefab;
    public int moneyCount = 20; // Number of bills
    public float spawnInterval = 0.1f; // How often bills spawn
    public float spawnAreaWidth = 8f; // How spread out bills are
    public float fallSpeed = 2f; // Falling speed

    void Start()
    {
        StartCoroutine(SpawnMoney());
    }

    IEnumerator SpawnMoney()
    {
        for (int i = 0; i < moneyCount; i++)
        {
            float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

            // Spawn money
            GameObject money = Instantiate(moneyPrefab, spawnPosition, Quaternion.Euler(0, 0, Random.Range(-30f, 30f)));

            // Add Rigidbody for smooth falling
            Rigidbody2D rb = money.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0.5f; // Makes them fall at a controlled speed
            rb.drag = 2f; // Adds slight air resistance

            // Randomize the torque (spinning effect)
            rb.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

