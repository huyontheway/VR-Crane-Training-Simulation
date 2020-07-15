﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    public bool leverTwoActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);
        //Debug.Log(BoomRaise.Instance.rotation.x);
        

        if ((leverRotation.x <= 10f && leverRotation.x >= 0f) || (leverRotation.x >= 350f && leverRotation.x <= 360f))
        {
            BoomRaise.Instance.BoomStationary();
            leverTwoActive = false;
        }

        else if (leverRotation.x > 10f && leverRotation.x <= 61f)
        {
            if (BoomRaise.Instance.rotation.x >= 310f && BoomRaise.Instance.rotation.x <= 359.5f)
            {
                BoomRaise.Instance.LowerBoom(15f);
                leverTwoActive = true;
            }
            else
            {
                BoomRaise.Instance.BoomStationary();
                Debug.Log("No force 1");
            }            
        }
        else if (leverRotation.x < 350f && leverRotation.x >= 299f)
        {
            if (BoomRaise.Instance.rotation.x >= -0.5f && BoomRaise.Instance.rotation.x <= 0.5f)
            {
                BoomRaise.Instance.RaiseBoom(15f);
                leverTwoActive = true;
            }
            else if(BoomRaise.Instance.rotation.x > 311f)
            {
                BoomRaise.Instance.RaiseBoom(15f);
                leverTwoActive = true;
            }
            else
            {
                BoomRaise.Instance.BoomStationary();
                Debug.Log("No force 3");
            }
            
        }
    }
}
