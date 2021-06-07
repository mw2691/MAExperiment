using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentController : MonoBehaviour
{
    public StateMachine stateMachineScript;
    public StateInit stateInitScript;

    public IState getState { get; set; }
    public int trialOrderLineCounter = 1;

    private bool waitForInstantiateTrial = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitForInstantiateTrial == false)
        {
            if (stateMachineScript.currentState.ToString() == "StateInit (StateInit)")
            {
                var currentTrialOrder = FileWriteManagement.GetTrialOrderLine("05", trialOrderLineCounter);
                string[] subs = currentTrialOrder.Split(' ');
                Trial currentTrial = new Trial(subs[0], subs[1], subs[2]);
                Debug.Log("CURRENTTRIAAAAL: " + currentTrial.SOAFactors + currentTrial.Interaction + currentTrial.InteractionPlacement);
                waitForInstantiateTrial = true;
            }
        }
        ResetBools();
    }



    void ResetBools()
    {
        this.waitForInstantiateTrial = false;
    }
}
