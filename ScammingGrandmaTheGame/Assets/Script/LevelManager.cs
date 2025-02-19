using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Function to load the first scene in the build order
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0); // Loads the first scene in Build Settings
    }
}
