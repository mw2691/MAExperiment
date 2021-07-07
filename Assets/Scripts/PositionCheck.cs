using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    public GameObject HandReference;

    public MeshRenderer InitialHandVolume;

    public Material TransparentMaterial;
    public Material ValidPositionMaterial;
    public Material InvalidPositionMaterial;

    public bool InPosition { get; private set; }

    public enum PoseControllerMode
    {
        IDLE,
        CHECK
    }

    public PoseControllerMode mode = PoseControllerMode.IDLE;

    private void Update()
    {
        if (this.mode == PoseControllerMode.CHECK)
        {
            this.InPosition = this.checkInitialPose();
        }
    }
    
    public bool checkInitialPose(bool verbose = false)
    {
        if (this.InitialHandVolume.bounds.Contains(this.HandReference.transform.position))
        {
            this.InitialHandVolume.material = this.ValidPositionMaterial;
            return true;
        }

        this.InitialHandVolume.material = this.InvalidPositionMaterial;
        return false;
    }
    
    public void resetInitialPoseChecks()
    {
        this.InitialHandVolume.material = this.InvalidPositionMaterial;
        this.InPosition = false;
    }

    public void setControllerMode(PoseControllerMode mode)
    {
        this.mode = mode;
        if (this.mode == PoseControllerMode.IDLE)
        {
            this.InitialHandVolume.material = this.TransparentMaterial;
            this.InPosition = false;
        }
        else if (this.mode == PoseControllerMode.CHECK)
        {
            this.InitialHandVolume.material = this.InvalidPositionMaterial;
            this.InPosition = false;
        }
    }
}
