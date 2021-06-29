using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        if (Keyboard.current[Key.B].isPressed)
        {
            finished = true;
        }

        if (Keyboard.current[Key.Q].isPressed)
        {
            Debug.Log("Bool State Training set");
            isStateTraining = true;
        }

        if (Keyboard.current[Key.W].isPressed)
        {
            isStateTraining = false;
            Debug.Log("Training finished");
            finished = true;
        }

        if (finished)
            Exit();

    }

    public void Exit()
    {
        finished = true;
        //Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }
}
