using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckAction : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;



    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateCheckAction");
    }

    public void Execute()
    {
        //Debug.Log("Execute StateCheckAction");
        if (Input.GetKeyDown(KeyCode.G))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        //Debug.Log("Exit StateCheckAction");
        //nextState.Enter();
    }
}
