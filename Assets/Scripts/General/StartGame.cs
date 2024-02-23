using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject Light01;
    public GameObject LightBulb01;
    public GameObject Light02;
    public GameObject LightBulb02;
    public GameObject Light03;
    public GameObject LightBulb03;
    public GameObject Light04;
    public GameObject LightBulb04;

    public AudioSource LightsOff;
    public AudioSource Unlock;

    public Material LampOff;
    public UnityEvent PostActivate;
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
        StartCoroutine(TurnLightOff(Light01, LightBulb01, 2));
        StartCoroutine(TurnLightOff(Light02, LightBulb02, 4));
        StartCoroutine(TurnLightOff(Light03, LightBulb03, 6));
        StartCoroutine(TurnLightOff(Light04, LightBulb04, 8));
        StartCoroutine(UnlockDoor());
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        
    }

    
    IEnumerator TurnLightOff(GameObject light, GameObject lightbulb, int waitTime)
    {
        //Wait for a few seconds before turning the lights off and changing materials
        yield return new WaitForSeconds(waitTime);
        light.GetComponent<Light>().enabled = false;
        
        Material[] materials1 = lightbulb.GetComponent<Renderer>().materials;
        for (int i = 0; i < materials1.Length; i++)
        {
            materials1[i] = LampOff;
        }
        lightbulb.GetComponent<Renderer>().materials = materials1;
        LightsOff.Play();

    }

    IEnumerator UnlockDoor()
    {
        //When all the lights are off, play the unlocked door sound
        yield return new WaitForSeconds(10);
        Unlock.Play();
        PostActivate.Invoke();
    }
}
