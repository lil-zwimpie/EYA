using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorCounterText : MonoBehaviour
{
    public GameObject Counter;
    public float Count;
    public Text CountText;


    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
         DisplayCount(Count);
    }
 
   
    void DisplayCount(float timeToDisplay)
    {
                
            CountText.text = string.Format("0");
        
    }
     
    
}
