using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string taskName;
        public Toggle taskToggle;
        public GameObject taskPrefab; // Will be set dynamically
    }

    public List<Task> taskList; // The 4 task slots (each linked to a toggle)
    public string[] possibleTasks = { "Bring meal to 101", "Bring meal to 102", 
        "Bring meal to 103", "Bring meal to 104", "Bring blanket to 101", 
        "Bring blanket to 102", "Bring blanket to 103", "Bring blanket to 104", 
        "Mark spill in hall", "Mark spill in kitchen"}; 
        //"Water tomatoes", "Water roses", "Mark spill in hall", "Mark spill in kitchen"
    public List<GameObject> allTaskPrefabs; // All available task prefabs in the scene

    private Dictionary<string, Toggle> taskToggleMap = new Dictionary<string, Toggle>();

    void Start()
    {
        AssignRandomChecklist();
    }

    void AssignRandomChecklist()
    {
        if (possibleTasks.Length < taskList.Count)
        {
            Debug.LogError("Not enough tasks to assign uniquely to all toggles!");
            return;
        }

        if (allTaskPrefabs == null || allTaskPrefabs.Count == 0)
        {
            Debug.LogError("No task prefabs assigned in allTaskPrefabs list!");
            return;
        }

        List<string> availableTasks = new List<string>(possibleTasks);
        taskToggleMap.Clear();
        List<string> selectedTaskNames = new List<string>();
        List<GameObject> selectedPrefabs = new List<GameObject>();

        // Randomly assign 4 tasks
        for (int i = 0; i < taskList.Count; i++)
        {
            int randomIndex = Random.Range(0, availableTasks.Count);
            string chosenTask = availableTasks[randomIndex].Trim();

            // Find a matching prefab
            GameObject matchingPrefab = allTaskPrefabs.Find(prefab => 
            {
                pickupDropoff taskScript = prefab.GetComponent<pickupDropoff>();
                return taskScript != null && taskScript.taskName.Trim() == chosenTask;
            });

            if (matchingPrefab == null)
            {
                Debug.LogError($"No matching prefab found for task: {chosenTask}. Ensure allTaskPrefabs contains all needed prefabs.");
                continue;
            }

            // Assign task name and prefab
            taskList[i].taskName = chosenTask;
            taskList[i].taskPrefab = matchingPrefab;
            taskToggleMap[chosenTask] = taskList[i].taskToggle;
            selectedTaskNames.Add(chosenTask);
            selectedPrefabs.Add(matchingPrefab);
            availableTasks.RemoveAt(randomIndex);

            // ✅ Ensure toggle text updates
            Text toggleText = taskList[i].taskToggle.GetComponentInChildren<Text>();
            if (toggleText != null)
            {
                toggleText.text = chosenTask;  // ✅ Update toggle UI text
            }
            else
            {
                Debug.LogError("No Text component found inside toggle for task: " + chosenTask);
            }
        }

        Debug.Log("Selected tasks this round: " + string.Join(", ", selectedTaskNames));

        // Activate only selected prefabs
        foreach (GameObject taskPrefab in allTaskPrefabs)
        {
            if (selectedPrefabs.Contains(taskPrefab))
            {
                taskPrefab.SetActive(true);
                Debug.Log(taskPrefab.GetComponent<pickupDropoff>().taskName + " has been activated!");
            }
            else
            {
                taskPrefab.SetActive(false);
                Debug.Log(taskPrefab.GetComponent<pickupDropoff>().taskName + " has been deactivated.");
            }
        }
    }

    // Function to get the correct toggle for a given task
    public Toggle GetToggleForTask(string taskName)
    {
        return taskToggleMap.ContainsKey(taskName) ? taskToggleMap[taskName] : null;
    }
}