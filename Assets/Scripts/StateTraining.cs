using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTraining : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;
    public StateCheckAction stateCheckActionScript;

    public bool isStateTraining;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateTraining");

    }

    public void Execute()
    {
        //Debug.Log("Execute StateTraining");
        if (Input.GetKeyDown(KeyCode.B))
        {
            finished = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Bool State Training set");
            isStateTraining = true;
        }

        if (finished)
            Exit();

    }

    public void Exit()
    {
        //finished = true;
        //Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }

}
