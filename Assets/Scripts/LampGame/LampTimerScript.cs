using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampTimerScript : MonoBehaviour
{
    public float startTime;
    public bool finished;
    public Text TimerText;
    public AudioSource emergencySoundSource;
    public ShineOnObjects shineScript;


    private AudioSource ClockTicking;
    private bool playingSound = false;
    private bool clockTicking = false;


    // Start is called before the first frame update
    void Start()
    {
        ClockTicking = GetComponent<AudioSource>();
        finished = true;
        shineScript = GameObject.Find("Light").GetComponent<ShineOnObjects>();
    }

    // Update is called once per frame
    void Update()
    {
        // If timer reachead 0, display the final score on the watch
        if (!shineScript.InTime)
        { 
            ClockTicking.Stop();
            clockTicking = false;
            UpdateText();
        }

        if (!finished)
        {
            UpdateTime();
            DisplayTime(startTime);
        }
    }

    void UpdateTime()
    {
        if (startTime > 0)
        {
            if (!clockTicking)
            {
                ClockTicking.Play();
                clockTicking = true;
            }
         
            startTime -= Time.deltaTime;
        }
        else
        {
            startTime = 0;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            shineScript.InTime = false;
        }
        else
        {
            // With every tick update the time on the watch.
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            if (timeToDisplay < 8) // Dangerzone of the time
            {
                TimerText.color = Color.red;
                if (!playingSound)
                {
                    emergencySoundSource.Play();
                    playingSound = true;
                }
            }
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StopTimer()
    {
        // Stop the timer
        finished = true;
        shineScript.InTime = false;
    }

    void UpdateText()
    {
        if (startTime > 0)
        {
            TimerText.color = Color.green;
            TimerText.text = "DONE";
            return;
        }

        startTime = 0;
        TimerText.text = "FAIL";
        return;
    }

    public float GetTime()
    {
        if (finished && startTime > 0)
        {
            return startTime;
        }
        return -1;
    }
}
