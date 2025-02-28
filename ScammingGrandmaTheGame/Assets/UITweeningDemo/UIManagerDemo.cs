using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LeanTween;


public class UIManagerDemo : MonoBehaviour
{
    public GameObject startButton, settingsButton, creditButton;
    private RectTransform startButtonRect, scorePanelRect, settingsButtonRect, creditButtonRect;
    private float fadeProgress, slideProgress, pulseProgress;
    private bool isFading, isSliding, isPulsing;
    
    private AnimationCurve fadeEase = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); 
    
    private AnimationCurve slideEase = new AnimationCurve( 
        new Keyframe(0f, 0f, 0f, 3f),      // Starts slow
        new Keyframe(0.3f, 0.5f, 2f, 2f),  // Gains some speed
        new Keyframe(0.6f, 1.3f, 3f, -3f), // Overshoots further for more bounce
        new Keyframe(0.8f, 0.9f, -2f, -2f), // Corrects back towards final position
        new Keyframe(1f, 1f, -1f, 0f)      // Settles in final position
    );
    
    public AnimationCurve pulseEase; // Public curve for pulse animation
   
    // public GameObject scorePanel;
    // public TextMeshProUGUI scoreText;
    // private int score = 0;
    void Start()
    {
        startButtonRect = startButton.GetComponent<RectTransform>();
        settingsButtonRect = settingsButton.GetComponent<RectTransform>();
        creditButtonRect = creditButton.GetComponent<RectTransform>(); 

	    // scorePanelRect = scorePanel.GetComponent<RectTransform>();
	    
        startButton.GetComponent<Image>().color = new Color(0, 255, 215, 255);
        settingsButton.GetComponent<Image>().color = new Color(0, 255, 215, 255);
        creditButton.GetComponent<Image>().color = new Color(0, 255, 215, 255);
	    // startButtonRect.localScale = Vector3.one * 0.8f;
	    
        isFading = true;
	    
        startButtonRect.anchoredPosition = new Vector2(0, 0);
        settingsButtonRect.anchoredPosition = new Vector2(0, -140);
        creditButtonRect.anchoredPosition = new Vector2(0, -278);
	    // scorePanelRect.anchoredPosition = new Vector2(0, 600);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
		    fadeProgress += Time.deltaTime * 1f;
		    float alpha = fadeEase.Evaluate(fadeProgress);
		    startButton.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            settingsButton.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            creditButton.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
		    if (fadeProgress >= 1f) isFading = false;
	    }

        // if (isSliding) 
        // {
	    //     slideProgress += Time.deltaTime * 1.5f;
	    //     scorePanelRect.anchoredPosition = Vector2.LerpUnclamped(new Vector2(0, 600), new Vector2(0, 0), customSlideEase.Evaluate(slideProgress));
	    //     if (slideProgress >= 1f) isSliding = false;
        // }

        // if (isPulsing)
        // {
	    //     pulseProgress += Time.deltaTime * 2f;
	    //     float scale = pulseEase.Evaluate(pulseProgress);
	    //     scorePanel.transform.localScale = Vector3.one * scale;
	    //     if (pulseProgress >= 1f) isPulsing = false;
        // }

        
    }

    // public void OnStartButtonPressed()
    // {
    // 	isSliding = true;
	//     startButtonRect.anchoredPosition = new Vector2(0, -600);
    // }

    // public void IncreaseScore()
    // {
	//     score++;
	//     scoreText.text = "Score: " + score;
	//     pulseProgress = 0f;
	//     isPulsing = true;
    // }

    // public void DecreaseScore()
    // {
    //     score--;
    //     scoreText.text = "Score: " + score;
    //     LeanTween.scale(scorePanel, Vector3.one * 0.9f, 0.2f).setEase(LeanTweenType.easeInOutQuad)
    //     .setOnComplete(() => LeanTween.scale(scorePanel, Vector3.one, 	0.2f).setEase(LeanTweenType.easeInOutQuad));
    // }



}
