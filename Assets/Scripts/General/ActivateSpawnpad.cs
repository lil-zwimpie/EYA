using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class ActivateSpawnpad : MonoBehaviour
{
    public GameObject GreenSpawnPad;
    public GameObject DoorHandle;
    public AudioSource ActivatePadAudio;

    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePad()
    {
        if (!spawned)
        {
            Grabbable grabbale = DoorHandle.GetComponent<Grabbable>();
            grabbale.enabled = true;
            spawned = true;
            StartCoroutine(ChangePad());
        }
    }

    IEnumerator ChangePad()
    {
      
        Instantiate(GreenSpawnPad, transform.position, transform.rotation);
        ActivatePadAudio.Play();
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
