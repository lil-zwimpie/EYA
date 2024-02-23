using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeTimerScript : MonoBehaviour
{
    public Text TimerText;
    public float startTime;
    public bool finished = false;
    public AudioSource emergencySoundSource;
    public TubeGame TubeGame;

    private bool playingSound = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If timer reachead 0, display the final score on the watch
        if (!TubeGame.InTime && finished)
        {
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
            //When time is up this part can be used for whatever
        }
        else
        {
            // With every tick update the time on the watch.
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            if (timeToDisplay < 12) // Dangerzone of the time
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
        TubeGame.InTime = false;
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

    public string GetTime()
    {
        if (finished && startTime > 0)
        {
            Mathf.Round(startTime);
            return startTime.ToString();
        }
        return null;
    }
}
