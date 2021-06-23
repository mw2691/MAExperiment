using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVeloScript : MonoBehaviour
{
    public Transform cube1;
    public Transform ObjectReference;
    private Vector3 lastPositionThumb;
    private Vector3 lastPositionIndex;
    private Vector3 lastPositionObject;

    private Vector3 currentPositionThumb;
    private Vector3 currentPositionIndex;
    private Vector3 currentPositionObject;



    // Start is called before the first frame update
    void Start()
    {
        lastPositionThumb = this.transform.position;
        lastPositionIndex = cube1.position;
        lastPositionObject = ObjectReference.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPositionThumb = this.transform.position;
        currentPositionIndex = cube1.transform.position;
        currentPositionObject = ObjectReference.transform.position;

        //Call function
        if (IsBottleGraspedAndMoved())
        {
            Debug.Log("aaaaaaaaa geil");
        }

        //Do Stuff
        lastPositionThumb = currentPositionThumb;
        lastPositionIndex = currentPositionIndex;
        lastPositionObject = currentPositionObject;

    }


    public bool IsBottleGraspedAndMoved()
    {
        var thumb = new Vector3(0, 0, this.transform.position.z);
        var indexFinger = new Vector3(0, 0, cube1.transform.position.z);
        var objectTransform = new Vector3(0, 0, ObjectReference.transform.position.z);
        var distanceBetweenThumbAndObject = Vector3.Distance(thumb, objectTransform);
        var distanceBetweenIndexAndObject = Vector3.Distance(indexFinger, objectTransform);
        var velocityHand = (currentPositionThumb - lastPositionThumb) / Time.deltaTime;
        var velocityObject = (currentPositionObject - lastPositionObject) / Time.deltaTime;


        if ((velocityHand == velocityObject) &&
            velocityHand != Vector3.zero &&
            velocityObject != Vector3.zero &&
            distanceBetweenIndexAndObject <= 2.0f &&
            distanceBetweenThumbAndObject <= 2.0f)
        {
            Debug.Log("Bottle is grasped");
            return true;
        }
        else
            return false;
    }
}
