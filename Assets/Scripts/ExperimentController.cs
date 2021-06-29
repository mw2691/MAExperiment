using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public GameObject CrossHairControllerReference;

    public bool stateInitFinished;
    public bool stateStartExperimentFinished;
    public bool stateCheckHandPositionFinished;
    public bool stateCheckAction;
    public bool stateInitActionAppendOfErrorTrialsIsFinished;
    public bool stateCheckActionAppendRemainingErrorTrialsIsFinished;
    private bool isKeyPressedToEditTrialRow;

    private const string StateInit = "StateInit (StateInit)";
    private const string StateStartExperiment = "StateStartExperiment (StateStartExperiment)";
    private const string StateCheckAction = "StateCheckAction (StateCheckAction)";
    private const string StateCheckHandPosition = "StateCheckHandPosition (StateCheckHandPosition)";

    private const string StartAnnotation = "Start";
    private const string ReachForBottleAnnotation = "ReachForBottle";
    private const string GraspedAnnotation = "Grasped";
    private const string TrialActionEndedAnnotation = "TrialActionEnded";
    private const string TrialNotSuccesful= "TrialNotSuccesful";

    private const string GazeAnnotationBottle = "Bottle";
    private const string GazeAnnotationFixationCross = "FixationCross";
    private const string GazeAnnotationCupLeft = "CupLeft";
    private const string GazeAnnotationCupRight = "CupRight";
    private const string GazeAnnotationHand = "Hand";
    private const string GazeAnnotationDefault = "Default";


    //Annotationbools
    private bool StartExperimentSOA1Bool1;
    private bool StartExperimentSOA1Bool2;
    private bool StartExperimentSOA1Bool3;
    private bool StartExperimentSOA2Bool1;
    private bool StartExperimentSOA2Bool2;
    private bool StartExperimentSOA2Bool3;
    private bool StartExperimentSOA3Bool1;
    private bool StartExperimentSOA3Bool2;
    private bool StartExperimentSOA3Bool3;
    private bool StartExperimentSOA4Bool1;
    private bool StartExperimentSOA4Bool2;
    private bool StartExperimentSOA4Bool3;

    private bool CheckActionSOA1Bool1;
    private bool CheckActionSOA1Bool2;
    private bool CheckActionSOA1Bool3;
    private bool CheckActionSOA2Bool1;
    private bool CheckActionSOA2Bool2;
    private bool CheckActionSOA2Bool3;
    private bool CheckActionSOA3Bool1;
    private bool CheckActionSOA3Bool2;
    private bool CheckActionSOA3Bool3;
    private bool CheckActionSOA4Bool1;
    private bool CheckActionSOA4Bool2;
    private bool CheckActionSOA4Bool3;




    public Trial currentTrial;
    public int trialOrderLineCounter = 1;
    public string ParticipantID;
    public int trialOrderLineCounterBeforeAppendErrorTrials;

    private string resultFileName;
    private string resultFileDirectory;

    public AnnotationStates currentAnnotationState;
    public GazeAnnotationStates currentGazeAnnotationState;

    public enum AnnotationStates
    {
        Start,
        ReachForBottle,
        Grasped,
        TrialActionEnded,
        TrialNotSuccesful
    }

    public enum GazeAnnotationStates
    {
        GazeAnnotationDefault,
        GazeAnnotationBottle,
        GazeAnnotationFixationCross,
        GazeAnnotationCupLeft,
        GazeAnnotationCupRight,
        GazeAnnotationHand
    }

    // Start is called before the first frame update
    void Start()
    {

        currentAnnotationState = AnnotationStates.Start;
        currentGazeAnnotationState = GazeAnnotationStates.GazeAnnotationDefault;
        //FileWriteManagement.WriteProgressInTrialOrderFile("29", 1);


        ///Read out command line arguments (demographic data)
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length < 12)
        {
            args = new string[] { "10", "dummyValue1", "dummyArg2", "dummyValue2" };
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
                if (!stateTrainingScript.isStateTraining && stateTrainingScript.finished)
                {
                    trialOrderLineCounter = 1;
                }

                ResetAnnotationBools();
                CrossHairControllerReference.SetActive(true);
                currentAnnotationState = AnnotationStates.Start;
                if (!stateInitActionAppendOfErrorTrialsIsFinished)
                {
                    if (FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter))
                    {
                        FileWriteManagement.AppendErrorTrialsToTrialOrderFile(ParticipantID);
                        stateInitActionAppendOfErrorTrialsIsFinished = true;
                    }
                }

                var lastprogressValue = FileWriteManagement.GetProgressValueFromTrialOrderLine(ParticipantID);
                if (FileWriteManagement.CheckAllTrialsAreEqualToTrialOrderCounter(ParticipantID, trialOrderLineCounter) &&
                    lastprogressValue == "1" &&
                    stateInitActionAppendOfErrorTrialsIsFinished)
                {
                    EditorApplication.isPlaying = false;
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
                currentTrial.resultFileGazeAnnotations = GazeAnnotationInResultFile(currentGazeAnnotationState);

                stateInitFinished = true;
            }
        }

        if (!stateCheckHandPositionFinished)
        {
            if (stateMachineScript.currentState.ToString() == StateCheckHandPosition)
            {
                if (!isKeyPressedToEditTrialRow)
                {
                    if (Keyboard.current[Key.P].isPressed)
                    {
                        var counter = trialOrderLineCounter;
                        FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, counter-1, "0");
                        isKeyPressedToEditTrialRow = true;
                    }
                }
                if (Keyboard.current[Key.L].isPressed)
                {
                    isKeyPressedToEditTrialRow = false;
                    stateCheckHandPositionFinished = true;
                }
            }
        }



        if (!stateStartExperimentFinished)
        {
            if (stateMachineScript.currentState.ToString() == StateStartExperiment)
            {
                #region Annotations
                //Annotations SOA1
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!StartExperimentSOA1Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            StartExperimentSOA1Bool1 = true;
                        }
                    }

                    if (!StartExperimentSOA1Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA1Bool2 = true;
                        }
                    }

                    if (!StartExperimentSOA1Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA1Bool3 = true;
                        }
                    }
                }

                //Annotations SOA2
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!StartExperimentSOA2Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            StartExperimentSOA2Bool1 = true;
                        }
                    }

                    if (!StartExperimentSOA2Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA2Bool2 = true;
                        }
                    }

                    if (!StartExperimentSOA2Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA2Bool3 = true;
                        }
                    }
                }

                //Annotations SOA3
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!StartExperimentSOA3Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            StartExperimentSOA3Bool1 = true;
                        }
                    }

                    if (!StartExperimentSOA3Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA3Bool2 = true;
                        }
                    }

                    if (!StartExperimentSOA3Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA3Bool3 = true;
                        }
                    }
                }

                //Annotations SOA4
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!StartExperimentSOA4Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            StartExperimentSOA4Bool1 = true;
                        }
                    }

                    if (!StartExperimentSOA4Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA4Bool2 = true;
                        }
                    }

                    if (!StartExperimentSOA4Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            StartExperimentSOA4Bool3 = true;
                        }
                    }
                }
                #endregion

                CrossHairControllerReference.SetActive(false);
                //write results in resultfile
                //generate resultline
                currentTrial.palmData = PalmReference.transform.position;
                currentTrial.thumbData = ThumbReference.transform.position;
                currentTrial.indexData = IndexReference.transform.position;
                currentTrial.eyeData = EyeReference.transform.position;
                currentTrial.objectData = ObjectReference.transform.position;
                currentTrial.resultFileAnnotations = AnnotationInResultFile(currentAnnotationState);
                currentTrial.resultFileGazeAnnotations = GazeAnnotationInResultFile(currentGazeAnnotationState);

                FileWriteManagement.WriteFile(currentTrial.GenerateResultLine(), resultFileDirectory, true);
            }
        }





        if (!stateCheckAction)
        {
            if (stateMachineScript.currentState.ToString() == StateCheckAction)
            {
                #region Annotations
                //Annotations SOA1
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!CheckActionSOA1Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            CheckActionSOA1Bool1 = true;
                        }
                    }

                    if (!CheckActionSOA1Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA1Bool2 = true;
                        }
                    }

                    if (!CheckActionSOA1Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA1Bool3 = true;
                        }
                    }
                }

                //Annotations SOA2
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!CheckActionSOA2Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            CheckActionSOA2Bool1 = true;
                        }
                    }

                    if (!CheckActionSOA2Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA2Bool2 = true;
                        }
                    }

                    if (!CheckActionSOA2Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA2Bool3 = true;
                        }
                    }
                }

                //Annotations SOA3
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!CheckActionSOA3Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            CheckActionSOA3Bool1 = true;
                        }
                    }

                    if (!CheckActionSOA3Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA3Bool2 = true;
                        }
                    }

                    if (!CheckActionSOA3Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA3Bool3 = true;
                        }
                    }
                }

                //Annotations SOA4
                if (currentTrial.SOAFactors == "SOA1")
                {
                    if (!CheckActionSOA4Bool1)
                    {
                        if (stateStartExperimentScript.AtMovementOnset())
                        {
                            currentAnnotationState = AnnotationStates.ReachForBottle;
                            CheckActionSOA4Bool1 = true;
                        }
                    }

                    if (!CheckActionSOA4Bool2)
                    {
                        if (stateStartExperimentScript.AtBottle())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA4Bool2 = true;
                        }
                    }

                    if (!CheckActionSOA4Bool3)
                    {
                        if (stateStartExperimentScript.IsBottleGraspedAndMoved())
                        {
                            currentAnnotationState = AnnotationStates.Grasped;
                            CheckActionSOA4Bool3 = true;
                        }
                    }
                }
                #endregion

                currentTrial.palmData = PalmReference.transform.position;
                currentTrial.thumbData = ThumbReference.transform.position;
                currentTrial.indexData = IndexReference.transform.position;
                currentTrial.eyeData = EyeReference.transform.position;
                currentTrial.objectData = ObjectReference.transform.position;
                currentTrial.resultFileAnnotations = AnnotationInResultFile(currentAnnotationState);
                currentTrial.resultFileGazeAnnotations = GazeAnnotationInResultFile(currentGazeAnnotationState);

                FileWriteManagement.WriteFile(currentTrial.GenerateResultLine(), resultFileDirectory, true);

                if (stateCheckActionScript.ExperimentalTrialSuccesful && !stateTrainingScript.isStateTraining)
                {
                    FileWriteManagement.WriteProgressInTrialOrderFile(ParticipantID, trialOrderLineCounter);
                    stateCheckAction = true;
                }

                if (stateCheckActionScript.ExperimentalTrialSuccesful && stateTrainingScript.isStateTraining)
                {
                    stateCheckAction = true;
                }
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

    private string GazeAnnotationInResultFile(GazeAnnotationStates gazeAnnotationState)
    {
        switch (gazeAnnotationState)
        {
            case GazeAnnotationStates.GazeAnnotationDefault:
                return GazeAnnotationDefault;
            case GazeAnnotationStates.GazeAnnotationBottle:
                return GazeAnnotationBottle;
            case GazeAnnotationStates.GazeAnnotationFixationCross:
                return GazeAnnotationFixationCross;
            case GazeAnnotationStates.GazeAnnotationCupLeft:
                return GazeAnnotationCupLeft;
            case GazeAnnotationStates.GazeAnnotationCupRight:
                return GazeAnnotationCupRight;
            case GazeAnnotationStates.GazeAnnotationHand:
                return GazeAnnotationHand;
            default:
                return "Gaze Annotation State Not defined";

        }
    }

    public void ResetAnnotationBools()
    {
        this.StartExperimentSOA1Bool1 = false;
        this.StartExperimentSOA1Bool2 = false;
        this.StartExperimentSOA1Bool3 = false;
        this.StartExperimentSOA2Bool1 = false;
        this.StartExperimentSOA2Bool2 = false;
        this.StartExperimentSOA2Bool3 = false;
        this.StartExperimentSOA3Bool1 = false;
        this.StartExperimentSOA3Bool2 = false;
        this.StartExperimentSOA3Bool3 = false;
        this.StartExperimentSOA4Bool1 = false;
        this.StartExperimentSOA4Bool2 = false;
        this.StartExperimentSOA4Bool3 = false;


        this.CheckActionSOA1Bool1 = false;
        this.CheckActionSOA1Bool2 = false;
        this.CheckActionSOA1Bool3 = false;
        this.CheckActionSOA2Bool1 = false;
        this.CheckActionSOA2Bool2 = false;
        this.CheckActionSOA2Bool3 = false;
        this.CheckActionSOA3Bool1 = false;
        this.CheckActionSOA3Bool2 = false;
        this.CheckActionSOA3Bool3 = false;
        this.CheckActionSOA4Bool1 = false;
        this.CheckActionSOA4Bool2 = false;
        this.CheckActionSOA4Bool3 = false;

    }



public void ResetBools()
    {
        this.stateInitFinished = false;
        this.stateStartExperimentFinished = false;
        this.stateCheckHandPositionFinished = false;
        this.stateCheckAction = false;
        stateCheckActionAppendRemainingErrorTrialsIsFinished = true;
    }
}
