using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoroutineManager : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeSpeed = 1f;
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

    public void endDay()
    {
        StartCoroutine(FadeAndLoadSceneRoutine());
    }
    IEnumerator FadeAndLoadSceneRoutine()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("NightScreen");
    }

    public void startNight() {
        StartCoroutine(LoadNightRoutine());
    }

    IEnumerator LoadNightRoutine() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("NightimeCOPY");
    }

    public void endNight() {
        StartCoroutine(FadeAndLoadDayRoutine());
    }

    IEnumerator FadeAndLoadDayRoutine() {
        //Wait a little longer so player can see money earned
        yield return new WaitForSeconds(1.5f);

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("NextDayScene");
    }

    public void startDay() {
        StartCoroutine(LoadDayRoutine());
    }

    IEnumerator LoadDayRoutine() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("DaytimePhase");
    }
}

