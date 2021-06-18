using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;

public class UDPClient : MonoBehaviour {

    private static string DEFAULT_MESSAGE_SEPARATOR = ":";
    private static string DEFAULT_NUMBER_SEPARATOR = ";";

    public List<float> Sensor0DataFloat = new List<float>();
    public List<float> Sensor1DataFloat = new List<float>();
    public List<float> Sensor2DataFloat = new List<float>();
    public List<float> Sensor3DataFloat = new List<float>();

    private Thread messageThread;
    private UdpClient client;

    public int port = 4445;

    private string udpMessage;
    
    public bool messageReceived;

    public string[] tokens;

    // Use this for initialization
    void Start () {
        Application.runInBackground = true;

        this.client = new UdpClient();
        this.client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        this.client.Client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.port));

        this.messageThread = new Thread(new ThreadStart(this.ReceiveData));
        this.messageThread.IsBackground = true;
        this.messageThread.Start();
	}

    private void ReceiveData()
    {
        //this.client = new UdpClient("127.0.0.1", this.port);
        //this.client = new UdpClient(this.port);
        
        while (true)
        {
            try
            {
                Debug.Log("waiting for input at " + this.client.ToString() + "...");
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, this.port);
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.port);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = this.client.Receive(ref endPoint);
                this.udpMessage = Encoding.ASCII.GetString(data);

                this.messageReceived = true;
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(exc.ToString());
            }
        }
    }


    // Update is called once per frame
    void Update () {
		if (this.messageReceived)
        {
            //UnityEngine.Debug.Log(this.udpMessage);
            this.tokens = udpMessage.Split(new string[] { DEFAULT_MESSAGE_SEPARATOR, DEFAULT_NUMBER_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            string[] sensorData = udpMessage.Split(new string[] { DEFAULT_MESSAGE_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            string[] numberTokens = sensorData[0].Split(new string[] { DEFAULT_NUMBER_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-US");


            //for (int i = 0; i < 6; i++)
            //{
            //    var valuesSensor0Data = float.Parse(numberTokens[i], Culture);
            //    Sensor0DataFloat.Add(valuesSensor0Data);
            //}

            //numberTokens = sensorData[1].Split(new string[] { DEFAULT_NUMBER_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 6; i < 12; i++)
            //{
            //    var valuesSensor1Data = float.Parse(tokens[i], Culture);
            //    Sensor1DataFloat.Add(valuesSensor1Data);
            //}

            //numberTokens = sensorData[2].Split(new string[] { DEFAULT_NUMBER_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 12; i < 18; i++)
            //{
            //    var valuesSensor2Data = float.Parse(tokens[i], Culture);
            //    Sensor2DataFloat.Add(valuesSensor2Data);
            //}

            //for (int i = 18; i < 24; i++)
            //{
            //    var valuesSensor3Data = float.Parse(tokens[i], Culture);
            //    Sensor3DataFloat.Add(valuesSensor3Data);
            //}
        }
	}

}
