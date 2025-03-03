using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour {
    public int timer = 60;
    private float theTimer = 0f;
    public GameObject timerText;
    public CoroutineManager coroutineManager;

    void FixedUpdate(){
        theTimer += 0.01f;
        if (theTimer >= 1f){
            timer -=1;
            theTimer = 0;
            UpdateTimer();
        }
            
        if (timer <= 0) {
        timerEnded();    
        }
        theTimer += 0.01f;
        if (theTimer >= 1f){
            timer -=1;
            theTimer = 0;
            UpdateTimer();
        }
            
        if (timer <= 0) {
        timerEnded();    
        }
    } 

    public void UpdateTimer(){
        Text timeTextTemp = timerText.GetComponent<Text>();
        timeTextTemp.text = "" + timer;
    }

    public void timerEnded(){
        UnityEngine.Debug.Log("Timer has reached 0");
        
        
        //Here is the place where we will transition to the nightscreen. But it is not working. 
        
        coroutineManager.endNight();

    }
}