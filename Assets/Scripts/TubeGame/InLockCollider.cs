using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class InLockCollider : MonoBehaviour
{
    public LogScript LogScript;

    private Collider thisColider;



    // Start is called before the first frame update
    void Start()
    {
        thisColider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When a objects hits the collider
    void OnTriggerEnter(Collider Obj)
    {
        if (Obj.CompareTag("Key"))
        {
            LogScript.AddToList(Obj.name + " went through " + thisColider.name + " in the following rotation " + Obj.transform.rotation.ToString("f3"));
        }
    }

}
