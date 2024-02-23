using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class KeyStartGame : MonoBehaviour
{
    public Rigidbody DisableHatchKinematic;
    public TubeTimerScript TubeTimerScript;
    public TubeGame TubeGame;
    public Canvas TimeCanvas;
    public LogScript LogScript;

    private bool firstGrab = true;

    // Start is called before the first frame update
    void Start()
    {
        TimeCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void CheckForFirstGrab()
    {
        //When the key is firstly grabbed activate the game
        if (firstGrab)
        {
            DisableHatchKinematic.isKinematic = false;
            TubeTimerScript.startTime = 300;
            TubeTimerScript.finished = false;
            TubeTimerScript.TimerText.color = Color.yellow;
            LogScript.AddToList("Game was started at " + DateTime.Now);
            TubeGame.InTime = true;
            TimeCanvas.enabled = true;
            firstGrab = false;
        }
    }

}
