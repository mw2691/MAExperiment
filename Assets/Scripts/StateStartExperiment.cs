using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateStartExperiment : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject CheckAction;
    public StateInit StateInitScript;
    public ExperimentController ExperimentControllerScript;
    public Transform HandReference;
    public Transform ObjectReference;
    public MeshRenderer ObjectMesh;
    public GameObject FixationCross;
    public StateCheckHandPosition StateCheckHandPositionScript;
    public StateCheckAction StateCheckActionScript;
    public Transform HandStartPosition;
    public Transform ObjectStartPosition;
    public Transform ThumbReferencePosition;
    public Transform IndexFingerReferencePosition;


    //visual cues
    public GameObject PlaceStimulationLeft;
    public GameObject PlaceStimulationRight;
    public GameObject PourStimulationLeft;
    public GameObject PourStimulationRight;


    public bool ExperimentalTrialSuccesful;
    //public bool ExperimentalTrialNOTSuccesful;
    //public bool TrialTimeOut;

    private Trial currentTrialConditions;

    private const string SOA1 = "SOA1";
    private const string SOA2 = "SOA2";
    private const string SOA3 = "SOA3";
    private const string SOA4 = "SOA4";
    private const string InteractionPour = "Pour";
    private const string InteractionPlace = "Place";
    private const string PlacementLeft = "Left";
    private const string PlacementRight = "Right";

    private float constDistanceBetweenHandAndObject;

    public float CheckDuration = 0.5f;
    //private float timeStamp = 0.0f;
    public float TrialDurationTimeStamp = 0.0f;
    public float TrialMaxDuration = 25.0f;

    private Vector3 lastPositionPalm;
    private Vector3 lastPositionObject;
    private Vector3 currentPositionPalm;
    private Vector3 currentPositionObject;



    private void Start()
    {
        this.PlaceStimulationLeft.SetActive(false);
        this.PlaceStimulationRight.SetActive(false);
        this.PourStimulationLeft.SetActive(false);
        this.PourStimulationRight.SetActive(false);


    }

    public void Enter()
    {
        finished = false;
        //TrialTimeOut = false;
        nextState = CheckAction.GetComponent<IState>();
        //Debug.Log("Enter StateStartExperiment");
        currentTrialConditions = ExperimentControllerScript.currentTrial;

        var handReferenceZ = new Vector3(0, 0, HandStartPosition.position.z);
        var objectReferenceZ = new Vector3(0, 0, ObjectStartPosition.position.z);
        constDistanceBetweenHandAndObject = Vector3.Distance(handReferenceZ, objectReferenceZ);

        //this.timeStamp = 0.0f;
        this.TrialDurationTimeStamp = 0.0f;
        this.TrialMaxDuration = 25.0f;

        lastPositionPalm = HandReference.transform.position;
        lastPositionObject = ObjectReference.transform.position;

    }

    public void Execute()
    {
        currentPositionPalm = HandReference.transform.position;
        currentPositionObject = ObjectReference.transform.position;

        if (!FixationCross.activeSelf && StateCheckHandPositionScript.finished)
        {
            //TrialDurationTimeStamp += Time.deltaTime;
            StartExperiment(currentTrialConditions);
        }

        lastPositionPalm = currentPositionPalm;
        lastPositionObject = currentPositionObject;

        //if (TrialDurationTimeStamp >= TrialMaxDuration)
        //{
        //    TrialTimeOut = true;
        //    this.finished = true;
        //}


        //for test: Experiment was good
        if (Keyboard.current[Key.O].isPressed)
        {
            ExperimentalTrialSuccesful = true;

        }
        //if(Keyboard.current[Key.P].isPressed)
        //{
        //    ExperimentalTrialNOTSuccesful = true;
        //}

        //Debug.Log("Execute StateStartExperiment");
        if (Keyboard.current[Key.F].isPressed)
        {
            this.finished = true;
        }
        if (finished)
        {
            //ExperimentControllerScript.trialOrderLineCounter++;
            //Exit();
        }
    }

    public void Exit()
    {
        ExperimentalTrialSuccesful = false;
        //ExperimentalTrialNOTSuccesful = false;
        //ExperimentControllerScript.ResetBools();

        //Debug.Log("Exit StateStartExperiment");
        //nextState.Enter();
    }


        public void StartExperiment(Trial trialInfo)
    {
        //checks for SOA conditions
        if (trialInfo.SOAFactors == SOA1)
        {
            StartCoroutine(SOA1MovementOnset());
        }

        if (trialInfo.SOAFactors == SOA2)
        {
            StartCoroutine(SOA2Halfway());
        }

        if (trialInfo.SOAFactors == SOA3)
        {
            StartCoroutine(SOA3AtBottle());
        }

        if (trialInfo.SOAFactors == SOA4)
        {
            StartCoroutine(SOA4AfterGrasp());
        }
    } 


    private IEnumerator SOA1MovementOnset()
    {
        while (!AtMovementOnset())
        {
            yield return null;
        }
        //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.ReachForBottle;
        StartStimulation(currentTrialConditions.Interaction, currentTrialConditions.InteractionPlacement);
        yield return new WaitForSeconds(1.0f);
        PlaceStimulationLeft.SetActive(false);
        PlaceStimulationRight.SetActive(false);
        PourStimulationLeft.SetActive(false);
        PourStimulationRight.SetActive(false);
        this.finished = true;
    }
    
    private IEnumerator SOA2Halfway()
    {
        while (!HalfWayDone())
        {
            yield return null;
        }
        //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.ReachForBottle;
        StartStimulation(currentTrialConditions.Interaction, currentTrialConditions.InteractionPlacement);
        yield return new WaitForSeconds(1.0f);
        PlaceStimulationLeft.SetActive(false);
        PlaceStimulationRight.SetActive(false);
        PourStimulationLeft.SetActive(false);
        PourStimulationRight.SetActive(false);
        this.finished = true;
    }

    private IEnumerator SOA3AtBottle()
    {
        while (!AtBottle())
        {
            yield return null;
        }
        //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.ReachForBottle;
        StartStimulation(currentTrialConditions.Interaction, currentTrialConditions.InteractionPlacement);
        yield return new WaitForSeconds(1.0f);
        PlaceStimulationLeft.SetActive(false);
        PlaceStimulationRight.SetActive(false);
        PourStimulationLeft.SetActive(false);
        PourStimulationRight.SetActive(false);
        this.finished = true;
    }

    private IEnumerator SOA4AfterGrasp()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.ReachForBottle;
        StartStimulation(currentTrialConditions.Interaction, currentTrialConditions.InteractionPlacement);
        yield return new WaitForSeconds(1.0f);
        PlaceStimulationLeft.SetActive(false);
        PlaceStimulationRight.SetActive(false);
        PourStimulationLeft.SetActive(false);
        PourStimulationRight.SetActive(false);
        this.finished = true;
    }

    public bool AtMovementOnset()
    {
        if (!FixationCross.activeSelf)
        {
            //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.ReachForBottle;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HalfWayDone()
    {
        var handStartZ = new Vector3(0, 0, HandReference.position.z);
        var objectStartZ = new Vector3(0, 0, ObjectStartPosition.position.z);
        var distanceBetweenHandAndObject = Vector3.Distance(handStartZ, objectStartZ);

        if (distanceBetweenHandAndObject <= (constDistanceBetweenHandAndObject) / 2)
        {
            return true;
        }
        else
            return false;
    }

    public bool AtBottle()
    {
        var thumb = new Vector3(0, 0, ThumbReferencePosition.position.z);
        var indexFinger = new Vector3(0, 0, IndexFingerReferencePosition.position.z);
        var objectTransform = new Vector3(0, 0, ObjectReference.position.z);
        var distanceBetweenThumbAndObject = Vector3.Distance(thumb, objectTransform);
        var distanceBetweenIndexAndObject = Vector3.Distance(indexFinger, objectTransform);

        if (distanceBetweenThumbAndObject <= 2.0f && distanceBetweenIndexAndObject <= 2.0f)
        {
            Debug.Log("At bottle is true");
            return true;
        }
        return false;
    }

    public bool IsBottleGraspedAndMoved()
    {
        var thumb = new Vector3(0, 0, ThumbReferencePosition.position.z);
        var indexFinger = new Vector3(0, 0, IndexFingerReferencePosition.position.z);
        var objectTransform = new Vector3(0, 0, ObjectReference.transform.position.z);
        var distanceBetweenThumbAndObject = Vector3.Distance(thumb, objectTransform);
        var distanceBetweenIndexAndObject = Vector3.Distance(indexFinger, objectTransform);
        var velocityHand = (currentPositionPalm - lastPositionPalm) / Time.deltaTime;
        var velocityObject = (currentPositionObject - lastPositionObject) / Time.deltaTime;
        var thresholdVector = new Vector3(1, 1, 1);
        var thresholdValue = 1.5f;

        if (Vector3.Distance(velocityHand, thresholdVector) >= thresholdValue &&
           Vector3.Distance(velocityObject, thresholdVector) >= thresholdValue &&
            distanceBetweenIndexAndObject <= 2.0f &&
            distanceBetweenThumbAndObject <= 2.0f)
        {
            //ExperimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.Grasped;
            return true;
        }
        else
            return false;
    }



    private void StartStimulation(string trialInteraction, string trialPlacement)
    {
        if ((trialInteraction == InteractionPour) && (trialPlacement == PlacementRight))
        {
            MeshRenderer meshStimulus = PourStimulationRight.GetComponent<MeshRenderer>();
            meshStimulus.material.color = Color.red;
            PourStimulationRight.SetActive(true);
            return;
        }
        if ((trialInteraction == InteractionPour) && (trialPlacement == PlacementLeft))
        {
            MeshRenderer meshStimulus = PourStimulationLeft.GetComponent<MeshRenderer>();
            meshStimulus.material.color = Color.red;
            PourStimulationLeft.SetActive(true);
            return;
        }
        if ((trialInteraction == InteractionPlace) && (trialPlacement == PlacementRight))
        {
            MeshRenderer meshStimulus = PlaceStimulationRight.GetComponent<MeshRenderer>();
            meshStimulus.material.color = Color.red;
            PlaceStimulationRight.SetActive(true);
            return;
        }
        if ((trialInteraction == InteractionPlace) && (trialPlacement == PlacementLeft))
        {
            MeshRenderer meshStimulus = PlaceStimulationLeft.GetComponent<MeshRenderer>();
            meshStimulus.material.color = Color.red;
            PlaceStimulationLeft.SetActive(true);
            return;
        }
    }


}
