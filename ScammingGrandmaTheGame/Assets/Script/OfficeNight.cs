using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeNight : MonoBehaviour
{
    public bool inOffice;

    public CoroutineManager couroutineManager;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("collided with inOffice true");
        } else if (collision.CompareTag("Player") && !inOffice) {
            //When player returns to the office
            couroutineManager.endNight();
            Debug.Log("collided with inOffice false");
        }
    }
}
