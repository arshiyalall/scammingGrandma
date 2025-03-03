using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateringCanAction : MonoBehaviour
{
    bool watering = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.)
        if (Input.GetKeyDown("e")){
            watering = true;
            Debug.Log("Watering!");
        }
        else {
            watering = false;
        }
    }
}
