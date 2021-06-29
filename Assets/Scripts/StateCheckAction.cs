using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateCheckAction : MonoBehaviour, IState
{
    public bool finished { get; set; }
    public IState nextState { get; set; }
    public GameObject StateInit;
    public GameObject StateTraining;

    public StateTraining stateTrainingScript;
    public ExperimentController experimentControllerScript;
    public StateStartExperiment stateStartExperimentScript;

    private Trial currentTrialConditions;

    public GameObject PlaceStimulationLeft;
    public GameObject PlaceStimulationRight;
    public GameObject PalmReference;
    public GameObject ObjectReference;
    public GameObject BottleReference;
    public GameObject IndexFingerReference;
    public GameObject ThumbFingerReference;
    public GameObject CupRight;
    public GameObject CupLeft;
    public GameObject PlaceRightEmptyObject;
    public GameObject PlaceLeftEmptyObject;

    public bool ExperimentalTrialSuccesful;
    //public bool ExperimentalTrialNOTSuccesful;

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

        CheckExperiment(currentTrialConditions);
        //var velocityHand = (currentPositionPalm - lastPositionPalm) / Time.deltaTime;
        //var velocityObject = (currentPositionObject - lastPositionObject) / Time.deltaTime;
        //Debug.Log("veloHand: " + velocityHand + ",,,,, veloObject: " + velocityObject);
        //Debug.Log("Distance between velos: " + Vector3.Distance(velocityHand, velocityObject));
        lastPositionPalm = currentPositionPalm;
        lastPositionObject = currentPositionObject;
        
        

        stateStartExperimentScript.TrialDurationTimeStamp += Time.deltaTime;

        //StartCoroutine(TrialTimeOutAndBottleIsNotGrasped());
        //StartCoroutine(TrialTimeOutAndBottleIsGrasped());


        if (Keyboard.current[Key.O].isPressed)
        {
            ExperimentalTrialSuccesful = true;

        }
        //if (Keyboard.current[Key.P].isPressed)
        //{
        //    ExperimentalTrialNOTSuccesful = true;
        //}


        if (stateTrainingScript.isStateTraining)
        {
            nextState = StateTraining.GetComponent<IState>();
        }

        ////Ask for to exit training
        //if (Keyboard.current[Key.Digit1].isPressed)
        //{
        //    experimentControllerScript.trialOrderLineCounter = 1;
        //    stateTrainingScript.isStateTraining = false;
        //}


        //Debug.Log("Execute StateCheckAction");
        if (Keyboard.current[Key.G].isPressed)
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
        //ExperimentalTrialNOTSuccesful = false;
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
        //stateStartExperimentScript.TrialTimeOut = true;
        //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
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
        //stateStartExperimentScript.TrialTimeOut = true;
        //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialNotSuccesful;
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
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitUntil(() => CheckPourRight());
        yield return new WaitForSeconds(2.5f);
        this.finished = true;
    }

    private IEnumerator PourLeft()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitUntil(() => CheckPourLeft());
        yield return new WaitForSeconds(2.5f);
        this.finished = true;
    }


    private IEnumerator PlaceRight()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitUntil(() => CheckPlaceRight());
        yield return new WaitForSeconds(2.5f);
        this.finished = true;
    }

    private IEnumerator PlaceLeft()
    {
        while (!IsBottleGraspedAndMoved())
        {
            yield return null;
        }
        yield return new WaitUntil(() => CheckPlaceLeft());
        yield return new WaitForSeconds(2.5f);
        this.finished = true;
    }


    private bool CheckPourRight()
    {
        if ((Vector3.Distance(ObjectReference.transform.position, CupRight.transform.position) >= 0.0f &&
            Vector3.Distance(ObjectReference.transform.position, CupRight.transform.position) <= 4.0f))
        {
            ExperimentalTrialSuccesful = true;
            //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            return true;
        }
        else
            return false;
    }

    private bool CheckPourLeft()
    {
        if ((Vector3.Distance(ObjectReference.transform.position, CupLeft.transform.position) >= 0.0f &&
            Vector3.Distance(ObjectReference.transform.position, CupLeft.transform.position) <= 4.0f))
        {
            ExperimentalTrialSuccesful = true;
            //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            return true;
        }
        else
            return false;
    }

    private bool CheckPlaceRight()
    {
        var placeRightCollider = PlaceRightEmptyObject.GetComponent<BoxCollider>();
        if (placeRightCollider.bounds.Contains(BottleReference.transform.position))
        {
            ExperimentalTrialSuccesful = true;
            //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckPlaceLeft()
    {
        var placeLeftCollider = PlaceLeftEmptyObject.GetComponent<BoxCollider>();
        if (placeLeftCollider.bounds.Contains(BottleReference.transform.position))
        {
            ExperimentalTrialSuccesful = true;
            //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.TrialActionEnded;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsBottleGraspedAndMoved()
    {
        var thumb = new Vector3(0, 0, ThumbFingerReference.transform.position.z);
        var indexFinger = new Vector3(0, 0, IndexFingerReference.transform.position.z);
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
            //experimentControllerScript.currentAnnotationState = ExperimentController.AnnotationStates.Grasped;
            return true;
        }
        else
            return false;
    }
}
