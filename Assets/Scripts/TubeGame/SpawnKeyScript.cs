using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Optional script if more keys are needed
public class SpawnKeyScript : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadzone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;

    public GameObject Key;

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
        Transform beginpos = Key.GetComponent<Transform>();
        Vector3 spawnpos = new Vector3(beginpos.position.x, beginpos.position.y + 0.8f, beginpos.position.z);
        Instantiate(Key, spawnpos, beginpos.rotation);
    }
}
