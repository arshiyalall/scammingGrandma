using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupDropoff : MonoBehaviour
{
    private Vector2 lastDirection;
    private bool pickedUp = false;
    private bool droppedOff = false;

    public GameObject itemArt;
    public GameObject scammer;
    public GameObject spawnpoint;
    public GameObject despawnpoint;

    public string taskName; // This should match a task name from ChecklistManager
    private Toggle associatedToggle;

    public TaskManager checklistManager;
    public CoroutineManager couroutineManager;

    void Start()
    {
        if (!gameObject.activeSelf) return; // Prevents unselected tasks from running

        if (spawnpoint != null)
        {
            transform.position = spawnpoint.transform.position;
        }
        else
        {
            Debug.LogError("Spawnpoint not assigned for " + gameObject.name);
        }

        if (checklistManager != null)
        {
            associatedToggle = checklistManager.GetToggleForTask(taskName);
            if (associatedToggle == null)
            {
                Debug.LogError("No toggle found for task: " + taskName);
            } else {
                Debug.Log("Toggle found!!!!! " + taskName);
            }
        }
        else
        {
            Debug.LogError("ChecklistManager not assigned in " + gameObject.name);
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) lastDirection = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow)) lastDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow)) lastDirection = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow)) lastDirection = Vector2.down;

        if (itemArt != null && pickedUp)
        {
            itemArt.transform.position = scammer.transform.position + (Vector3)lastDirection * 0.5f;
        }
    }

    public bool IsPickedUp()
    {
        return pickedUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !droppedOff)
        {
            pickedUp = true;
            Debug.Log(gameObject.name + " picked up!");
        }
        else if (collision.gameObject == despawnpoint && !droppedOff)
        {
            couroutineManager.IncreaseSatisfaction();
            itemArt.transform.position = despawnpoint.transform.position;
            pickedUp = false;
            droppedOff = true;

            if (associatedToggle != null)
            {
                associatedToggle.isOn = true;
                Debug.Log(taskName + " task completed! Toggle set to true.");
            }
            else
            {
                Debug.LogError("No toggle found for task: " + taskName);
            }
        }
    }
}
