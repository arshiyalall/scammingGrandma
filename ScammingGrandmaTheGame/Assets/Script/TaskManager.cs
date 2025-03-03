using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChecklistManager : MonoBehaviour
{
    public Toggle[] checklistToggles;
    public string[] possibleTasks = { " Bring meal to 101", " Bring meal to 102", 
        " Bring meal to 103", " Bring meal to 104", " Water roses", 
        " Water tomatoes", " Bring blanket to 101", " Bring blanket to 101", 
        " Bring blanket to 102", " Bring blanket to 103", " Bring blanket to 104",
        " Mark spill in hall", " Mark spill in kitchen"};

    private List<string> availableTasks;

    void Start()
    {
        AssignRandomChecklist();
    }

    void AssignRandomChecklist()
    {

        availableTasks = new List<string>(possibleTasks);

        for (int i = 0; i < checklistToggles.Length; i++)
        {
            int randomIndex = Random.Range(0, availableTasks.Count);
            string chosenTask = availableTasks[randomIndex];

            // Assign text to the toggle's Legacy Text child
            Text toggleText = checklistToggles[i].GetComponentInChildren<Text>();
            if (toggleText != null)
            {
                toggleText.text = chosenTask;
            }
            else
            {
                Debug.LogError("No Text component found on toggle " + i);
            }

            availableTasks.RemoveAt(randomIndex);
        }
    }
}
