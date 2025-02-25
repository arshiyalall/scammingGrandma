using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour{

    private GameObject player;
    private string sceneName;
    private int grandmaSatisfaction;

    //information to gather
    private bool gotSSN;
    private bool gotMaiden;
    private bool gotAddress;
    private bool gotBankInfo;

    private bool caughtByGuard;

    private int numTasksCompleted;
    private int numTasksAssigned;
    public Button startButton;

    // struct task {
    //     string taskName;
    //     bool taskCompleted;
    // }

    // private std::vector<task> taskArr;

    void Start(){
        // SceneManager.LoadScene("StartScreen");
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        startButton.onClick.AddListener(StartGame);
    }


    //Each possible task function will be called if a bool is set to true.
    //    This is so we can effectively choose which tasks the player has to 
    //    complete for each day phase (and add more tasks if they get caught 
    //    in night mode)


    public void StartGame() {
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

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }
}    


