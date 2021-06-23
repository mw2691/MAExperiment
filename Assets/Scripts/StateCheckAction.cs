using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCheckAction : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;

    public StateTraining stateTrainingScript;
    public ExperimentController experimentControllerScript;
    public StateStartExperiment stateStartExperimentScript;

    private Trial currentTrialConditions;

    public GameObject PlaceStimulationLeft;
    public GameObject PlaceStimulationRight;

    public GameObject PalmReference;
    public GameObject ObjectReference;
    public GameObject IndexFingerReference;
    public GameObject ThumbFingerReference;
    private Vector3 palmLastPosition;
    private Vector3 objectLastPosition;
    public GameObject CupRight;
    public GameObject CupLeft;

    public bool ExperimentalTrialSuccesful;
    public bool ExperimentalTrialNOTSuccesful;

    private float timeStamp = 0.0f;
    public float CheckDuration = 10f;

    private const string InteractionPour = "Pour";
    private const string InteractionPlace = "Place";
    private const string PlacementLeft = "Left";
    private const string PlacementRight = "Right";

    public float BottleRotationRangeMin = 45.0f;
    public float BottleRotationRangeMax = 110.0f;

    private Vector3 lastPositionPalm;
    private Vector3 lastPositionObject;
    private Vector3 currentPositionPalm;
    private Vector3 currentPositionObject;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateCheckAction");
        currentTrialConditions = experimentControllerScript.currentTrial;

        lastPositionPalm = PalmReference.transform.position;
        lastPositionObject = ObjectReference.transform.position;
    }
    
    public void Execute()
    {
        currentPositionPalm = PalmReference.transform.position;
        currentPositionObject = ObjectReference.transform.position;
        IsBottleGraspedAndMoved();
        lastPositionPalm = currentPositionPalm;
        lastPositionObject = currentPositionObject;
        

        this.palmLastPosition = PalmReference.transform.position;
        this.objectLastPosition = ObjectReference.transform.position;

        



        CheckExperiment(currentTrialConditions);

        stateStartExperimentScript.TrialDurationTimeStamp += Time.deltaTime;

        StartCoroutine(TrialTimeOutAndBottleIsNotGrasped());
        StartCoroutine(TrialTimeOutAndBottleIsGrasped());


        if (Input.GetKey(KeyCode.O))
        {
            ExperimentalTrialSuccesful = true;

        }
        if (Input.GetKey(KeyCode.P))
        {
            ExperimentalTrialNOTSuccesful = true;
        }



        //Ask for to exit training
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            experimentControllerScript.trialOrderLineCounter = 1;
            stateTrainingScript.isStateTraining = false;
        }


        //Debug.Log("Execute StateCheckAction");
        if (Input.GetKeyDown(KeyCode.G))
        {
            finished = true;
        }
        if (finished)
        {
            experimentControllerScript.trialOrderLineCounter++;
            Exit();
        }
    }

    public void Exit()
    {
        ExperimentalTrialSuccesful = false;
        ExperimentalTrialNOTSuccesful = false;
        experimentControllerScript.ResetBools();
        this.finished = true;
        //Debug.Log("Exit StateCheckAction");
        //nextState.Enter();
    }



    public IEnumerator TrialTimeOutAndBottleIsNotGrasped()
    {
        while (!(stateStartExperimentScript.TrialDurationTimeStamp >= stateStartExperimentScript.TrialMaxDuration &&
            !IsBottleGraspedAndMoved()))
        {
            yield return null;
        }
        stateStartExperimentScript.TrialTimeOut = true;
        experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
        Debug.Log("Bitte schneller die Flasche greifen");
        yield return new WaitForSeconds(0.5f);
        this.finished = true;
    }

    public IEnumerator TrialTimeOutAndBottleIsGrasped()
    {
        while (!(stateStartExperimentScript.TrialDurationTimeStamp >= stateStartExperimentScript.TrialMaxDuration &&
            IsBottleGraspedAndMoved()))
        {
            yield return null;
        }
        stateStartExperimentScript.TrialTimeOut = true;
        experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
        Debug.Log("Bitte schneller die Aktion ausführen");
        yield return new WaitForSeconds(0.5f);
        this.finished = true;
    }




    public void CheckExperiment(Trial trialInfo)
    {
        if ((trialInfo.Interaction == InteractionPour) && (trialInfo.InteractionPlacement == PlacementRight))
        {
            StartCoroutine(PourRight());
        }
        if ((trialInfo.Interaction == InteractionPour) && (trialInfo.InteractionPlacement == PlacementLeft))
        {
            StartCoroutine(PourLeft());
        }
        if ((trialInfo.Interaction == InteractionPlace) && (trialInfo.InteractionPlacement == PlacementRight))
        {
            StartCoroutine(PlaceRight());
        }
        if ((trialInfo.Interaction == InteractionPlace) && (trialInfo.InteractionPlacement == PlacementLeft))
        {
            StartCoroutine(PlaceLeft());
        }
    }


    private IEnumerator PourRight()
    {
        while (!IsBottleRotated())
        {
            if (IsBottleGraspedAndMoved())
            {
                experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.Grasped;
            }
            yield return null;
        }

        if (CheckPourRight())
        {
            ExperimentalTrialSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
        else
        {
            ExperimentalTrialNOTSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
    }

    private IEnumerator PourLeft()
    {
        while (!IsBottleRotated())
        {
            if (IsBottleGraspedAndMoved())
            {
                experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.Grasped;
            }
            yield return null;
        }

        if (CheckPourLeft())
        {
            ExperimentalTrialSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
        else
        {
            ExperimentalTrialNOTSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
    }


    private IEnumerator PlaceRight()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(ObjectReference.transform.position, PlaceStimulationRight.transform.position) <= 0.2)
        {
            ExperimentalTrialSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
        else
        {
            ExperimentalTrialNOTSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
    }

    private IEnumerator PlaceLeft()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(ObjectReference.transform.position, PlaceStimulationLeft.transform.position) <= 0.2)
        {
            ExperimentalTrialSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
        else
        {
            ExperimentalTrialNOTSuccesful = true;
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
    }


    private bool CheckPourRight()
    {
        if ((ObjectReference.transform.eulerAngles.z >= BottleRotationRangeMin && ObjectReference.transform.eulerAngles.z <= BottleRotationRangeMax) &&
            (Vector3.Distance(ObjectReference.transform.position, CupRight.transform.position) >= 0.0f &&
            Vector3.Distance(ObjectReference.transform.position, CupRight.transform.position) <= 6.0f))
        {
            return true;
        }
        else
            return false;
    }

    private bool CheckPourLeft()
    {
        if ((ObjectReference.transform.eulerAngles.z >= BottleRotationRangeMin && ObjectReference.transform.eulerAngles.z <= BottleRotationRangeMax) &&
            (Vector3.Distance(ObjectReference.transform.position, CupLeft.transform.position) >= 0.0f &&
            Vector3.Distance(ObjectReference.transform.position, CupLeft.transform.position) <= 6.0f))
        {
            return true;
        }
        else
            return false;
    }


    public bool IsBottleGraspedAndMoved()
    {
        var thumb = new Vector3(0, 0, ThumbFingerReference.transform.position.z);
        var indexFinger = new Vector3(0, 0, IndexFingerReference.transform.position.z);
        var objectTransform = new Vector3(0, 0, ObjectReference.transform.position.z);
        var distanceBetweenThumbAndObject = Vector3.Distance(thumb, objectTransform);
        var distanceBetweenIndexAndObject = Vector3.Distance(indexFinger, objectTransform);
        var velocityHand = (currentPositionPalm - lastPositionPalm) / Time.deltaTime;
        var velocityObject = (currentPositionPalm - lastPositionPalm) / Time.deltaTime;

        if ((velocityHand == velocityObject) &&
            velocityHand != Vector3.zero &&
            velocityObject != Vector3.zero &&
            distanceBetweenIndexAndObject <= 2.0f &&
            distanceBetweenThumbAndObject <= 2.0f)
        {
            experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.Grasped;
            Debug.Log("Bottle is grasped");
            return true;
        }
        else
            return false;
    }


    private bool IsBottleRotated()
    {
        if (ObjectReference.transform.eulerAngles.z >= BottleRotationRangeMin && ObjectReference.transform.eulerAngles.z <= BottleRotationRangeMax)
        {
            return true;
        }
        else
            return false;
    }

}
