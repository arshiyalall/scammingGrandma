using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Switch name of class
public class WPBGameHandlerHasArray : MonoBehaviour{

    private GameObject player;
    private string sceneName;
    private int grandmaSatisfaction;

    private bool caughtByGuard;

    private int numTasksCompleted;
    private int numTasksAssigned;

    private int startint=0;
    private int updateint=0;

    // private GameObject[10] taskArr;

    // struct task {
    //     string taskName;
    //     bool taskCompleted;
    // }

    // private std::vector<task> taskArr;

    void Start(){
        Debug.Log("Starting: " + startint);
        startint++;
        // SceneManager.LoadScene("StartScreen");
        if (Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Hell yeah\n");
            // Debug.LogError("Damn mf we pressed escape\n");
            // SceneManager.LoadScene("PauseMenu");
        }

        // sceneName = SceneManager.GetActiveScene().name;
        // player = GameObject.FindWithTag("Player");
        // instantianteTaskArr();
    }

    void Update(){
        Debug.Log("Updating: " + updateint);
        updateint++;   
        Debug.Log("after updating" + updateint + "\n");


        if (! (Input.GetKeyDown(KeyCode.Q))){
            Debug.Log("Hell yeah\n");
            // Debug.LogError("Damn mf we pressed escape\n");
            // SceneManager.LoadScene("PauseMenu");
        }
        
         if ((Input.GetKeyDown(KeyCode.Q))){
            Debug.Log("Hell yeah key press\n");
            // Debug.LogError("Damn mf we pressed escape\n");
            // SceneManager.LoadScene("PauseMenu");
        }
        
        //  if (Input.GetKeyDown(KeyCode.Escape)){
        //     Debug.Log("Hell yeah\n");
        //     Debug.LogError("Damn mf we pressed escape\n");
        //     SceneManager.LoadScene("PauseMenu");
        // }
    }


    //populate the task array with objects of the correct tag
    private void instantianteTaskArr(){
        // taskArr[0] = GameObject.FindWithTag("paper");
        // taskArr[1] = GameObject.FindWithTag("plate");
        // taskArr[2] = GameObject.FindWithTag("wateringCan");


    }

    //Each possible task function will be called if a bool is set to true.
    //    This is so we can effectively choose which tasks the player has to 
    //    complete for each day phase (and add more tasks if they get caught 
    //    in night mode)


    public void StartGame() {
        Debug.Log("Inside Startgame\n");
        SceneManager.LoadScene("DaytimePhase");

        //set default values
        grandmaSatisfaction = 100;
        numTasksCompleted = 0;
        numTasksAssigned = 5;
    }

      // Return to MainMenu
      public void RestartGame() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("StartScreen");
                // Please also reset all static variables here, for new games!
      }


    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    //call this at the end of the day
    //+1 task every day, +3 if you do not finish tasks
    public void adjustNumTasks() {

        //add 1 new task every day
        numTasksAssigned += 1;

        //2 extra tasks if caught by the guard
        if (caughtByGuard) {
            numTasksAssigned += 2;
        }

        //reset number of tasks completed
        numTasksCompleted = 0;
    }

    public void incrementTasks(){
        numTasksCompleted++;
    }

    public void spawnResistanceBand() {
        //resistanceBand visibility true
    }

    public void spawnFood() {
        //food visibility true
    }

    public void nextDay() {
        SceneManager.LoadScene("NextDay");
    }

    public void youveBeenCaught() {
        SceneManager.LoadScene("youveBeenCaught");
    }
    
    public void nightScreen() {
        SceneManager.LoadScene("nightScreen");
    }

    //Functions for opening scenes. 
    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Pause() {
        //remember to pause timer. 
        SceneManager.LoadScene("Pause");
    }

    public void printOnKeyPress(){
        for (int i = 0; i< 100; i++){
        Debug.Log("Yay you pressed this key!");
    }
    }

    public void Credits() {
        for (int i = 0; i < 100; i++) {
            Debug.Log("In Credits");
        }
        SceneManager.LoadScene("Credits");
    }
}    


