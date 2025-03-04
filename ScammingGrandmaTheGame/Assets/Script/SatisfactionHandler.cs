using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SatisfactionHandler : MonoBehaviour
{
    public Sprite[] satisfactionSprites; // Assign your 25 sprites in the Inspector
    public static int satisfactionLevel;
    private int maxSatisfaction; // Max index for sprites
    private int minSatisfaction = 0; // Minimum sprite index
    private Image imageComponent;
    //public float decreaseRate = 10f;
    private float decreaseTimer = 0f;
    public float decreaseInterval = 10f;

    void Awake() {
        //Set satisfaction to max to start
        maxSatisfaction = satisfactionSprites.Length - 1;

        if (satisfactionLevel == 0) {
            satisfactionLevel = maxSatisfaction;
        }
    }

    void Start()
    {
        imageComponent = GetComponent<Image>();
        UpdateSatisfactionMeter();
    }

    void FixedUpdate()
    {
        decreaseTimer += Time.deltaTime;
        if (decreaseTimer >= decreaseInterval) {
            //Reset timer
            decreaseTimer = 0f;

            AdjustSatisfaction(-1);

            if (satisfactionLevel <= minSatisfaction) {
                SceneManager.LoadScene("GameLoseScene");
            }
        }
    }

    public void AdjustSatisfaction(int change)
    {
        satisfactionLevel += change;
        satisfactionLevel = Mathf.Clamp(satisfactionLevel, minSatisfaction, maxSatisfaction);
        UpdateSatisfactionMeter();
    }

    private void UpdateSatisfactionMeter()
    {
        imageComponent.sprite = satisfactionSprites[satisfactionLevel];
    }

    public IEnumerator AddSatisfactionCoroutine(int amount, float duration)
    {
        //Total number of satisfaction steps to incremement
        int steps = Mathf.Abs(amount);
        //Duration of each step
        float stepDuration = duration / steps;
        
        for (int i = 0; i < steps; i++)
        {
            // Add one satisfaction level
            satisfactionLevel += (amount > 0) ? 1 : -1;
            satisfactionLevel = Mathf.Clamp(satisfactionLevel, minSatisfaction, maxSatisfaction);
            UpdateSatisfactionMeter();

            // Wait for the next step
            yield return new WaitForSeconds(stepDuration);
        }
    }
}