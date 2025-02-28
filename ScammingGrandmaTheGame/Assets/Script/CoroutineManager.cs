using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public SatisfactionHandler satisfactionHandler;

    private void Awake()
    {
        if (satisfactionHandler == null)
        {
            satisfactionHandler = FindObjectOfType<SatisfactionHandler>();  // Ensure the reference is assigned
        }
    }

    // Call this method to start the satisfaction increase
    public void IncreaseSatisfaction()
    {
        if (satisfactionHandler != null)
        {
            satisfactionHandler.StartCoroutine(satisfactionHandler.AddSatisfactionCoroutine(5, 1f)); // Increase satisfaction by 10 over 2 seconds
        }
        else
        {
            Debug.LogError("SatisfactionHandler not found.");
        }
    }
}
