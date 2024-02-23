using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogScript : MonoBehaviour
{
    public List<string> Logs = new List<string>();
    public float StartTime;

    private DateTime timeObjectGrabbed;
    private DateTime timeObjectReleased;
    private string localPath;
    private string logPath;
    private Scene scene;


    //Start is called before the first frame update
    void Start()
    {
        StartTime = Time.realtimeSinceStartup;
        scene = SceneManager.GetActiveScene();
        AddToList(scene.name + " was started at " + DateTime.Now);
    }

    
    // Update is called once per frame
    void Update()
    {
    
    }

    public void AddToList(string data)
    {
        Logs.Add(data);
        WriteToLogFile();
    }

    public void ObjectGrabbed()
    {
        timeObjectGrabbed = DateTime.Now;
    }

    public void ObjectReleased(GameObject obj)
    {
        timeObjectReleased = DateTime.Now;
        AddToList(obj.name + " grabbed at: " + timeObjectGrabbed + " and released at: " + timeObjectReleased);
    }

    public void WriteToLogFile()
    {
        //log for apk
        localPath = Application.persistentDataPath;

        logPath = localPath + "/" + scene.name + ".txt";
      
        using (System.IO.StreamWriter logFile = new System.IO.StreamWriter(logPath))
        {
            foreach (var message in Logs)
            {
                logFile.Write(message + "\n");
            }
        }
       
    }
}
