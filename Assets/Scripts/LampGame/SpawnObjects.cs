using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjects : MonoBehaviour
{
    public GameObject SpawnArea;
    public GameObject TheRightTarget;
    public LogScript LogScript;

    private bool firstObject = true;
    private List<Vector3> savePositions;

    /* If some false objects are needed to draw the attention the #optional parts could be used as false objects.
    //public GameObject TheWrongTarget;
    //private int amountOfObjects;
    */

    // Start is called before the first frame update
    void Start()
    {
        //#optional amountOfObjects = 5; // These are the amount objects that are spawned. (if 5 then 1 right and 4 wrong)
        savePositions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObjects()
    {
        destroyObjects();
        MeshCollider c = SpawnArea.GetComponent<MeshCollider>();
        float areaX, areaY, areaZ;
        Vector3 pos;

        //#optional Spawn the amount of objects
        /*for (int i = 0; i < amountOfObjects; i++)
        {*/
            areaX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            areaY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            areaZ = Random.Range(c.bounds.min.z - 0.4f, c.bounds.max.z - 0.4f);
            // Calculate a random position for the objects to spawn
            pos = new Vector3(areaX, areaY, areaZ);
            if (firstObject)
            {
                // Always spawn 1 good object
                Instantiate(TheRightTarget, pos, TheRightTarget.transform.rotation);
                // Log the center of this object
                LogScript.AddToList(pos.ToString());
                savePositions.Add(pos);
                firstObject = false;
            }/*
            else
            {   #optional
                //Make sure the objects don't overlap
                bool hitsOtherObject = Physics.CheckBox(pos, TheWrongTarget.transform.localScale / 2);
                if (savePositions.Contains(pos) || hitsOtherObject)
                {
                    i--;
                }
                else
                {
                    Instantiate(TheWrongTarget, pos, TheWrongTarget.transform.rotation);

                    savePositions[i] = pos;
                }
            }
            
        }*/
    }

    public void destroyObjects()
    {
        // Clear all objects
        savePositions.Clear();
        firstObject = true;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            Destroy(o);
        }
    }
}
