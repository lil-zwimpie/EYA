using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class radioControll : MonoBehaviour
{
    public void MuteHandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
        }
        else 
        {
            AudioListener.volume = 0.2f;
        }
    }
}
