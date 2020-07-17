﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : Singleton<GameController>

{
    public AudioController audioController;
    public GameObject locationStart;
    public GameObject locationCab;
    public Transform rigLocation;

    private bool engine = false;
    
    public void GotoCab()
    {
        rigLocation.position=new Vector3(
            locationCab.transform.position.x,
            locationCab.transform.position.y,
            locationCab.transform.position.z);
        audioController.AtCab();
    }

    public void GotoStart()
    {
        rigLocation.position=new Vector3(
            locationStart.transform.position.x,
            locationStart.transform.position.y,
            locationStart.transform.position.z);
        audioController.AtStart();
    }

    public void Engine()
    {
        if (engine == false)
        {
            audioController.CraneStart();
            engine = true;
        }
        else
        {
            // TODO crane shutdown sound, set variable, pause, then bring back to start
            GotoStart();
        }
    }
}

