using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileWriteManagement
{
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
            Debug.Log("filenotthere");
            return false;
        }
    }

    public static void CreateFile(string fileName)
    {
        using(FileStream fs = File.Create(fileName)) ;
    }

    public static void WriteFile(string data, string fileName)
    {
        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        using(StreamWriter writeText = new StreamWriter(currentPath))
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
}

