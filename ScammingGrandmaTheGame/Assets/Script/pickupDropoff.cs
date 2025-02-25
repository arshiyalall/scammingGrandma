using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupDropoff : MonoBehaviour
{
    private Vector2 lastDirection;
    private bool pickedUp = false;
    private bool droppedOff = false;
    public GameObject itemArt;
    public GameObject scammer;
    public GameObject spawnpoint;
    public GameObject despawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn at spawnpoint
        transform.position = spawnpoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get last direction player moved
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lastDirection = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lastDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lastDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lastDirection = Vector2.down;
        }

        //Move with player
        if (itemArt != null && pickedUp)
        {
            itemArt.transform.position = scammer.transform.position + (Vector3)lastDirection * 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !droppedOff)
        {
            pickedUp = true;
        } else if (collision.gameObject == despawnpoint) 
        {
            itemArt.transform.position = despawnpoint.transform.position;
            pickedUp = false;
            droppedOff = true;
        }
    }
}
