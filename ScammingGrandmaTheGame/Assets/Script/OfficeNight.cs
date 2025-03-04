using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeNight : MonoBehaviour
{
    public bool inOffice;

    public CoroutineManager coroutineManager;
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
            Debug.Log("Left office");
        } else if (collision.CompareTag("Player") && !inOffice) {
            //When player returns to the office
            //Check which pills have been collected and add random money
            Debug.Log("Entered office");
            for (int i = 0; i < 5; i++) {
                if (pillScripts[i].pickedUpThisRound) {
                    MoneyHandler.money += Random.Range(10000, 15000);
                    Debug.Log(MoneyHandler.money);
                }
            }
            coroutineManager.endNight();
        }
    }
}
