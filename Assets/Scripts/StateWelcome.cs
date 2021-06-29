using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System;

public class StateWelcome : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateTraining;




    public void Enter()
    {
        finished = false;
        nextState = StateTraining.GetComponent<IState>();
        //Debug.Log("Enter StateWelcome");


    }

    public void Execute()
    {
        //Debug.Log("Execute StateWelcome");
        if (Keyboard.current[Key.A].isPressed)
        {
            finished = true;
        }


        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //    finished = true;
        //}

        if (finished)
            Exit();
    }

    public void Exit()
    {
        //Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }
}
