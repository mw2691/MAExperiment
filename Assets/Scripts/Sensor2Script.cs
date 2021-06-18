using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor2Script : MonoBehaviour
{
    public Vector3 Euler1 = new Vector3(180, 0, 0);
    public Vector3 Euler2 = new Vector3(0, 0, 90);
    public Vector3 Euler3 = new Vector3(0, 90, 0);

    public Vector3 positionOffset = new Vector3(0, 0, 0);

    public SensorData palmData;
    private List<float> sensorDataList = new List<float>();



    void Update()
    {
        //if (dataScript.messageReceived)
        //{
        //    //Translation
        //    //this.transform.position = new Vector3(-dataScript.Sensor2DataFloat[1], dataScript.Sensor2DataFloat[2], -dataScript.Sensor2DataFloat[0]) + positionOffset;

        //    //Orientation
        //    //Quaternion trakStarRot = Quaternion.Euler(dataScript.Sensor2DataFloat[3], dataScript.Sensor2DataFloat[4], -dataScript.Sensor2DataFloat[5]);
        //    //Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        //    //Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        //    //Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        //    //this.transform.rotation = thirdRot;

        //    //dataScript.Sensor2DataFloat.Clear();



        //    sensorData = palmData.GetSensorData(2);
        //    this.transform.position = new Vector3(-sensorData[1], sensorData[2], -sensorData[0]);


        //    Quaternion trakStarRot = Quaternion.Euler(sensorData[3], sensorData[4], -sensorData[5]);
        //    Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        //    Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        //    Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        //    this.transform.rotation = thirdRot;

        //    palmData.SensorDataIndexList.Clear();
        //    sensorData.Clear();
        //}
        sensorDataList = palmData.SensorDataPalmList;
        Quaternion trakStarRot = Quaternion.Euler(sensorDataList[3], sensorDataList[4], -sensorDataList[5]);
        Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        this.transform.rotation = thirdRot;
        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]);
        sensorDataList.Clear();
        palmData.SensorDataPalmList.Clear();
    }
}
