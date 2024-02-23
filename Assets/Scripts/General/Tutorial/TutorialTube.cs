using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTube : MonoBehaviour
{
    public Animator StartMachineAnimation;
    public AudioSource StartEngineSound;
    public AudioSource MachineRunning;

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
        }
    }

    void ActivateMachine()
    {
        //Start the machine and play the sound
        StartMachineAnimation.enabled = true;
        StartEngineSound.Play();
        
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(5f);
        StartEngineSound.Stop();
        MachineRunning.Play();
    }
}
