using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Trial
{
    public string SOAFactors;
    public string Interaction;
    public string InteractionPlacement;
    public string ParticipantID;

    public int TrialNumber;

    //Dependent measures
    public Vector3 thumbData;
    public Vector3 indexData;
    public Vector3 palmData;
    public Vector3 objectData;
    public Vector3 eyeData;
    public string resultFileAnnotations;
    public string resultFileGazeAnnotations;


    public Trial(string ParticipantID, int TrialNumber, string SOAFactors, string Interaction, string InteractionPlacement)
    {
        char tab = '\u0009';
        string soaRemovedTab = SOAFactors.Replace(tab.ToString(), "");
        string interactionRemovedTab = Interaction.Replace(tab.ToString(), "");
        string placementRemovedTab = InteractionPlacement.Replace(tab.ToString(), "");

        this.ParticipantID = ParticipantID;
        this.TrialNumber = TrialNumber;
        this.SOAFactors = soaRemovedTab;
        this.Interaction = interactionRemovedTab;
        this.InteractionPlacement = placementRemovedTab;
    }


    public string Vector3ToString(Vector3 vectorData)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(vectorData.x + ",");
        sb.Append(vectorData.y + ",");
        sb.Append(vectorData.z + ",");

        return sb.ToString();
    }


    public string GenerateFileName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(TrialNumber + "_");
        sb.Append(ParticipantID + "_");
        sb.Append(SOAFactors + "_");
        sb.Append(Interaction + "_");
        sb.Append(InteractionPlacement + ".log");

        return sb.ToString();
    }


    public string GenerateHeader()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("ThumbPosition" + "\t");
        sb.Append("IndexPosition" + "\t");
        sb.Append("PalmPosition" + "\t");
        sb.Append("ObjectPosition" + "\t");
        sb.Append("GazePosition" + "\t");
        sb.Append("Annotations" + "\t");
        sb.Append("GazeAnnotations" + "\t" + Environment.NewLine);

        return sb.ToString();
    }


    public string GenerateResultLine()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Vector3ToString(thumbData) + "\t");
        sb.Append(Vector3ToString(indexData) + "\t");
        sb.Append(Vector3ToString(palmData) + "\t");
        sb.Append(Vector3ToString(objectData) + "\t");
        sb.Append(Vector3ToString(eyeData) + "\t");
        sb.Append(resultFileAnnotations + "\t");
        sb.Append(resultFileGazeAnnotations + "\t" + Environment.NewLine);

        return sb.ToString();
    }



}
