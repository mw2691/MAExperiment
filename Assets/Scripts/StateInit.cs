using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateInit : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckHandPosition;
    public ExperimentController ExperimentControllerScript;


    public void Enter()
    {
        finished = false;
        nextState = CheckHandPosition.GetComponent<IState>();
        //Debug.Log("Enter StateInit");
    }

    public void Execute()
    {
        if (ExperimentControllerScript.stateInitFinished)
        {
            finished = true;
        }

        //Debug.Log("Execute StateInit");
        if (Keyboard.current[Key.C].isPressed)
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        //Debug.Log("Exit StateInit");
        //nextState.Enter();
    }


}
