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
    void Update()
    {
        if (Input.GetKeyDown("e")){
            watering = true;
        }
        else {
            watering = false;
        }
    }
}
