using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewScene()
    {
        //Based on the tag of the button load the correct scene
        if (gameObject.tag != null)
        {
            SceneManager.LoadScene(tag);
        }
    }
}
