using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeNight : MonoBehaviour
{
    public bool inOffice;

    public CoroutineManager couroutineManager;
    public ItemPill[] pillScripts;
    public int moneyThisRound;

    // Start is called before the first frame update
    void Start()
    {
        inOffice = true;
        moneyThisRound = 0;
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
            //Check which pills have been collected and add random money
            for (int i = 0; i < 5; i++) {
                if (pillScripts[i].pickedUpPills) {
                    
                }
            }
            //Debug.Log();
            couroutineManager.endNight();
        }
    }
}
