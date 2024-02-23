using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace Autohand
{
    public class SliderLeverScript : MonoBehaviour
    {
        public Transform Movement;
        public ConfigurableJoint Lever;
        public Rigidbody RB;
        public UnityEvent Call;
        public UnityEvent UndoCall;
        //Number should be the same as the Linear limit on the Joint
        public float limit = 0.1f;

        private Transform beginPos;
        private Transform CurrentPos;
        private Vector3 newAnchor;

        private float beginPosZ;
        private float currentPosZ;
        private float beginPosY;
        private float currentPosY;
        private float currentPosX;
        private float beginPosX;
        private bool activateY;
        private bool isUp;
       

        // Start is called before the first frame update
        void Start()
        {
            // Defining the begin position and the limits
            beginPos = GetComponent<Transform>();
            beginPosZ = beginPos.localPosition.z;
            beginPosX = beginPos.localPosition.x;
            beginPosY = beginPos.localPosition.y;
            limit = 0.1f;

            activateY = false;
            isUp = false;
        }

        // Update is called once per frame
        void Update()
        {
            CurrentPos = GetComponent<Transform>();
            currentPosZ = CurrentPos.localPosition.z;
            currentPosX = CurrentPos.localPosition.x;
            
            Vector3 newPos = Movement.localPosition; 

            //Place the handle at the beginning spot
            if(currentPosX != beginPosX)
            {
                newPos.x = beginPosX;
                Movement.localPosition = newPos;
            }

            //When the current position reached the limit activate Y, place new anchor
            if (currentPosZ - beginPosZ >= limit && activateY == false)
            {
                activateY = true;
                newAnchor = Lever.connectedAnchor;
                Lever.anchor = newAnchor;
            }

            //When the Y activates unlock the Y and lock Z
            if (currentPosZ - beginPosZ >= limit && activateY == true)
            {
                Lever.zMotion = ConfigurableJointMotion.Locked;
                Lever.yMotion = ConfigurableJointMotion.Limited;
                
                limit = 0.08f;

                RB.useGravity = true;

            }

            currentPosY = CurrentPos.localPosition.y;

            //When the handle reached the Y limit invoke call
            if (currentPosY - beginPosY >= limit)
            {
                isUp = true;

                Call.Invoke();
            }

            //When the handle is back down undo the invoke
            if (isUp == true && currentPosY - beginPosY <= limit)
            {
                isUp = false;
                UndoCall.Invoke();
            }
        }
    }
}
