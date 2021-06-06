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


    public static bool CheckExistingFile(string fileName)
    {
        string pathWithfileName = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        if (File.Exists(pathWithfileName))
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

    public static string GetTrialOrderLine(string participantID)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        using (StreamReader sr = new StreamReader(fullPath))
        {
            //skip header
            sr.ReadLine();
            return sr.ReadLine();
        }
    }

    public static void WriteProgressInTrialOrderFile(string participantID, int lineToEdit)
    {
        string fullPath = trialOrderFilePath + "/" + GetTrialOrderFileName(participantID);
        var currentLine = GetTrialOrderLine(participantID);
        string[] subs = currentLine.Split(' ');
        subs[subs.Length - 1] = "1";
        var newLine = string.Join(" ", subs);

        string[] arrLine = File.ReadAllLines(fullPath);
        arrLine[lineToEdit - 1] = newLine;
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
}

