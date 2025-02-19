using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import SceneManager

public class Checklist : MonoBehaviour
{
    public GameObject paperItem;
    public GameObject currScene;
    private Animator anim;

    private MoveSprite moveSprite; // Reference to MoveSprite

    void Start()
    {
        // Find the MoveSprite script in the scene
        moveSprite = FindObjectOfType<MoveSprite>();

        if (moveSprite == null)
        {
            Debug.LogError("MoveSprite script not found in the scene!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            paperItem.transform.Translate(Vector2.left * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("paper"))
        {
            // Check if moveSprite is found and if toggles has at least 2 elements
            if (moveSprite != null && moveSprite.toggles.Count > 1)
            {
                moveSprite.toggles[1].isOn = true; // Turn on toggle[1]
                
                // Start coroutine to delay and load next scene
                StartCoroutine(DelayedSceneChange());
            }
            else
            {
                Debug.LogError("MoveSprite reference is missing or toggles list is too short!");
            }

            Destroy(collision.gameObject);
            Destroy(currScene.gameObject);
        }
    }

    IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Load the next scene (Change "NextSceneName" to your actual scene name)
        SceneManager.LoadScene("NextLevel");
    }
}
