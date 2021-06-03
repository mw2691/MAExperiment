using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckHandPosition : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StartExperiment;



    public void Enter()
    {
        finished = false;
        nextState = StartExperiment.GetComponent<IState>();
        Debug.Log("Enter StateCheckHandPosition");

    }

    public void Execute()
    {
        Debug.Log("Execute StateCheckHandPosition");
        if (Input.GetKeyDown(KeyCode.E))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateCheckHandPosition");
        //nextState.Enter();
    }
}
