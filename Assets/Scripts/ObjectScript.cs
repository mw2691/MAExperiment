using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{

    public Vector3 Euler1 = new Vector3(180, 0, 0);
    public Vector3 Euler2 = new Vector3(0, 0, 90);
    public Vector3 Euler3 = new Vector3(0, 90, 0);

    public SensorData objectData;
    private List<float> sensorDataList = new List<float>();


    // Update is called once per frame
    void Update()
    {
        //if (dataScript.messageReceived)
        //{
        //    //TrakStar x = Unity z, TrakStar y = Unity x, TrakStar z = Unity y
        //    //this.transform.position = new Vector3(-dataScript.Sensor3DataFloat[1], dataScript.Sensor3DataFloat[2], -dataScript.Sensor3DataFloat[0]);

        //    //Quaternion trakStarRot = Quaternion.Euler(dataScript.Sensor3DataFloat[3], dataScript.Sensor3DataFloat[4], -dataScript.Sensor3DataFloat[5]);
        //    //Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        //    //Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        //    //Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        //    //this.transform.rotation = thirdRot;


        //    //dataScript.Sensor3DataFloat.Clear();
        //    sensorData = objectData.GetSensorData(3);
        //    this.transform.position = new Vector3(-sensorData[1], sensorData[2], -sensorData[0]);


        //    Quaternion trakStarRot = Quaternion.Euler(sensorData[3], sensorData[4], -sensorData[5]);
        //    Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        //    Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        //    Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        //    this.transform.rotation = thirdRot;

        //    objectData.SensorDataIndexList.Clear();
        //    sensorData.Clear();
        //}



        sensorDataList = objectData.SensorDataObjectList;
        //Debug.Log(sensorDataList.Count);
        Quaternion trakStarRot = Quaternion.Euler(sensorDataList[3], sensorDataList[4], -sensorDataList[5]);
        Quaternion firstRot = Quaternion.Euler(Euler1) * trakStarRot;
        Quaternion secondRot = Quaternion.Euler(Euler2) * firstRot;
        Quaternion thirdRot = Quaternion.Euler(Euler3) * secondRot;
        this.transform.rotation = thirdRot;
        this.transform.position = new Vector3(-sensorDataList[1], sensorDataList[2], -sensorDataList[0]);
        sensorDataList.Clear();
        objectData.SensorDataObjectList.Clear();
    }
}
