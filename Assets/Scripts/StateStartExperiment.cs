using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStartExperiment : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckAction;
    public StateInit StateInitScript;
    public ExperimentController ExperimentControllerScript;

    public bool ExperimentalTrialSuccesful;
    public bool ExperimentalTrialNOTSuccesful;


    public void Enter()
    {
        finished = false;
        nextState = CheckAction.GetComponent<IState>();
        //Debug.Log("Enter StateStartExperiment");
    }

    public void Execute()
    {
        //for test: Experiment was good
        if (Input.GetKey(KeyCode.O))
        {
            //Debug.Log("StartExperiment: TrialSuccessful: " + ExperimentalTrialSuccesful);
            ExperimentalTrialSuccesful = true;

        }
        if(Input.GetKey(KeyCode.P))
        {
            //Debug.Log("StartExperiment: TrialNOTSuccessful: " + ExperimentalTrialNOTSuccesful);
            ExperimentalTrialNOTSuccesful = true;
        }

        //Debug.Log("Execute StateStartExperiment");
        if (Input.GetKeyDown(KeyCode.F))
        {
            finished = true;
        }
        if (finished)
        {
            ExperimentControllerScript.trialOrderLineCounter++;
            Exit();
        }
    }

    public void Exit()
    {
        ExperimentControllerScript.ResetBools();
        //Debug.Log("Exit StateStartExperiment");
        //nextState.Enter();
    }



}
