using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnOffice : MonoBehaviour
{
    public Toggle myToggle;
    public bool inOffice;

    public CoroutineManager couroutineManager;
    public TaskManager taskManager;

    // Start is called before the first frame update
    void Start()
    {
        myToggle.isOn = false;
        inOffice = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && inOffice)
        {
            //When player exits the office initially
            inOffice = false;
        } else if (collision.CompareTag("Player") && !inOffice) {
            //When player returns to the office
            if (taskManager.taskList[0].taskToggle.isOn){
                if (taskManager.taskList[1].taskToggle.isOn){
                    if (taskManager.taskList[2].taskToggle.isOn){
                        if (taskManager.taskList[3].taskToggle.isOn){
                            myToggle.isOn = true;
                            GameHandler.dayNumber++;
                            couroutineManager.IncreaseSatisfaction();
                            couroutineManager.endDay();
                        }
                    }
                }
            }
        }
    }
}
