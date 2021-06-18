using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerScript : MonoBehaviour
{
    private List<float> sensorDataList = new List<float>();
    public SensorData middleData;
    public float distanceValue;


    // Update is called once per frame
    void Update()
    {
        sensorDataList = middleData.SensorDataMiddleFingerList;

        Vector3 sensorDataRightOrientation = this.transform.rotation * Vector3.right;
        Debug.Log(sensorDataRightOrientation * distanceValue);
        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]) + sensorDataRightOrientation * distanceValue;;
        sensorDataList.Clear();
        middleData.SensorDataMiddleFingerList.Clear();
    }
}
