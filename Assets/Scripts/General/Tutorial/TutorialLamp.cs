using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialLamp : MonoBehaviour
{
    public UnityEvent Call;
    public AudioSource HitSoundAudioSource;

    private bool isPlaying;
    private float timer;

    

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Bit shift the index of the layer (11) to get a bit mask
        int layerMask = 1 << 11;

        // This would cast rays only against colliders in layer 11.
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity, layerMask))
        {
            //If audio is not playing play it
            if (!HitSoundAudioSource.isPlaying)
            {
                HitSoundAudioSource.Play();
                isPlaying = true;
            }

            //If the audio is playing count to .8 before audio can be played again.
            if (HitSoundAudioSource.isPlaying)
            {
                if (timer > .8f)
                {
                    HitSoundAudioSource.Stop();
                    isPlaying = false;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }




}
