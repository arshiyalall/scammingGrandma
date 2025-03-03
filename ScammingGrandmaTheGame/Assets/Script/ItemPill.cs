using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPill : MonoBehaviour
{
    public GameObject spawnpoint;
    public GameObject itemArt;
    public Toggle myToggle;
    public static bool[] pickedUpPills;
    public bool pickedUpThisRound;
    public int itemIndex;

    void Awake() {
        if (pickedUpPills == null)
        {
            pickedUpPills = new bool[5];
        }
    }
    
    void Start()
    {
        if (pickedUpPills[itemIndex]) {
            itemArt.SetActive(false);
            myToggle.isOn = true;
        } else {
            //Spawn at spawnpoint
            transform.position = spawnpoint.transform.position;
            myToggle.isOn = false;
        }
        pickedUpThisRound = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myToggle.isOn = true;
            itemArt.SetActive(false);
            pickedUpPills[itemIndex] = true;
            pickedUpThisRound = true;
        }
    }
}
