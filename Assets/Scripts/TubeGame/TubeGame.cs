using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeGame : MonoBehaviour
{
    public bool InTime;
    public int machinesActivated;

    public Canvas IntroductionCanvas;
    public Text IntroductionText;
    public TubeTimerScript TubeTimerScript;
    public LogScript logScript;

    // Start is called before the first frame update
    void Start()
    {
        machinesActivated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogFinalScore()
    {
       
        if (TubeTimerScript.startTime > 0)
        {
            IntroductionText.text = "You completed the game with = " + Mathf.Round(TubeTimerScript.startTime) + " seconds left!";
        }
        else
        {
            IntroductionText.text = "You did not complete the game within the time";
        }
        logScript.AddToList("Tube game result: " + IntroductionText.text);
    }

}
