using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnableWelcome : MonoBehaviour
{
    private Text enableText;

    private GameObject TextCanvas;
    // Start is called before the first frame update
    void Start()
    {
        TextCanvas = GameObject.Find("WelcomeText");
        enableText = TextCanvas.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Obj)
    {
        if (Obj.CompareTag("Player"))
        {
            enableText.enabled = true;
        }
    }

    void OnTriggerExit(Collider Obj)
    {
        if (Obj.CompareTag("Player"))
        {
            enableText.enabled = false;
        }
    }
}
