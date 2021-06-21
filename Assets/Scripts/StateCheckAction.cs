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

    public float BottleRotationRangeMin = 60.0f;
    public float BottleRotationRangeMax = 110.0f;


    public void Enter()
    {
        finished = false;
        nextState = StateInit.GetComponent<IState>();
        //Debug.Log("Enter StateCheckAction");
        currentTrialConditions = experimentControllerScript.currentTrial;

        palmLastPosition = PalmReference.transform.position;
        objectLastPosition = ObjectReference.transform.position;
    }
    
    public void Execute()
    {

        CheckExperiment(currentTrialConditions);


        #region Check bottle grasp
        this.palmLastPosition = PalmReference.transform.position;
        this.objectLastPosition = ObjectReference.transform.position;
        stateStartExperimentScript.TrialDurationTimeStamp += Time.deltaTime;

        StartCoroutine(TrialTimeOutAndBottleIsNotGrasped());
        StartCoroutine(TrialTimeOutAndBottleIsGrasped());
        #endregion


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
            !IsBottleGrasped(palmLastPosition, objectLastPosition)))
        {
            yield return null;
        }
        stateStartExperimentScript.TrialTimeOut = true;
        Debug.Log("Bitte schneller die Flasche greifen");
        yield return new WaitForSeconds(0.5f);
        this.finished = true;
    }

    public IEnumerator TrialTimeOutAndBottleIsGrasped()
    {
        while (!(stateStartExperimentScript.TrialDurationTimeStamp >= stateStartExperimentScript.TrialMaxDuration &&
            IsBottleGrasped(palmLastPosition, objectLastPosition)))
        {
            yield return null;
        }
        stateStartExperimentScript.TrialTimeOut = true;
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

    #region coroutines
    private IEnumerator PourRight()
    {
        while (!CheckPourRight())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Check PourRight good");
        ExperimentalTrialSuccesful = true;
        this.finished = true;
    }


    private IEnumerator PourLeft()
    {
        while (!CheckPourLeft())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Check PourLeft good");
        ExperimentalTrialSuccesful = true;
        this.finished = true;
    }


    private IEnumerator PlaceRight()
    {
        while (!IsBottleGrasped(palmLastPosition, objectLastPosition))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(ObjectReference.transform.position, PlaceStimulationRight.transform.position) <= 0.2)
        {
            ExperimentalTrialSuccesful = true;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
        else
        {
            ExperimentalTrialNOTSuccesful = true;
            yield return new WaitForSeconds(0.5f);
            this.finished = true;
        }
    }

    //private IEnumerator PlaceRight()
    //{
    //    while (!CheckPlaceRight())
    //    {
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(0.5f);
    //    Debug.Log("Check PlaceRight good");
    //    ExperimentalTrialSuccesful = true;
    //    this.finished = true;
    //}

    private IEnumerator PlaceLeft()
    {
        while (!CheckPlaceLeft())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Check PlaceLeft good");
        ExperimentalTrialSuccesful = true;
        this.finished = true;
    }
    #endregion

    #region coroutines condition methods

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

    private bool CheckPlaceRight()
    {
        if (Vector3.Distance(PlaceStimulationRight.transform.position, ObjectReference.transform.position) <= 0.2)
        {
            return true;
        }
        else
            return false;
    }

    private bool CheckPlaceLeft()
    {
        if (Vector3.Distance(PlaceStimulationLeft.transform.position, ObjectReference.transform.position) <= 0.2)
        {
            return true;
        }
        else
            return false;
    }
    #endregion

    private bool IsBottleGrasped(Vector3 lastPositionPalm, Vector3 lastPositionObject)
    {
        if ((Vector3.Distance(lastPositionPalm, lastPositionObject) <= 0.4f) &&
    Vector3.Distance(IndexFingerReference.transform.position, ObjectReference.transform.position) <= 0.4f &&
    Vector3.Distance(ThumbFingerReference.transform.position, ObjectReference.transform.position) <= 0.4f)
        {
            Debug.Log("Bottle is grasped");
            return true;
        }
        else
            return false;
    }

}
