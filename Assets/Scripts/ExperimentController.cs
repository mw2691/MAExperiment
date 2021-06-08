using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ExperimentController : MonoBehaviour
{
    public StateMachine stateMachineScript;
    public StateInit stateInitScript;

    public IState getState { get; set; }
    public int trialOrderLineCounter = 1;

    private bool waitForInstantiateTrial = false;

    private Trial currentTrial;
    


    // Start is called before the first frame update
    void Start()
    {

        ///Read out command line arguments (demographic data) from java program)
        /*string[] args = System.Environment.GetCommandLineArgs();
        string input = "";
        for (int i = 0; i < args.Length; i++)
        {
            Debug.Log("ARG " + i + ": " + args[i]);
        }*/



        //Create folderstructure for data recording
        FileWriteManagement.CreateDirectory("01");
    }




    // Update is called once per frame
    void Update()
    {
        if (waitForInstantiateTrial == false)
        {
            if (stateMachineScript.currentState.ToString() == "StateInit (StateInit)")
            {
                //Instantiate Trial
                currentTrial = InstantiateTrial("01", trialOrderLineCounter);
                //Debug.Log("From EXPERIMENTCONTROLLER: " + currentTrial.SOAFactors + currentTrial.Interaction + currentTrial.InteractionPlacement);


                var resultFileName = currentTrial.GenerateFileName();
                var resultHeader = currentTrial.GenerateHeader();



                if (FileWriteManagement.CheckExistingFile(resultFileName))
                    Debug.Log("ok");
                ////Check for ResultFile
                //if (!FileWriteManagement.CheckExistingFile(resultFileName))
                //{
                //    FileWriteManagement.CreateFile(resultFileName);
                //    FileWriteManagement.WriteFile(trial.GenerateHeader(), resultFileName);
                //}
                //else if (resultFileName.StartsWith(trial.ParticipantID))
                //{
                //    FileWriteManagement.WriteFile(trial.GenerateResultLine(), resultFileName, true);
                //}

                var resultFileDirectory = FileWriteManagement.GetResultFileDirectory("01");
                resultFileDirectory = resultFileDirectory + "/" + resultFileName;
                FileWriteManagement.CreateFile(resultFileDirectory);
                //Write Header into file
                FileWriteManagement.WriteFile(resultHeader, resultFileDirectory, true);


                waitForInstantiateTrial = true;
            }
        }





    }


    Trial InstantiateTrial(string participantID, int lineNumber)
    {
        var currentTrialOrder = FileWriteManagement.GetTrialOrderLine(participantID, lineNumber);
        string[] subs = currentTrialOrder.Split(' ');
        Trial _currentTrial = new Trial(participantID, subs[0], subs[1], subs[2]);
        //Debug.Log("CURRENTTRIAAAAL: " + currentTrial.SOAFactors + currentTrial.Interaction + currentTrial.InteractionPlacement);
        return _currentTrial;
    }





    public void ResetBools()
    {
        this.waitForInstantiateTrial = false;
    }
}
