using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateringCanAction : MonoBehaviour
{
    private pickupDropoff pickupScript;
    bool watering = false;
    // Start is called before the first frame update
    void Start() {
        pickupScript = GetComponent<pickupDropoff>(); // Get reference to pickupDropoff script
        if (pickupScript == null)
        {
            Debug.LogError("pickupDropoff script not found on watering can!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // && pickupScript != null && pickupScript.IsPickedUp()
        //if (gameObject.)
        if (Input.GetKeyDown("e") ) {
            watering = true;
            Debug.Log("Watering!");
        }
        else {
            watering = false;
        }
    }
}
