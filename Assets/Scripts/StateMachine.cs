using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;
    public GameObject StateWelcome;

    // Start is called before the first frame update
    void Start()
    {
        currentState = StateWelcome.GetComponent<IState>();
        currentState.Enter();
    }


    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
        if (currentState.finished)
        {
            //currentState.Exit();
            currentState = currentState.nextState;
            currentState.Enter();
        }


        Debug.Log("CurrentState:::::: " + currentState);
    }
}
