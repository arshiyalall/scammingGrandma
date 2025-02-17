using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checklist : MonoBehaviour
{
    public GameObject paperItem;
    public GameObject currScene;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            paperItem.transform.Translate(Vector2.left * Time.deltaTime);
        }
    }

    //collision refers to the game object which hits this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When an item collides with the bed
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            Destroy(currScene.gameObject);
        }
    }
}
