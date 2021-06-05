using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    bool finished { get; set; }
    IState nextState { get; set; }

    void Enter();

    void Execute();

    void Exit();

}
