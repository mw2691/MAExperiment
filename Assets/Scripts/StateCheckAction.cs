using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckAction : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;

    public StateTraining stateTrainingScript;
    public ExperimentController experimentControllerScript;
    public StateStartExperiment stateStartExperimentScript;


    public GameObject PalmReference;
    public GameObject ObjectReference;
    public GameObject IndexFingerReference;
    public GameObject ThumbFingerReference;
    private Vector3 palmLastPosition;
    private Vector3 objectLastPosition;

    public bool ExperimentalTrialSuccesful;
    public bool ExperimentalTrialNOTSuccesful;

    private float timeStamp = 0.0f;
    public float CheckDuration = 10f;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateCheckAction");

        palmLastPosition = PalmReference.transform.position;
        objectLastPosition = ObjectReference.transform.position;
        Debug.Log("Uga timestamp");
    }

    public void Execute()
    {
        #region Check bottle grasp
        //Check for is bottle grasped?
        this.palmLastPosition = PalmReference.transform.position;
        this.objectLastPosition = ObjectReference.transform.position;
        stateStartExperimentScript.TrialDurationTimeStamp += Time.deltaTime;

        if (stateStartExperimentScript.TrialDurationTimeStamp >= stateStartExperimentScript.TrialMaxDuration &&
            !IsBottleGrasped(palmLastPosition, objectLastPosition))
        {
            Debug.Log("Bitte schneller die Flasche greifen");
            stateStartExperimentScript.TrialTimeOut = true;
        }

        if (stateStartExperimentScript.TrialDurationTimeStamp >= stateStartExperimentScript.TrialMaxDuration &&
            IsBottleGrasped(palmLastPosition, objectLastPosition))
        {
            Debug.Log("Bitte schneller die Aktion ausführen");
            stateStartExperimentScript.TrialTimeOut = true;
        }

        #endregion




        if (Input.GetKey(KeyCode.O))
        {
            ExperimentalTrialSuccesful = true;

        }
        if (Input.GetKey(KeyCode.P))
        {
            ExperimentalTrialNOTSuccesful = true;
        }




        //Ask for to exit training
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            experimentControllerScript.trialOrderLineCounter = 1;
            stateTrainingScript.isStateTraining = false;
        }


        //Debug.Log("Execute StateCheckAction");
        if (Input.GetKeyDown(KeyCode.G))
        {
            finished = true;
        }
        if (finished)
        {
            experimentControllerScript.trialOrderLineCounter++;
            Exit();
        }
    }

    public void Exit()
    {
        ExperimentalTrialSuccesful = false;
        ExperimentalTrialNOTSuccesful = false;
        experimentControllerScript.ResetBools();
        this.finished = true;
        //Debug.Log("Exit StateCheckAction");
        //nextState.Enter();
    }


    private bool IsBottleGrasped(Vector3 lastPositionPalm, Vector3 lastPositionObject)
    {
        if ((Vector3.Distance(lastPositionPalm, lastPositionObject) <= 0.4f) &&
    Vector3.Distance(IndexFingerReference.transform.position, ObjectReference.transform.position) <= 0.4f &&
    Vector3.Distance(ThumbFingerReference.transform.position, ObjectReference.transform.position) <= 0.4f)
        {
            Debug.Log("Bottle is grasped");
            return true;
        }
        else
            return false;
    }

}
