using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtToDay : MonoBehaviour
{
    public CoroutineManager coroutineManager;
    // Start is called before the first frame update
    void Start()
    {
        coroutineManager.startDay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
