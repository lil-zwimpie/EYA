using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandleLamp : MonoBehaviour
{
    public float ErrorCounter;
    public Text ErrorCounterText;
    public UnityEvent CallGood;
    public UnityEvent CallBad;
    public UnityEvent LockUp;

    private float internCount;
    private bool delayActive;
    private bool completed;

    // Start is called before the first frame update
    void Start()
    {
        ErrorCounter = 0;
        delayActive = false;
        completed = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Delay of 1 second before counter and sound will activate again
        if (internCount >= 1)
        {
            internCount = 0;
            delayActive = false;
        }
        else
        {
            internCount += Time.deltaTime;
        }

        // Bit shift the index of the layer (11) to get a bit mask
        int layerMask = 1 << 11;
        
        //Layer of stuff not to hit
        int badLayer = 1 << 16;

        // This would cast rays only against colliders in layer 11.
        RaycastHit hit;

        // Does the ray of the light hit the right layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity, layerMask))
        {
            if (!delayActive && completed == false)
            {
                CallGood.Invoke();
                delayActive = true;
                completed = true;
            }
            

        }

        //When the ray of the light hits the bad layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity, badLayer))
        {
            if (!delayActive)
            {
                if (completed == true)
                {
                    LockUp.Invoke();
                }
                CallBad.Invoke();

                ErrorCounter++;
                ErrorCounterText.text = ErrorCounter.ToString();
                delayActive = true;
                completed = false;
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

    }

}
