using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPill : MonoBehaviour
{
    public GameObject spawnpoint;
    public GameObject itemArt;
    public Toggle myToggle;

    void Start()
    {
        //Spawn at spawnpoint
        transform.position = spawnpoint.transform.position;
        myToggle.isOn = false;
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
        }
    }
}
