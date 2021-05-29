using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTraining : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        Debug.Log("Enter StateTraining");

    }

    public void Execute()
    {
        Debug.Log("Execute StateTraining");
        if (Input.GetKeyDown(KeyCode.B))
        {
            finished = true;
        }

        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }

}
