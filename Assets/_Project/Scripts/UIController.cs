﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : Singleton<UIController>
{
    public Transform lever1;
    public Transform lever2;
    public Transform lever3;
    public Transform lever4;
    public Transform dropButton;

    public GameObject onOffButtonPanel;
    public GameObject lever1Panel;
    public GameObject lever2Panel;
    public GameObject lever3Panel;
    public GameObject lever4Panel;
    public GameObject endPracticeSectionPanel;
    public GameObject leverMessage;

    //public Text timerText;
    //public Text scoreText;

    public GameObject cargo;
    public GameObject targetPlinth;

    private string mode;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject checkmark1;
    public GameObject checkmark2;
    public GameObject scoreArea;

    public GameObject cab;
    public GameObject boomA;
    public GameObject boomB;
    public GameObject hook;

    private Outline outlineForCargo;
    private Outline outlineForTarget;
    private Vector3 cargoOriginalPosition;

    private void Start()
    {
        cargoOriginalPosition = cargo.transform.position;
    }
    private void Update()
    {
        if (mode == "FirstStep" && GameController.Instance.engineRunning == true)
        {
            // display the checkmark for engine on
            checkmark1.SetActive(true);
        }

        if (mode == "SecondStep" && TargetTrigger1.Instance.detected == true)
        {
            // display checkmark for rotating crane right
            checkmark1.SetActive(true);
        }

        if (mode == "SecondStep" && TargetTrigger2.Instance.detected == true)
        {
            // display checkmark for rotating crane left
            checkmark2.SetActive(true);
        }

        if (mode == "ThirdStep" && TargetTrigger3.Instance.detected == true)
        {
            // display checkmark for raising boom up 
            checkmark1.SetActive(true);
        }

        if (mode == "FourthStep" && TargetTrigger4.Instance.detected == true)
        {
            // display checkmark for extending boom
            checkmark1.SetActive(true);
        }

        if (mode == "FifthStep" && TargetTrigger5.Instance.detected == true)
        {
            // display checkmark for lowering hook
            checkmark1.SetActive(true);
        }


        // Highlight cargo and target subsequently
        //if (mode == "training" && HookPickUp.Instance.cargoDetected == false)
        //{
        //    if (cargo.GetComponent<Outline>() == false)
        //    {
        //        outlineForCargo = cargo.AddComponent<Outline>();

        //        outlineForCargo.OutlineMode = Outline.Mode.OutlineAll;
        //        outlineForCargo.OutlineColor = Color.yellow;
        //        outlineForCargo.OutlineWidth = 5f;
        //    }
        //    else
        //    {
        //        var outline = cargo.GetComponent<Outline>();
        //        outline.enabled = true;
        //    }
        //}
        //else if (mode == "training" && HookPickUp.Instance.cargoDetected == true)
        //{
        //    var outline = cargo.GetComponent<Outline>();
        //    outline.enabled = false; // un-higlight cargo

        //    if (targetPlinth.GetComponent<Outline>() == false)
        //    {
        //        outlineForTarget = targetPlinth.AddComponent<Outline>();

        //        outlineForTarget.OutlineMode = Outline.Mode.OutlineAll;
        //        outlineForTarget.OutlineColor = Color.yellow;
        //        outlineForTarget.OutlineWidth = 5f;
        //    }
        //    else
        //    {
        //        var outlineTarget = targetPlinth.GetComponent<Outline>();
        //        outlineTarget.enabled = true;
        //    }
        //}
        if (GameController.Instance.score == 0 && GameController.Instance.engineRunning == true)
        {
            if (targetPlinth.GetComponent<Outline>() == false)
            {
                outlineForTarget = targetPlinth.AddComponent<Outline>();

                outlineForTarget.OutlineMode = Outline.Mode.OutlineVisible;
                outlineForTarget.OutlineColor = Color.green;
                outlineForTarget.OutlineWidth = 5f;
            }
            else
            {
                var outlineTarget = targetPlinth.GetComponent<Outline>();
                outlineTarget.enabled = true;
            }

            if (cargo.GetComponent<Outline>() == false)
            {
                outlineForCargo = cargo.AddComponent<Outline>();

                outlineForCargo.OutlineMode = Outline.Mode.OutlineVisible;
                outlineForCargo.OutlineColor = Color.yellow;
                outlineForCargo.OutlineWidth = 5f;
            }
            else
            {
                var outline = cargo.GetComponent<Outline>();
                outline.enabled = true;
            }
        }
        else if (GameController.Instance.score >= 1)
        {
            var outlineTarget = targetPlinth.GetComponent<Outline>();
            outlineTarget.enabled = false; // un-highlight if user scores

            var outlineCargo = cargo.GetComponent<Outline>();
            outlineCargo.enabled = false; // un-highlight if user scores
        }

        // Setting up target for tutorial
        //target3.transform.position = new Vector3(HookController.Instance.worldPosition.x, 6f, HookController.Instance.worldPosition.z-0.5f);
        //target4.transform.position = new Vector3(HookController.Instance.worldPosition.x, HookController.Instance.worldPosition.y - 0.5f, HookController.Instance.worldPosition.z+2);
        target5.transform.position = new Vector3(HookController.Instance.worldPosition.x, 1, HookController.Instance.worldPosition.z);
    }

    public void TutorialMode()
    {
        //MyRoutine(); // Reload Scene to make sure the orientation of the crane is correct
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cab.transform.localEulerAngles = CabRotation.Instance.originalRotation;
        cab.transform.localPosition = CabRotation.Instance.originalPosition;
        boomA.transform.localEulerAngles = BoomRaise.Instance.originalRotation;
        boomA.transform.localPosition = BoomRaise.Instance.originalPosition;
        boomB.transform.localPosition = ExtendBoom.Instance.originalPosition;
        boomB.transform.localEulerAngles = ExtendBoom.Instance.originalRotation;
        hook.transform.localPosition = HookController.Instance.originalPosition;
        HookController.Instance.springJoint.maxDistance = 0;

        mode = "FirstStep";

        //timerText.gameObject.SetActive(false);
        //scoreText.gameObject.SetActive(false);
        scoreArea.gameObject.SetActive(false);
        /*
        TimerController.Instance.timerText.gameObject.SetActive(false);
        GameController.Instance.scoreText.gameObject.SetActive(false);
        */

        cargo.SetActive(false);
        targetPlinth.SetActive(false);

        ChangeLayersRecursively(lever1, "Practice Mode"); // lever1 is not interactive
        ChangeLayersRecursively(lever2, "Practice Mode");
        ChangeLayersRecursively(lever3, "Practice Mode");
        ChangeLayersRecursively(lever4, "Practice Mode");
        ChangeLayersRecursively(dropButton, "Practice Mode");

        // Teleport user into the cab
        GameController.Instance.GotoCab();

        // UI to tell user to click on/off button
        onOffButtonPanel.SetActive(true);
        lever1Panel.SetActive(false);
        lever2Panel.SetActive(false);
        lever3Panel.SetActive(false);
        lever4Panel.SetActive(false);
        endPracticeSectionPanel.SetActive(false);
        leverMessage.SetActive(false);
        checkmark1.SetActive(false);
        checkmark2.SetActive(false);
        target1.SetActive(false);
        target2.SetActive(false);
        target3.SetActive(false);
        target4.SetActive(false);
        target5.SetActive(false);
    }

    public void MoveToFirstLever()
    {
        if (GameController.Instance.engineRunning) // Once the On/Off button is pressed
        {
            mode = "SecondStep";
            checkmark1.SetActive(false);
            checkmark2.SetActive(false);

            target1.SetActive(true);
            target2.SetActive(true);

            ChangeLayersRecursively(lever1, "Grab Ignore Ray"); // first lever active
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");

            // UI instruction for lever 1 appears
            onOffButtonPanel.SetActive(false);
            lever1Panel.SetActive(true);
        }
    }

    public void MoveToSecondLever()
    {
        if (FirstLeverController.Instance.leverOneActive == false && ((TargetTrigger1.Instance.detected == true) && (TargetTrigger2.Instance.detected == true)))
        {
            mode = "ThirdStep";
            checkmark1.SetActive(false);
            checkmark2.SetActive(false);

            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(true);

            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Grab Ignore Ray"); // lever2 is active
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");

            // TODO turn off UI for lever1 and turn on for UI lever2
            lever1Panel.SetActive(false);
            leverMessage.SetActive(false);
            lever2Panel.SetActive(true);

        }
        else if (FirstLeverController.Instance.leverOneActive == true && ((TargetTrigger1.Instance.detected == true) && (TargetTrigger2.Instance.detected == true)))
        {
            leverMessage.gameObject.SetActive(true);
        }

    }

    public void MoveToThirdLever()
    {
        if (SecondLeverController.Instance.leverTwoActive == false && (TargetTrigger3.Instance.detected == true))
        {
            mode = "FourthStep";
            checkmark1.SetActive(false);
            checkmark2.SetActive(false);

            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(false);
            target4.SetActive(true);

            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Grab Ignore Ray"); // lever3 is active
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");
            // TODO turn off UI for lever2 and turn on for UI lever3
            lever2Panel.SetActive(false);
            leverMessage.SetActive(false);
            lever3Panel.SetActive(true);
        }
        else if (SecondLeverController.Instance.leverTwoActive == true && (TargetTrigger3.Instance.detected == true))
        {
            leverMessage.SetActive(true);
        }

    }

    public void MoveToFourthLever()
    {
        if ((ThirdLeverController.Instance.leverThreeActive == false) && (TargetTrigger4.Instance.detected == true))
        {
            mode = "FifthStep";
            checkmark1.SetActive(false);
            checkmark2.SetActive(false);

            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(false);
            target4.SetActive(false);
            target5.SetActive(true);

            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Grab Ignore Ray"); // lever4 is active
            ChangeLayersRecursively(dropButton, "Practice Mode");

            // TODO turn off UI for lever3 and turn on for UI lever4
            lever3Panel.SetActive(false);
            leverMessage.SetActive(false);
            lever4Panel.SetActive(true);
        }
        else if (ThirdLeverController.Instance.leverThreeActive == true && (TargetTrigger4.Instance.detected == true))
        {
            leverMessage.SetActive(true);
        }

    }

    //public void MoveToButton()
    //{
    //    ChangeLayersRecursively(lever1, "Practice Mode");
    //    ChangeLayersRecursively(lever2, "Practice Mode");
    //    ChangeLayersRecursively(lever3, "Practice Mode");
    //    ChangeLayersRecursively(lever4, "Practice Mode"); 
    //    ChangeLayersRecursively(dropButton, "Grab Ignore Ray"); // 2 buttons are active
    //    // TODO turn off UI for lever4 and turn on for UI buttons
    //    lever4Canvas.gameObject.SetActive(false);
    //    dropButtonCanvas.gameObject.SetActive(true);
    //}

    public void EndPracticeSection()
    {
        if ((FourthLeverController.Instance.leverFourActive == false) && (TargetTrigger5.Instance.detected == true))
        {
            mode = "SixthStep";

            checkmark1.SetActive(false);
            checkmark2.SetActive(false);

            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(false);
            target4.SetActive(false);
            target5.SetActive(false);

            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");

            lever4Panel.SetActive(false);
            leverMessage.SetActive(false);
            endPracticeSectionPanel.SetActive(true);
        }
        else if (FourthLeverController.Instance.leverFourActive == true && (TargetTrigger5.Instance.detected == true))
        {
            leverMessage.SetActive(true);
        }

    }

    public IEnumerator MyRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(3f);
    }

    public void PracticeMode()
    {
        cab.transform.localEulerAngles = CabRotation.Instance.originalRotation;
        cab.transform.localPosition = CabRotation.Instance.originalPosition;
        boomA.transform.localEulerAngles = BoomRaise.Instance.originalRotation;
        boomA.transform.localPosition = BoomRaise.Instance.originalPosition;
        boomB.transform.localPosition = ExtendBoom.Instance.originalPosition;
        boomB.transform.localEulerAngles = ExtendBoom.Instance.originalRotation;
        hook.transform.localPosition = HookController.Instance.originalPosition;
        HookController.Instance.springJoint.maxDistance = 0;
        //MyRoutine();

        mode = "training";
        //timerText.gameObject.SetActive(true);
        //scoreText.gameObject.SetActive(true);
        /*
        TimerController.Instance.timerText.gameObject.SetActive(true);
        GameController.Instance.scoreText.gameObject.SetActive(true);
        */
        scoreArea.gameObject.SetActive(true);

        cargo.SetActive(true);
        cargo.transform.position = cargoOriginalPosition;
        targetPlinth.SetActive(true);

        ChangeLayersRecursively(lever1, "Grab Ignore Ray");
        ChangeLayersRecursively(lever2, "Grab Ignore Ray");
        ChangeLayersRecursively(lever3, "Grab Ignore Ray");
        ChangeLayersRecursively(lever4, "Grab Ignore Ray");
        ChangeLayersRecursively(dropButton, "Grab Ignore Ray");

        GameController.Instance.GotoCab();
        //Debug.Log("Running");

        onOffButtonPanel.SetActive(false);
        lever1Panel.SetActive(false);
        lever2Panel.SetActive(false);
        lever3Panel.SetActive(false);
        lever4Panel.SetActive(false);
        endPracticeSectionPanel.SetActive(false);
        leverMessage.SetActive(false);
        checkmark1.SetActive(false);
        checkmark2.SetActive(false);
        target1.SetActive(false);
        target2.SetActive(false);
        target3.SetActive(false);
        target4.SetActive(false);
        target5.SetActive(false);
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            child.gameObject.layer = LayerMask.NameToLayer(name);
            ChangeLayersRecursively(child, name);
        }
    }

    public void DisableLevers()
    {
        ChangeLayersRecursively(lever1, "Practice Mode"); // lever1 is not interactive
        ChangeLayersRecursively(lever2, "Practice Mode");
        ChangeLayersRecursively(lever3, "Practice Mode");
        ChangeLayersRecursively(lever4, "Practice Mode");
        ChangeLayersRecursively(dropButton, "Practice Mode");
    }

    public void ActivateLevers()
    {
        ChangeLayersRecursively(lever1, "Grab Ignore Ray");
        ChangeLayersRecursively(lever2, "Grab Ignore Ray");
        ChangeLayersRecursively(lever3, "Grab Ignore Ray");
        ChangeLayersRecursively(lever4, "Grab Ignore Ray");
        ChangeLayersRecursively(dropButton, "Grab Ignore Ray");
    }
}
