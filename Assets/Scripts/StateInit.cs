using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInit : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckObjectPosition;


    public void Enter()
    {
        finished = false;
        nextState = CheckObjectPosition.GetComponent<IState>();
        //Debug.Log("Enter StateInit");
    }

    public void Execute()
    {
        //Debug.Log("Execute StateInit");
        if (Input.GetKeyDown(KeyCode.C))
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
