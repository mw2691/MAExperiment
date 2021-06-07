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
        Debug.Log("Enter StateInit");



        //var currentTrialOrder = FileWriteManagement.GetTrialOrderLine("05", trialOrderLineCounter);
        //string[] subs = currentTrialOrder.Split(' ');
        //Trial currentTrial = new Trial(subs[0], subs[1], subs[2]);
        //Debug.Log("CURRENTTRIAAAAL: " + currentTrial.SOAFactors + currentTrial.Interaction + currentTrial.InteractionPlacement);

        #region Check, create, and write ResultFile
        ////Remove comments to create resultfile
        //if (!FileWriteManagement.CheckExistingFile(resultFileName))
        //{
        //    FileWriteManagement.CreateFile(resultFileName);
        //    FileWriteManagement.WriteFile(trial.GenerateHeader(), resultFileName);
        //}
        //else if (resultFileName.StartsWith(trial.ParticipantID))
        //{
        //    FileWriteManagement.WriteFile(trial.GenerateResultLine(), resultFileName, true);
        //}
        #endregion


    }

    public void Execute()
    {

        Debug.Log("Execute StateInit");
        if (Input.GetKeyDown(KeyCode.C))
        {
            finished = true;
        }
        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateInit");
        //nextState.Enter();
    }


}
