using UnityEngine;

public class SatisfactionHandler : MonoBehaviour
{
    public Sprite[] satisfactionSprites; // Assign your 25 sprites in the Inspector
    public int satisfactionLevel = 24; // Start in the middle (adjust as needed)
    private int maxSatisfaction; // Max index for sprites
    private int minSatisfaction = 0; // Minimum sprite index
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxSatisfaction = satisfactionSprites.Length - 1;
        UpdateSatisfactionMeter();
    }

    public void AdjustSatisfaction(int change)
    {
        satisfactionLevel += change;
        satisfactionLevel = Mathf.Clamp(satisfactionLevel, minSatisfaction, maxSatisfaction);
        UpdateSatisfactionMeter();
    }

    private void UpdateSatisfactionMeter()
    {
        spriteRenderer.sprite = satisfactionSprites[satisfactionLevel];
    }
}
