using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Trial : MonoBehaviour
{
    public string SOAFactors;
    public string Interaction;
    public string InteractionPlacement;


    //should be strings, for test purpose only
    public Transform thumbData;
    public Transform indexData;
    public Transform palmData;
    public Transform objectData;

    public string resultFileAnnotations;


    public Trial(string SOAFactors, string Interaction, string InteractionPlacement)
    {
        this.SOAFactors = SOAFactors;
        this.Interaction = Interaction; 
        this.InteractionPlacement = InteractionPlacement;
    }

    //for test purpose
    void Update()
    {
        if (!FileWriteManagement.CheckExistingFile())
        {
            FileWriteManagement.CreateFile();
            FileWriteManagement.WriteFile(GenerateHeader());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            FileWriteManagement.WriteFile(GenerateResultLine(), true);
        }
    }

    //change param to vector3 
    public string TransformToString(Transform vectorData)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(vectorData.position.x + ",");
        sb.Append(vectorData.position.y + ",");
        sb.Append(vectorData.position.z + ",");

        return sb.ToString();
    }


    public string GenerateHeader()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("TrialInfo" + "\t");
        sb.Append("ThumbPosition" + "\t");
        sb.Append("IndexPosition" + "\t");
        sb.Append("PalmPosition" + "\t");
        sb.Append("ObjectPosition" + "\t");
        sb.Append("Annotations" + "\t" + Environment.NewLine);

        return sb.ToString();
    }


    public string GenerateResultLine()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(SOAFactors + "," + Interaction + "," + InteractionPlacement + "\t");
        sb.Append(TransformToString(thumbData)  + "\t");
        sb.Append(TransformToString(indexData) + "\t");
        sb.Append(TransformToString(palmData) + "\t");
        sb.Append(TransformToString(objectData) + "\t");
        sb.Append(resultFileAnnotations + "\t" + Environment.NewLine);

        return sb.ToString();
    }



}
