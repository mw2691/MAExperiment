using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor0Script : MonoBehaviour
{

    private List<float> sensorDataList = new List<float>();
    public SensorData thumbData;



    // Update is called once per frame
    void Update()
    {
        //Position: TrakStar x = Unity z, TrakStar y = Unity x, TrakStar z = Unity y
        //this.transform.position = new Vector3(dataScript.Sensor0DataFloat[1], dataScript.Sensor0DataFloat[2], dataScript.Sensor0DataFloat[0]);
        //dataScript.Sensor0DataFloat.Clear();

        //thumb.GetSensorData(0);
        //sensorData = thumb.SensorDataThumbList;
        //this.transform.position = new Vector3(-sensorData[1], sensorData[2], -sensorData[0]);
        //sensorData.Clear();



        //sensorData = thumbData.GetSensorData(0);
        //this.transform.position = new Vector3(-sensorData[1], sensorData[2], -sensorData[0]);
        //thumbData.SensorDataThumbList.Clear();
        //sensorData.Clear();


        sensorDataList = thumbData.SensorDataThumbList;
        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]);
        sensorDataList.Clear();
        thumbData.SensorDataThumbList.Clear();


    }
}
