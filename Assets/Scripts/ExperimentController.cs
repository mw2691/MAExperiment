using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System;


public class ExperimentController : MonoBehaviour
{
    public StateMachine stateMachineScript;
    public StateStartExperiment stateStartExperimentScript;
    public IState getState { get; set; }
    public StateTraining stateTrainingScript;
    public StateCheckAction stateCheckActionScript;

    public GameObject PalmReference;
    public GameObject ThumbReference;
    public GameObject IndexReference;
    public GameObject EyeReference;
    public GameObject ObjectReference;

    public bool stateInitFinished;
    public bool stateStartExperimentFinished;
    public bool stateCheckHandPosition;
    public bool stateCheckAction;
    public bool stateInitActionAppendOfErrorTrialsIsFinished;
    public bool stateCheckActionAppendRemainingErrorTrialsIsFinished;

    private const string StateInit = "StateInit (StateInit)";
    private const string StateStartExperiment = "StateStartExperiment (StateStartExperiment)";
    private const string StateCheckAction = "StateCheckAction (StateCheckAction)";

    private const string StartAnnotation = "Start";
    private const string ReachForBottleAnnotation = "ReachForBottle";
    private const string GraspedAnnotation = "Grasped";
    private const string TrialActionEndedAnnotation = "TrialActionEnded";
    private const string TrialNotSuccesful= "TrialNotSuccesful";


    public Trial currentTrial;
    public int trialOrderLineCounter = 1;
    public string ParticipantID;
    public int trialOrderLineCounterBeforeAppendErrorTrials;

    private string resultFileName;
    private string resultFileDirectory;

    public AnnotationStates currentAnnotationState;

    public enum AnnotationStates
    {
        Start,
        ReachForBottle,
        Grasped,
        TrialActionEnded,
        TrialNotSuccesful
    }


    // Start is called before the first frame update
    void Start()
    {

        currentAnnotationState = AnnotationStates.Start;
        //FileWriteManagement.WriteProgressInTrialOrderFile("29", 1);


        ///Read out command line arguments (demographic data)
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length < 12)
        {
            args = new string[] { "09", "dummyValue1", "dummyArg2", "dummyValue2" };
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
            if (stateMachineScript.currentState.ToString() == StateInit)
            {
                //var aa = FileWriteManagement.CountRowsinTrialOrderFile(ParticipantID);
                //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa TrialOrderLineCounter: " + trialOrderLineCounter);
                //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa Amount of Rows in File: " + aa);


                if (!stateInitActionAppendOfErrorTrialsIsFinished)
                {
                    if (FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter))
                    {
                        Debug.Log("Ich appende die ErrorTrials");
                        FileWriteManagement.AppendErrorTrialsToTrialOrderFile(ParticipantID);
                        trialOrderLineCounterBeforeAppendErrorTrials = trialOrderLineCounter;
                        stateCheckActionAppendRemainingErrorTrialsIsFinished = true;
                        stateInitActionAppendOfErrorTrialsIsFinished = true;
                    }
                }


                if (stateCheckActionAppendRemainingErrorTrialsIsFinished)
                {
                    //Debug.Log("Before appending remaining error trials            TrialOrderLineCounter: " + trialOrderLineCounter + ", " + "CountRowsinTrialOrderFile: " + FileWriteManagement.CountRowsinTrialOrderFile(ParticipantID));
                    if (FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter))
                    {
                        Debug.Log("3.   AppendRemainingErrors");
                        FileWriteManagement.AppendRemainingErrorTrials(ParticipantID, trialOrderLineCounter);
                        stateCheckActionAppendRemainingErrorTrialsIsFinished = false;
                    }
                }


                var rowsInTrialOrderFile = FileWriteManagement.CountRowsinTrialOrderFile(ParticipantID);
                var progressValue = FileWriteManagement.GetProgressValueFromTrialOrderLine(ParticipantID);
                var testo = FileWriteManagement.CheckForRemainingErrorTrials(ParticipantID, trialOrderLineCounter);
                if (progressValue == "1" && !testo)
                {
                    EditorApplication.isPlaying = false;
                    //Application.Quit();
                }


                //Instantiate Trial
                currentTrial = InstantiateTrial(ParticipantID, trialOrderLineCounter);

                //ResultFile Management (check, create, write header)
                this.resultFileName = currentTrial.GenerateFileName();
                var resultHeader = currentTrial.GenerateHeader();


                //Check for training state and append train to result file name
                if (stateTrainingScript.isStateTraining)
                {
                    string[] subString = resultFileName.Split('_');
                    subString[0] += "_train";
                    resultFileName = string.Join("_", subString);
                }



                if (!FileWriteManagement.CheckExistingFile(resultFileName))
                {
                    resultFileDirectory = FileWriteManagement.GetResultFileDirectory(ParticipantID);
                    resultFileDirectory = resultFileDirectory + "/" + resultFileName;
                    FileWriteManagement.CreateFile(resultFileDirectory);
                    FileWriteManagement.WriteFile(resultHeader, resultFileDirectory, true);
                }

