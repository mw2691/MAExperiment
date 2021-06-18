using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

public class SensorData : MonoBehaviour
{
    public UDPClient UDPClientReference;

    public List<float> SensorDataThumbList = new List<float>();
    public List<float> SensorDataIndexList = new List<float>();
    public List<float> SensorDataPalmList = new List<float>();
    public List<float> SensorDataObjectList = new List<float>();
    public List<float> SensorDataMiddleFingerList = new List<float>();
    public List<float> SensorDataRingFingerList = new List<float>();
    public List<float> SensorDataPinkyFingerList = new List<float>();

    CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-US");



    void LateUpdate()
    {
        if (UDPClientReference.messageReceived)
        {
            for (int i = 1; i < 7; i++)
            {
                var valuesSensor0Data = float.Parse(UDPClientReference.tokens[i], Culture);
                SensorDataThumbList.Add(valuesSensor0Data);
            }

            for (int i = 8; i < 14; i++)
            {
                var valuesSensor1Data = float.Parse(UDPClientReference.tokens[i], Culture);
                SensorDataIndexList.Add(valuesSensor1Data);
                SensorDataMiddleFingerList.Add(valuesSensor1Data);
                SensorDataRingFingerList.Add(valuesSensor1Data);
                SensorDataPinkyFingerList.Add(valuesSensor1Data);
            }


            for (int i = 15; i < 21; i++)
            {
                var valuesSensor2Data = float.Parse(UDPClientReference.tokens[i], Culture);
                SensorDataPalmList.Add(valuesSensor2Data);
            }

            for (int i = 22; i <= 27; i++)
            {
                var valuesSensor3Data = float.Parse(UDPClientReference.tokens[i], Culture);
                SensorDataObjectList.Add(valuesSensor3Data);
            }

        }   
    }
    /*
    void Update()
    {
        SensorDataThumbList.Clear();
        SensorDataIndexList.Clear();
        SensorDataMiddleFingerList.Clear();
        SensorDataPalmList.Clear();
        SensorDataObjectList.Clear();
    }
    */
}
