using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStartExperiment : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckAction;



    public void Enter()
    {
        finished = false;
        nextState = CheckAction.GetComponent<IState>();
        Debug.Log("Enter StateStartExperiment");

    }

    public void Execute()
    {
        Debug.Log("Execute StateStartExperiment");
        if (Input.GetKeyDown(KeyCode.F))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateStartExperiment");
        //nextState.Enter();
    }
}
