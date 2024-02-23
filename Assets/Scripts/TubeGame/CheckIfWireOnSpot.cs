using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckIfWireOnSpot : MonoBehaviour
{
    public Animator StartMachineAnimation;
    public AudioSource StartEngineSound;
    public AudioSource MachineRunning;
    public TubeGame TubeGame;
    public LogScript logScript;
    public Light ActiveLight;
    public Material LightOnMaterial;
    public GameObject DoorLightBulb;

    public Rigidbody NextPipe;
    public ActivateSpawnpad ActivateSpawnpad;

    public UnityEvent Call;


    // Start is called before the first frame update
    void Start()
    {
        StartMachineAnimation.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When a objects hits the collider
    void OnTriggerEnter(Collider Obj)
    {
        //If an object with the tag key entered the collider AND the machine is not started yet
        if (Obj.CompareTag("Key") && StartMachineAnimation.enabled != true)
        {
            ActivateMachine();
            logScript.AddToList("Machine " + TubeGame.machinesActivated + " is activated!");
            if (TubeGame.machinesActivated >= 3)
            {
                TubeGame.LogFinalScore();
                TubeGame.IntroductionCanvas.enabled = true;
                TubeGame.TubeTimerScript.StopTimer();
                ActivateSpawnpad.ActivatePad();
            }
        }
    }

    void ActivateMachine()
    {
        //Start the machine and play the sound
        StartMachineAnimation.enabled = true;
        StartEngineSound.Play();
        TubeGame.machinesActivated++;
        ActivateLight();
        //Enable the next machine to be activated
        if (NextPipe != null && NextPipe.isKinematic == true)
        {
            NextPipe.isKinematic = false;
        }
        StartCoroutine(Coroutine());
    }

    void ActivateLight()
    {
        ActiveLight.enabled = true;
        Material[] materials = DoorLightBulb.GetComponent<Renderer>().materials;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = LightOnMaterial;
        }

        DoorLightBulb.GetComponent<Renderer>().materials = materials;
    }


    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(5f);
        StartEngineSound.Stop();
        MachineRunning.Play();
        Call.Invoke();
    }

}
