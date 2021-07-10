using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Globalization;

public class LogData : MonoBehaviour
{
    public string ParticipantID;
    public string TrialID;
    private string fileDirectory;
    public int fileRowCounter;
    public int trialOrderLineCounter;
    public List<string> AllPositionDataFromTrial;
    public List<Vector3> AllResultRows;
    private string xCoor;
    private string yCoor;
    private string zCoor;
    private float xParsed;
    private float yParsed;
    private float zParsed;
    public float ySpecialParsed;
    public bool startBool;
    public bool waitBool;
    public int trialCounter;

    public string strResultFileName;

    public List<string> AllRowsFromFile;
    public List<string> AllPositionData;

    public bool getPositionDataIsFinished;

    public MeshRenderer PositionStartMesh;
    public MeshRenderer ReachForBottleMeshRenderer;
    public MeshRenderer BottleMeshRenderer;
    public MeshRenderer StimulusLeftMesh;
    public MeshRenderer StimulusRightMesh;
    public MeshRenderer GraspedPositionMesh;
    public GameObject BottleReference;
    public GameObject ReachForBottleReference;

    public bool isReachedForBottleFinished;
    public bool isSettingsForTrialsIsFinished;




    void Start()
    {
        fileRowCounter = 1;
        trialOrderLineCounter = 1;
        trialCounter = 1;
        //get resultfile path
        // fileDirectory = /Users/markuswieland/Documents/MA/ResultFileAnnotation/ResultFiles/01
        fileDirectory = FileWriteManagement.GetResultFileDirectory(ParticipantID);

        


    }

    void Update()
    {
        if (!isSettingsForTrialsIsFinished)
        {
            if (!getPositionDataIsFinished)
            {
                //Debug.Log("In !isSettingsForTrialsIsFinished and !getPositionDataIsFinished");
                fileRowCounter = 1;
                AllRowsFromFile = ReturnAllRowsFromFile();
                strResultFileName = GetResultFileName();
                AllPositionData = ReturnAllPositionDataFromTrial(AllRowsFromFile);
                waitBool = true;
                startBool = false;
                getPositionDataIsFinished = true;
                isSettingsForTrialsIsFinished = true;
            }
        }

        if (waitBool)
        {
            if (!startBool)
            {
                //Debug.Log("FileRowCounter: " + fileRowCounter + ", Count: " + AllRowsFromFile.Count);

                var resultVector = PositionDataFromResultFile(AllPositionData[fileRowCounter]);
                this.transform.position = resultVector;

                if (PositionStartMesh.bounds.Contains(this.transform.position))
                {
                    //Debug.Log("Hand in StartPosition");
                    ReachForBottleReference.SetActive(true);
                    ReachForBottleMeshRenderer.enabled = true;
                    isReachedForBottleFinished = false;
                    WriteIntoResultFile("Start");
                }

                if (ReachForBottleMeshRenderer.bounds.Contains(this.transform.position) && !isReachedForBottleFinished)
                {
                    Debug.Log("Hand reaches for bottle");
                    WriteIntoResultFile("ReachForBottle");
                }

                if (GraspedPositionMesh.bounds.Contains(this.transform.position))
                {
                    //Debug.Log("Grasped in GraspPosition");
                    ReachForBottleReference.SetActive(false);
                    ReachForBottleMeshRenderer.enabled = false;
                    isReachedForBottleFinished = true;
                    WriteIntoResultFile("Grasped");
                }

                if ((StimulusLeftMesh.bounds.Contains(this.transform.position)) | (StimulusRightMesh.bounds.Contains(this.transform.position)))
                {
                    //Debug.Log("Grasped in Left or Right");
                    ReachForBottleReference.SetActive(false);
                    ReachForBottleMeshRenderer.enabled = false;
                    isReachedForBottleFinished = true;
                    WriteIntoResultFile("Grasped");
                }

                if (isReachedForBottleFinished && !(PositionStartMesh.bounds.Contains(this.transform.position)))
                {
                    WriteIntoResultFile("Grasped");
                }


                fileRowCounter += 1;

                if (fileRowCounter == AllRowsFromFile.Count)
                {
                    //Debug.Log("In fileRowCounter == AllRowsFromFile.Count ");
                    trialOrderLineCounter += 1;
                    trialCounter += 1;
                    isSettingsForTrialsIsFinished = false;
                    getPositionDataIsFinished = false;
                    waitBool = false;
                    startBool = true;
                    fileRowCounter = 1;
                    //EditorApplication.isPlaying = false;

                }

                if (trialOrderLineCounter > FileWriteManagement.GetAllResultFiles(ParticipantID).Count)
                {
                    EditorApplication.isPlaying = false;

                }

            }
        }
    }

