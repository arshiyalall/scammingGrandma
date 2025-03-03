using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    public int timer = 60;
=======
    public int timer = 60;
>>>>>>> Stashed changes
=======
    public int timer = 20;
>>>>>>> Stashed changes
    private float theTimer = 0f;
    public GameObject timerText;

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
        
        SceneManager.LoadScene("NightScreen");

    }
}