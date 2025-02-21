using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaylenTestScript : MonoBehaviour
{


    public SatisfactionHandler satisfactionHandler;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            satisfactionHandler.AdjustSatisfaction(1); // Increase satisfaction
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            satisfactionHandler.AdjustSatisfaction(-1); // Decrease satisfaction
        }
    }
}
