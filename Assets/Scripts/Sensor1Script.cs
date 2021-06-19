using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor1Script : MonoBehaviour
{
    private List<float> sensorDataList = new List<float>();
    public SensorData indexData;



    // Update is called once per frame
    void Update()
    {
        //if (dataScript.messageReceived)
        //{
        //    //TrakStar x = Unity z, TrakStar y = Unity x, TrakStar z = Unity y
        //    //this.transform.position = new Vector3(-dataScript.Sensor1DataFloat[1], dataScript.Sensor1DataFloat[2], -dataScript.Sensor1DataFloat[0]);
        //    //dataScript.Sensor1DataFloat.Clear();

        //    sensorData = indexData.GetSensorData(1);
        //    this.transform.position = new Vector3(-sensorData[1], sensorData[2], -sensorData[0]);
        //    indexData.SensorDataIndexList.Clear();
        //    sensorData.Clear();
        //}

        sensorDataList = indexData.SensorDataIndexList;

        //foreach (var item in sensorData)
        //{
        //    Debug.Log("i bims: " + item);
        //}

        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]);
        sensorDataList.Clear();
        indexData.SensorDataIndexList.Clear();
    }
}
