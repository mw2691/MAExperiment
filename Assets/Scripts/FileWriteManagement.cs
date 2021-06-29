using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class FileWriteManagement
{
    static string trialOrderFolderName = "TrialOrderFiles";
    static string trialOrderFilePath = Path.Combine(Directory.GetCurrentDirectory(), trialOrderFolderName);

    public static string GetResultFileDirectory(string participantID)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), "ResultFiles");
        return currentPath + "/" + participantID;
    }


    public static bool CheckExistingFile(string fileName)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), "ResultFiles");
        string[] subs = fileName.Split('_');
        currentPath += "/" + subs[0] + "/" + fileName;

        if (File.Exists(currentPath))
        {
            Debug.Log("file is already created");
            return true;
        }
        else
        {
            Debug.Log("file not there");
            return false;
        }
    }

    public static void CreateDirectory(string participantID)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory());
        currentPath = currentPath + "/" +  "ResultFiles" + "/" + participantID;
        if (!Directory.Exists(currentPath))
            Directory.CreateDirectory(currentPath);
    }


    public static void CreateFile(string fileName)

    {
        using (FileStream fs = File.Create(fileName)) ;
    }

    public static void WriteFile(string data, string fileName)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        using (StreamWriter writeText = new StreamWriter(currentPath))
        {
            writeText.Write(data);
        }
    }

    public static void WriteFile(string data, string fileName, bool appendFile)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        using (StreamWriter writeText = new StreamWriter(currentPath, appendFile))
        {
            writeText.Write(data);
        }
    }
    
    public static string GetTrialOrderFileName(string participantID)
    {
        string[] txtFiles = Directory.GetFiles(trialOrderFilePath, "*.txt").Select(Path.GetFileName).ToArray();
        foreach (var file in txtFiles)
        {
            if (file.StartsWith(participantID))
            {
                return file;
            }
            else
                continue;
        }
        return "Participant ID is not valid";
    }

    public static string GetTrialOrderLine(string participantID, int lineNumber)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);
        return arrLine[lineNumber];

    }

    public static void WriteProgressInTrialOrderFile(string participantID, int lineToEdit)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        var currentLine = GetTrialOrderLine(participantID, lineToEdit);
        string[] subs = currentLine.Split(' ');
        subs[subs.Length - 1] = "1";
        var newLine = string.Join(" ", subs);

        string[] arrLine = File.ReadAllLines(fullPath);
        arrLine[lineToEdit] = newLine;
        File.WriteAllLines(fullPath, arrLine);
    }

    public static void WriteProgressInTrialOrderFile(string participantID, int lineToEdit, string errorCode)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        var currentLine = GetTrialOrderLine(participantID, lineToEdit);
        string[] subs = currentLine.Split(' ');
        subs[subs.Length - 1] = errorCode;
        var newLine = string.Join(" ", subs);

        string[] arrLine = File.ReadAllLines(fullPath);
        arrLine[lineToEdit] = newLine;
        File.WriteAllLines(fullPath, arrLine);
    }


    public static Tuple<int, bool> GetRowNumberOfProgressInTrialOrderFile(string participantID)
    {
        List<int> rowNumber = new List<int>();
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);
        for (int i = 0; i < arrLine.Length; i++)
        {
            string[] subs = arrLine[i].Split(' ');
            if(subs[subs.Length - 1] == "1")
            {
                rowNumber.Add(i);
            }
        }
        if (!(rowNumber.Count == 0))
            return Tuple.Create(rowNumber[rowNumber.Count - 1], true);
        else
        {
            rowNumber.Add(0);
            return Tuple.Create(rowNumber[0], false);
        }
    }

    public static List<string> GetAllErrorTrials(string participantID)
    {
        List<string> allErrorTrials = new List<string>();
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);
        for (int i = 0; i < arrLine.Length; i++)
        {
            string[] subs = arrLine[i].Split(' ');
            if (subs[subs.Length - 1] == "0")
            {
                allErrorTrials.Add(arrLine[i]);
            }
        }
        return allErrorTrials;
    }

    public static void DeleteSingleErrorTrial(string participantID, int trialRowCounter)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        var trialOrderLine = GetTrialOrderLine(participantID, trialRowCounter);
        string[] arrLine = File.ReadAllLines(fullPath);
        var allLines = arrLine.ToList<string>();
        var subs = trialOrderLine.Split(' ');
        if (subs[subs.Length - 1] == "1")
        {
            allLines.RemoveAt(trialRowCounter);
            File.WriteAllLines(fullPath, allLines.ToArray());
        }
    }

    public static string GetSingleErrorTrial(string participantID, int trialRowCounter)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string newline = "";
        var trialOrderLine = GetTrialOrderLine(participantID, trialRowCounter);
        var subs = trialOrderLine.Split(' ');
        if (subs[subs.Length - 1] == "1")
        {
            subs[subs.Length - 1] = "0";
            newline = string.Join(" ", subs);
            //File.AppendAllText(fullPath, newLine);
        }
        return newline;
    }

    public static void AppendErrorTrialsToTrialOrderFile(string participantID)
    {
        var allErrorTrialsString = GetAllErrorTrials(participantID);
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        
        for (int i = 0; i < allErrorTrialsString.Count; i++)
        {
            File.AppendAllText(fullPath, allErrorTrialsString[i] + Environment.NewLine);
        }
    }


    public static bool CheckAllTrialsAreEqualToTrialOrderCounter(string participantID, int lineNumber)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);

        if (arrLine.Length == lineNumber)
        {
            return true;
        }
        else
            return false;
    }


    public static int CountRowsinTrialOrderFile(string participantID)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);

        return arrLine.Length;
    }


    public static bool CheckForRemainingErrorTrials(string participantID, int lineNumber)
    {
        bool areThereErrorTrials = false;
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);

        for (int i = lineNumber; i < arrLine.Length; i++)
        {
            string[] subs = arrLine[i].Split(' ');
            if (subs[subs.Length - 1] == "0")
            {
                areThereErrorTrials = true;
            }
        }
        if (areThereErrorTrials)
            return true;
        else
            return false;
    }


    public static List<string> GetRemainingErrorTrials(string participantID, int lineNumber)
    {
        List<string> allErrorTrials = new List<string>();
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);

        for (int i = lineNumber; i < arrLine.Length; i++)
        {
            string[] subs = arrLine[i].Split(' ');
            if (subs[subs.Length -1] == "2")
            {
                allErrorTrials.Add(arrLine[i]);
            }
        }
        return allErrorTrials;
    }

    public static void AppendRemainingErrorTrials(string participantID, int lineNumber)
    {
        var remainingErrorTrials = GetRemainingErrorTrials(participantID, lineNumber);
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);

        for (int i = 0; i < remainingErrorTrials.Count; i++)
        {
            File.AppendAllText(fullPath, remainingErrorTrials[i] + Environment.NewLine);
        }
    }

    public static string GetProgressValueFromTrialOrderLine(string participantID)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        string[] arrLine = File.ReadAllLines(fullPath);
        string lastLine = arrLine[arrLine.Length - 1];
        string[] subs = lastLine.Split(' ');
        string progress = subs[subs.Length - 1];
        return progress;
    }
}

