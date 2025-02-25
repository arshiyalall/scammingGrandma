using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false; // Track whether the game is paused
    public GameObject pauseMenuUI; // Assign the PauseMenu UI in Unity
    public Button myButton;

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(TogglePause); // Assign the button click function
        }
        else
        {
            Debug.LogError("Pause button not assigned!");
        }

        pauseMenuUI.SetActive(false);
    }

    void Update()
    {

    }

    public void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume normal game speed
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze game time
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed! (This will work in a built game)");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}