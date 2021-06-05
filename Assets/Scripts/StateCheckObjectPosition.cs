using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckObjectPosition : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckHandPosition;



    public void Enter()
    {
        finished = false;
        nextState = CheckHandPosition.GetComponent<IState>();
        Debug.Log("Enter StateCheckObjectPosition");

    }

    public void Execute()
    {
        Debug.Log("Execute StateCheckObjectPosition");
        if (Input.GetKeyDown(KeyCode.D))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateCheckObjectPosition");
        //nextState.Enter();
    }
}
