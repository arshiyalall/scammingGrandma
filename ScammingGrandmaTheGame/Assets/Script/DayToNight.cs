using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayToNight : MonoBehaviour
{

    public CoroutineManager coroutineManager;
    // Start is called before the first frame update
    void Start()
    {
        coroutineManager.startNight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
