using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RestartLampGame : MonoBehaviour
{
    public LampTimerScript LampTimerScript;
    public ShineOnObjects ShineOnObjectsScript;
    public UIBarScript uiBarScript;
    public SpawnObjects spawnScript;
    public Canvas IntroductionCanvas;
    public Canvas ScoreboardCanvas;

    public UnityEvent onPressed, onReleased;

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadzone = 0.025f;

   
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;


    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
            Pressed();
        if (_isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Math.Abs(value) < deadzone)
            value = 0;
        return Mathf.Clamp(value, -1f, 1f);
    }


    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        RestartGame();
    }

    void RestartGame()
    {
        //Restart the lampgame, make sure the data can be logged, score is reset and time is reset.
        LampTimerScript.startTime = 90;
        LampTimerScript.finished = false;
        LampTimerScript.TimerText.color = Color.yellow;
        spawnScript.spawnObjects();
        ShineOnObjectsScript.EmptyBatteryLamps();
        ShineOnObjectsScript.ObjectsLeft = 5;
        ShineOnObjectsScript.totalHit = 0;
        ShineOnObjectsScript.aimTime = 0;
        ShineOnObjectsScript.GameStarted = true;
        ShineOnObjectsScript.InTime = true;
        ShineOnObjectsScript.logged = false;
        IntroductionCanvas.enabled = false;
        ScoreboardCanvas.enabled = true;
        uiBarScript.animator.Play("Take 001", -1, 0);
    }

}
