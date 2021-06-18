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


    public GameObject PalmReference;
    public GameObject ObjectReference;
    private Vector3 palmLastPosition;
    private Vector3 objectLastPosition;

    public bool ExperimentalTrialSuccesful;
    public bool ExperimentalTrialNOTSuccesful;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateCheckAction");

        palmLastPosition = PalmReference.transform.position;
        objectLastPosition = ObjectReference.transform.position;

    }

    public void Execute()
    {
        //Check if bottle is grasped with position delta
        //Debug.Log("Deltaaaaa PALM: " + (PalmReference.transform.position - palmLastPosition));
        this.palmLastPosition = PalmReference.transform.position;

        //Debug.Log("Deltaaaaa OBJECT: " + (ObjectReference.transform.position - objectLastPosition));
        this.objectLastPosition = ObjectReference.transform.position;

        Debug.Log("Delta Distance: " + Vector3.Distance(palmLastPosition, objectLastPosition));

        if (Vector3.Distance(palmLastPosition, objectLastPosition) <= 0.4f)
        {
            Debug.Log("Vectors are in equal range");
        }


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
        //Debug.Log("Exit StateCheckAction");
        //nextState.Enter();
    }

}