                currentTrial.palmData = PalmReference.transform.position;
                currentTrial.thumbData = ThumbReference.transform.position;
                currentTrial.indexData = IndexReference.transform.position;
                currentTrial.eyeData = EyeReference.transform.position;
                currentTrial.objectData = ObjectReference.transform.position;
                currentTrial.resultFileAnnotations = AnnotationInResultFile(currentAnnotationState);

                stateInitFinished = true;
            }
        }



        if (!stateStartExperimentFinished)
        {
            if (stateMachineScript.currentState.ToString() == StateStartExperiment)
            {
                //write results in resultfile
                //generate resultline
                //Debug.Log("stateStartExperiment: " + currentTrial.GenerateResultLine());
                currentTrial.palmData = PalmReference.transform.position;
                currentTrial.thumbData = ThumbReference.transform.position;
                currentTrial.indexData = IndexReference.transform.position;
                currentTrial.eyeData = EyeReference.transform.position;
                currentTrial.objectData = ObjectReference.transform.position;
                currentTrial.resultFileAnnotations = AnnotationInResultFile(currentAnnotationState);
                
                FileWriteManagement.WriteFile(currentTrial.GenerateResultLine(), resultFileDirectory, true);
  


                //Check if experimental trial was successful --> eventuell in den stateCheckAction packen
                //if (stateStartExperimentScript.ExperimentalTrialSuccesful && !stateTrainingScript.isStateTraining)
                //{
                //    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter);
                //    stateStartExperimentFinished = true;
                //}
                //if (stateStartExperimentScript.ExperimentalTrialNOTSuccesful && !stateTrainingScript.isStateTraining)
                //{
                //    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter, "2");
                //    stateStartExperimentFinished = true;
                //}
            }
        }





        if (!stateCheckAction)
        {
            if (stateMachineScript.currentState.ToString() == StateCheckAction)
            {
                currentTrial.palmData = PalmReference.transform.position;
                currentTrial.thumbData = ThumbReference.transform.position;
                currentTrial.indexData = IndexReference.transform.position;
                currentTrial.eyeData = EyeReference.transform.position;
                currentTrial.objectData = ObjectReference.transform.position;
                currentTrial.resultFileAnnotations = AnnotationInResultFile(currentAnnotationState);
                
                FileWriteManagement.WriteFile(currentTrial.GenerateResultLine(), resultFileDirectory, true);
                

                if (stateStartExperimentScript.TrialTimeOut)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter, "2");
                }

                if (stateCheckActionScript.ExperimentalTrialSuccesful && !stateStartExperimentScript.TrialTimeOut)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter);
                    stateCheckAction = true;
                }
                if (stateCheckActionScript.ExperimentalTrialNOTSuccesful && !stateStartExperimentScript.TrialTimeOut)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter, "2");
                    stateCheckAction = true;
                }

                //Debug.Log("StateCheckAction: " + currentTrial.GenerateResultLine());
                //FileWriteManagement.WriteFile(currentTrial.GenerateResultLine(), resultFileName, true);
                //if (stateCheckActionAppendRemainingErrorTrialsIsFinished)
                //{
                //    //Debug.Log("1.   !stateInitAppendRemainingErrorTrialsIsFinished");
                //    if (FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter))
                //    {
                //        //Debug.Log("2.   FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter)");
                //        Debug.Log("3.   AppendRemainingErrors");
                //        FileWriteManagement.AppendRemainingErrorTrials(ParticipantID, trialOrderLineCounter);
                //        stateCheckActionAppendRemainingErrorTrialsIsFinished = false;
                //    }
                //}
            }
            //stateCheckAction = true;
        }
    }


    Trial InstantiateTrial(string participantID, int lineNumber)
    {
        var currentTrialOrder = FileWriteManagement.GetTrialOrderLine(participantID, lineNumber);
        string[] subs = currentTrialOrder.Split(' ');
        Trial _currentTrial = new Trial(participantID, lineNumber, subs[0], subs[1], subs[2]);
        return _currentTrial;
    }



    private string AnnotationInResultFile(AnnotationStates annotationState)
    {
        switch (annotationState)
        {
            case AnnotationStates.Start:
                return StartAnnotation;
            case AnnotationStates.ReachForBottle:
                return ReachForBottleAnnotation;
            case AnnotationStates.Grasped:
                return GraspedAnnotation;
            case AnnotationStates.TrialActionEnded:
                return TrialActionEndedAnnotation;
            case AnnotationStates.TrialNotSuccesful:
                return TrialNotSuccesful;
            default:
                return "Annotation State Not defined";

        }
    }




    public void ResetBools()
    {
        this.stateInitFinished = false;
        this.stateStartExperimentFinished = false;
        this.stateCheckHandPosition = false;
        this.stateCheckAction = false;
        stateCheckActionAppendRemainingErrorTrialsIsFinished = true;
    }
}
