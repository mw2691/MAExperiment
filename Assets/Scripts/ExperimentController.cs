using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ExperimentController : MonoBehaviour
{
    public StateMachine stateMachineScript;
    public StateStartExperiment stateStartExperimentScript;
    public IState getState { get; set; }

    public bool stateInitFinished;
    public bool stateStartExperimentFinished;
    public bool stateCheckHandPosition;

    private Trial currentTrial;
    public int trialOrderLineCounter = 1;
    public string ParticipantID;
    


    // Start is called before the first frame update
    void Start()
    {
        //FileWriteManagement.WriteProgressInTrialOrderFile("29", 1);

        
        ///Read out command line arguments (demographic data)
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length < 10)
        {
            args = new string[] { "24", "dummyValue1", "dummyArg2", "dummyValue2" };
        }

        ParticipantID = args[0];

        //FileWriteManagement.AppendErrorTrialsToTrialOrderFile(ParticipantID);

        //Create folderstructure for data recording (check for existing directory is implemented in method CreateDirectory())
        FileWriteManagement.CreateDirectory(ParticipantID);

        //Check for progress in TrialOrder
        var tupleRowNumber = FileWriteManagement.GetRowNumberOfProgressInTrialOrderFile(ParticipantID);
        var isProgressInTrialOrder = tupleRowNumber.Item2;
        var rowNumberOfProgressTrialOrder = tupleRowNumber.Item1;
        if (isProgressInTrialOrder)
        {
            trialOrderLineCounter = rowNumberOfProgressTrialOrder + 1;
        }
    }




    // Update is called once per frame
    void Update()
    {
        if (!stateInitFinished)
        {
            if (stateMachineScript.currentState.ToString() == "StateInit (StateInit)")
            {
                //Instantiate Trial
                currentTrial = InstantiateTrial(ParticipantID, trialOrderLineCounter);

                //ResultFile Management (check, create, write header)
                var resultFileName = currentTrial.GenerateFileName();
                Debug.Log("FromExperimentController: " + resultFileName);
                var resultHeader = currentTrial.GenerateHeader();

                if (!FileWriteManagement.CheckExistingFile(resultFileName))
                {
                    var resultFileDirectory = FileWriteManagement.GetResultFileDirectory(ParticipantID);
                    resultFileDirectory = resultFileDirectory + "/" + resultFileName;
                    FileWriteManagement.CreateFile(resultFileDirectory);
                    FileWriteManagement.WriteFile(resultHeader, resultFileDirectory, true);
                }

                stateInitFinished = true;
            }
        }



        if (!stateStartExperimentFinished)
        {
            if (stateMachineScript.currentState.ToString() == "StateStartExperiment (StateStartExperiment)")
            {
                //write results in resultfile
                //generate resultline



                //Check if experimental trial was successful
                if (stateStartExperimentScript.ExperimentalTrialSuccesful)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter);
                    stateStartExperimentFinished = true;
                }
                if (stateStartExperimentScript.ExperimentalTrialNOTSuccesful)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter, "2");
                    stateStartExperimentFinished = true;
                }                
            }
        }
    }


    Trial InstantiateTrial(string participantID, int lineNumber)
    {
        var currentTrialOrder = FileWriteManagement.GetTrialOrderLine(participantID, lineNumber);
        string[] subs = currentTrialOrder.Split(' ');
        Trial _currentTrial = new Trial(participantID, lineNumber, subs[0], subs[1], subs[2]);
        return _currentTrial;
    }





    public void ResetBools()
    {
        this.stateInitFinished = false;
        this.stateStartExperimentFinished = false;
        this.stateCheckHandPosition = false;
    }
}
