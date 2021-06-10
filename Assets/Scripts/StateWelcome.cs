using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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

        #region Check and get row number of progress in TrialOrderFile
        //Remove comments to check and get the row Number of progress
        //Tuple<int, bool> rowNumberOfProgressInTrialOrderFile = FileWriteManagement.GetRowNumberOfProgressInTrialOrderFile("12");
        //if (rowNumberOfProgressInTrialOrderFile.Item2)
        //    Debug.Log("Where did we stopped?: " + rowNumberOfProgressInTrialOrderFile.Item1);
        //else
        //    Debug.Log("We have not started any trials with this participant");
        #endregion

        #region Check and create ResultFile
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
        //trial.ParticipantID = "01";
        //trial.thumbData = new Vector3();
        //trial.indexData = new Vector3();
        //trial.palmData = new Vector3();
        //trial.objectData = new Vector3();
        //trial.eyeData = new Vector3();
        //var resultFileName = trial.GenerateFileName();







        //Debug.Log("Execute StateWelcome");
        if (Input.GetKeyDown(KeyCode.A))
        {

            finished = true;
        }

        if (finished)
            Exit();
    }

    public void Exit()
    {
        //Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }
}
