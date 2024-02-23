using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autohand;
using UnityEngine;
using UnityEngine.UI;

public class ShineOnObjects : MonoBehaviour
{
    public bool InTime;
    public bool logged;
    public bool GameStarted;
    public string FinalScore;
    public int ObjectsLeft;
    public int totalHit;
    public float aimTime;
    public Text scoreText;
    public AudioSource playSound;
    public Transform Lamp;
    public Transform LampHolder;
    public Grabbable GrabLamp;
    public GameObject DoorLightBulb;
    public GameObject DuracellBattery;
    public Material HitMaterial;
    public Material RedBattery;
    public Material LightOnMaterial;
    public LogScript LogScript;
    public ActivateSpawnpad Activate;
    public LampTimerScript lampTimerScript;
    public UIBarScript uiBarScript;
    public SpawnObjects spawnScript;
    public Light BigLight;
    public Light DoorLight;
    
    private bool aimComplete = false;
    private List<string> scores;
    private Light light;



    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        scores = new List<string>();

        GrabLamp.enabled = false;
        logged = false;
        InTime = true;

        string beginPos = "Vertical rotation: " + Lamp.transform.rotation.z +
                          " - Horizontal rotation: " + LampHolder.transform.rotation.y;
        LogScript.AddToList("Begin position \n" + beginPos);
    }

    // Update is called once per frame
   void FixedUpdate()
   {
       UpdateScoreBoard();

       if (GameStarted)
       {

           if (InTime && ObjectsLeft > 0)
           {
               StartGame();

               // Bit shift the index of the layer (11) to get a bit mask
               int layerMask = 1 << 11;

               // This would cast rays only against colliders in layer 11.
               RaycastHit hit;

               // Does the ray intersect any objects excluding the player layer
               if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                   Mathf.Infinity, layerMask))
               {

                   CheckAimTime();
                   AimComplete(hit);
               }
               else
               {
                   uiBarScript.enableAnimation = false;
                   Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
               }
           }
           else
           {
               GameDone();

               if (!logged)
               {
                   LogFinalSCore();
                   totalHit = 0;
               }

           }
       }
   }

   void StartGame()
   {
       GrabLamp.enabled = true;

       BigLight.enabled = true;
       light.enabled = true;
   }

   void LogFinalSCore()
   {
        //Logging the score
        if (lampTimerScript.startTime > 0)
        {
            scores.Add("Completed: " + Mathf.Round(lampTimerScript.GetTime()) + " seconds left\n");
            FinalScore = "You comepleted the game with = " + lampTimerScript.startTime + " seconds left!";
        }
        else
        {
            scores.Add("Failed: You hit " + totalHit + " out of 5 targets\n");
            FinalScore = "You did not complete the game";
        }
        LogScript.AddToList(FinalScore);
        logged = true;
    }

   void GameDone()
   {
       // Timer is expired, turn lights off, stop the sounds
       // And delete the objects spawned
       lampTimerScript.StopTimer();
       GrabLamp.enabled = false;
       spawnScript.destroyObjects();
       BigLight.enabled = false;
       light.enabled = false;
       uiBarScript.enableAnimation = false;
       if (ObjectsLeft <= 0)
       {
           if (!DoorLight.enabled)
           {
               Material[] materials = DoorLightBulb.GetComponent<Renderer>().materials;
               for (int i = 0; i < materials.Length ;i++)
               {
                   materials[i] = LightOnMaterial;
               }

               DoorLightBulb.GetComponent<Renderer>().materials = materials;
               DoorLight.enabled = true;
           } 
           Activate.ActivatePad();
       }
   }

   //If the aim is complete log everything and respawn the object
   void AimComplete(RaycastHit hit)
   {
       if (aimComplete)
       {
           LogScript.AddToList("The center where the light hit the object is: " + hit.point.ToString());
           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
               Color.yellow);
           string currentRotation = "Vertical rotation: " + Lamp.transform.rotation.z +
                                    " - Horizontal rotation: " + LampHolder.transform.rotation.y;
           playSound.Play();
           LogScript.AddToList("Current position \n" + currentRotation);
           spawnScript.spawnObjects();
           aimComplete = false;
           totalHit++;
           ObjectsLeft--;
           //Fill the battery
           for (int i = 0; i < totalHit; i++)
           {
               GameObject Batteries = DuracellBattery.gameObject.transform.GetChild(i).gameObject;
               Batteries.GetComponent<Renderer>().material = HitMaterial;
           }
       }
    }

   void CheckAimTime()
   {
       //bool canplay = true;
       if (aimTime < 3)
       {
           aimTime += Time.deltaTime;
           uiBarScript.enableAnimation = true;
       }
       else
       {
           //Reset the animation and time
           uiBarScript.enableAnimation = false;
           aimTime = 0;
           aimComplete = true;
           uiBarScript.animator.Play("Take 001", -1, 0);
       }
    }

   void UpdateScoreBoard()
   {
       if (scores.Count <= 0)
       {
           scoreText.text = "No scores yet";
       }
       else
       {
           scoreText.text = "";
           //Show scoreboard with scores
           List<string> orderedList = scores.OrderBy(o => o).ToList();
           for (int i = 0; i < orderedList.Count; i++)
           {
               scoreText.text += (i + 1) + ". " + orderedList[i];
           }
        }
    }

   public void EmptyBatteryLamps()
    {
        for (int i = 0; i < DuracellBattery.transform.childCount; i++)
        {
            GameObject Batteries = DuracellBattery.gameObject.transform.GetChild(i).gameObject;
            Batteries.GetComponent<Renderer>().material = RedBattery;
        }
    }
}
