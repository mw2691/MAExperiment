using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileWriteManagement
{
    public static int participantID = 4;
    public static string fileName = "ID" + participantID.ToString() + ".log";
    public static string currentPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

    public static bool CheckExistingFile()
    {
        if (File.Exists(currentPath))
        {
            Debug.Log("file is already created");
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void CreateFile()
    {
        using(FileStream fs = File.Create(fileName)) ;
    }

    public static void WriteFile(string data)
    {
        using(StreamWriter writeText = new StreamWriter(currentPath))
        {
            writeText.WriteLine(data);
        }
    }

    public static void WriteFile(string data, bool appendFile)
    {
        using (StreamWriter writeText = new StreamWriter(currentPath, appendFile))
        {
            writeText.WriteLine(data);
        }
    }
}