    private int IndexOfNth(string str, char c, int n)
    {
        int remaining = n;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == c)
            {
                remaining--;
                if (remaining == 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }


    private List<string> ReturnAllRowsFromFile()
    {
        List<string> newList = new List<string>();
        var fileName = GetResultFileName();
        //Debug.Log("resultfileName: " + resultFileName);
        string[] AllRowsFromFile = File.ReadAllLines(fileName);
        foreach (var item in AllRowsFromFile)
        {
            newList.Add(item);
        }
        return newList;
    }


    private string GetResultFileName()
    {
        var trialOrderLine = FileWriteManagement.GetTrialOrderLine(ParticipantID, trialOrderLineCounter);
        string[] subs = trialOrderLine.Split('\t');
        string soa = subs[0].Trim();
        string interaction = subs[1].Trim();
        string placement = subs[2].Trim();
        var strWithConditions = string.Join("_", soa, interaction, placement);
        var fileRowCounterStr = trialCounter.ToString();
        var resultFileName = fileDirectory + "/" + fileRowCounterStr + "_" + ParticipantID + "_" + strWithConditions + ".log";
        return resultFileName;
    }

    private List<string> ReturnAllPositionDataFromTrial(List<string> AllRows) {
        List<string> newList = new List<string>();
        foreach (var line in AllRows)
        {
            string singleRow = line;
            string[] singleRowSplitted = singleRow.Split('\t');
            string handPositionData = singleRowSplitted[2];
            newList.Add(handPositionData);
        }

        return newList;
    }

    private Vector3 PositionDataFromResultFile(string handposition)
    {
        var secondComma = IndexOfNth(handposition, ',', 2);
        var fourthComma = IndexOfNth(handposition, ',', 4);
        var sixthComma = IndexOfNth(handposition, ',', 6);

        xCoor = handposition.Substring(0, secondComma);
        yCoor = handposition.Substring(secondComma + 1, fourthComma - secondComma - 1);
        //zCoor = handposition.Substring(fourthComma + 1, sixthComma - fourthComma - 1);


        if (!((sixthComma - fourthComma) < 0))
        {
            zCoor = handposition.Substring(fourthComma + 1, sixthComma - fourthComma - 1);
        }

        xCoor = xCoor.Replace(",", ".");
        yCoor = yCoor.Replace(",", ".");
        zCoor = zCoor.Replace(",", ".");


        xParsed = float.Parse(xCoor);
        zParsed = float.Parse(zCoor);

        //if (float.TryParse(xCoor, out xParsed))
        //{

        //}


        if (float.TryParse(yCoor, out yParsed))
        {
            ySpecialParsed = yParsed;
        }

        //if (float.TryParse(zCoor, out zParsed))
        //{

        //}

        Vector3 result = new Vector3(xParsed, ySpecialParsed, zParsed);
        return result;
    }

    private string FormatPositionData(string handposition)
    {
        var secondComma = IndexOfNth(handposition, ',', 2);
        var fourthComma = IndexOfNth(handposition, ',', 4);
        var sixthComma = IndexOfNth(handposition, ',', 6);

        xCoor = handposition.Substring(0, secondComma);
        yCoor = handposition.Substring(secondComma + 1, fourthComma - secondComma - 1);
        //zCoor = handposition.Substring(fourthComma + 1, sixthComma - fourthComma - 1);


        if (!((sixthComma - fourthComma) < 0))
        {
            zCoor = handposition.Substring(fourthComma + 1, sixthComma - fourthComma - 1);
        }

        xCoor = xCoor.Replace(",", ".");
        yCoor = yCoor.Replace(",", ".");
        zCoor = zCoor.Replace(",", ".");

        return xCoor + ":" + yCoor + ":" + zCoor;

    }


    private void WriteIntoResultFile(string state)
    {
        var currentResultLine = AllRowsFromFile[fileRowCounter];
        string[] singleRowSplitted = currentResultLine.Split('\t');
        singleRowSplitted[5] = state;
        var thumbPositionData = singleRowSplitted[0];
        var indexPositionData = singleRowSplitted[1];
        var palmPositionData = singleRowSplitted[2];
        var objectPositionData = singleRowSplitted[3];
        var gazePositionData = singleRowSplitted[4];

        var thumbFormattedString = FormatPositionData(thumbPositionData);
        var indexFormattedString = FormatPositionData(indexPositionData);
        var palmFormattedString = FormatPositionData(palmPositionData);
        var objectFormattedString = FormatPositionData(objectPositionData);
        var gazeFormattedString = FormatPositionData(gazePositionData);

        singleRowSplitted[0] = thumbFormattedString;
        singleRowSplitted[1] = indexFormattedString;
        singleRowSplitted[2] = palmFormattedString;
        singleRowSplitted[3] = objectFormattedString;
        singleRowSplitted[4] = gazeFormattedString;

        var newLine = string.Join("\t", singleRowSplitted);
        var fullPath = strResultFileName;

        string[] arrline = File.ReadAllLines(fullPath);
        arrline[fileRowCounter] = newLine;
        File.WriteAllLines(fullPath, arrline);
    }

}
