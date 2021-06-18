using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFingerScript : MonoBehaviour
{
    private List<float> sensorDataList = new List<float>();
    public SensorData ringData;
    public float distanceValue;


    // Update is called once per frame
    void Update()
    {
        sensorDataList = ringData.SensorDataRingFingerList;
        Vector3 sensorDataRightOrientation = this.transform.rotation * Vector3.right;
        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]) + sensorDataRightOrientation * distanceValue;
        sensorDataList.Clear();
        ringData.SensorDataRingFingerList.Clear();
    }
}
