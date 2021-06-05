using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateWelcome : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateTraining;
    Trial trial= new Trial("Halfway", "Einschenken", "Links");


    public void Enter()
    {
        finished = false;
        nextState = StateTraining.GetComponent<IState>();
        Debug.Log("Enter StateWelcome");

    }

    public void Execute()
    {
        trial.ParticipantID = "01";
        trial.thumbData = new Vector3();
        trial.indexData = new Vector3();
        trial.palmData = new Vector3();
        trial.objectData = new Vector3();
        trial.eyeData = new Vector3();
        var resultFileName = trial.GenerateFileName();

        //Remove comments to create resultfile
        //if (!FileWriteManagement.CheckExistingFile(resultFileName))
        //{
        //    FileWriteManagement.CreateFile(resultFileName);
        //    FileWriteManagement.WriteFile(trial.GenerateHeader(), resultFileName);
        //}
        //else if (resultFileName.StartsWith(trial.ParticipantID))
        //{
        //    FileWriteManagement.WriteFile(trial.GenerateResultLine(), resultFileName, true);
        //}




        Debug.Log("Execute StateWelcome");
        if (Input.GetKeyDown(KeyCode.A))
        {

            finished = true;
        }

        if (finished)
            Exit();
    }

    public void Exit()
    {
        Debug.Log("Exit StateWelcome");
        //nextState.Enter();
    }
}
