using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckHandPosition : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StartExperiment;

    public GameObject FixationCross;
    public PositionCheck HandPositionController;
    public CrosshairController Crosshair;
    public PositionCheck ObjectPositionController;

    public Vector3 FixationCrossLocation;
    // in seconds; how long need both checks to be valid before the next state is initiated
    public float CheckDuration = 0.5f;
    private float timeStamp = 0.0f;

    void Start()
    {
        this.FixationCross.SetActive(false);
        this.Crosshair.active = false;
        this.HandPositionController.setControllerMode(PositionCheck.PoseControllerMode.IDLE);
        this.ObjectPositionController.setControllerMode(PositionCheck.PoseControllerMode.IDLE);
    }

    public void Enter()
    {
        finished = false;
        nextState = StartExperiment.GetComponent<IState>();
        Debug.Log("Enter StateCheckHandPosition");

        this.FixationCross.SetActive(true);
        this.FixationCross.transform.position = this.FixationCrossLocation;
        this.Crosshair.reset();
        this.Crosshair.active = true;
        this.timeStamp = 0.0f;

        this.HandPositionController.setControllerMode(PositionCheck.PoseControllerMode.CHECK);
        this.ObjectPositionController.setControllerMode(PositionCheck.PoseControllerMode.CHECK);
    }

    public void Execute()
    {
        if (this.Crosshair.Fixated && this.HandPositionController.InPosition && ObjectPositionController.InPosition)
        {
            this.timeStamp += Time.deltaTime;
            if (this.timeStamp >= this.CheckDuration)
            {
               this.finished = true;
            }
        }
        else
        {
            this.timeStamp += 0.0f;
        }

        //Debug.Log("Execute StateCheckHandPosition");
        if (Input.GetKeyDown(KeyCode.E))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        //Debug.Log("Exit StateCheckHandPosition");
        //nextState.Enter();
        this.FixationCross.SetActive(false);
        this.Crosshair.active = false;
        this.HandPositionController.setControllerMode(PositionCheck.PoseControllerMode.IDLE);
        this.ObjectPositionController.setControllerMode(PositionCheck.PoseControllerMode.IDLE);

    }
}
