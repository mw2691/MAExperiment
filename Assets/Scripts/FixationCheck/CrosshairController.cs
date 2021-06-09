using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    // should be the main camera
    public Camera RelevantCamera;
    public bool active = true;

    private static bool Verbose = false;
    // how long has the fixation to last (in seconds)...
    private static float timeThreshold = 0.5f;

    public bool Fixated { get; private set; }
    private bool fixationDone;
    private float timeStamp;
    public GameObject fixCrossRef;
    public FixationCrossColorSwitch colorSwitch;

    void Start()
    {
        if (CrosshairController.Verbose) Debug.Log("Crosshair is alive...");
        this.Fixated = false;
        this.fixationDone = false;
        this.timeStamp = 0f;
        //this.fixCrossRef = GameObject.FindWithTag("FixationCrossCenter");
        //this.colorSwitch = this.fixCrossRef.GetComponent<FixationCrossColorSwitch>();
        this.active = false;
    }

    public void reset()
    {
        this.Fixated = false;
        this.fixationDone = false;
        this.timeStamp = 0f;
        this.active = false;
        this.colorSwitch.switchColor(false);
    }

    void Update()
    {
        if (this.fixCrossRef == null || !this.active)
        {
            return;
        }

        Vector3 cameraPosition; // Current camera Position
        Vector3 cameraForwardOrientation; // Current camera Orientation
        Ray rayFromCamera; // Ray originated from Left Camera
        RaycastHit rayHitInfo; // Info of the ray hitting

        // get camera position & orientation
        cameraPosition = this.RelevantCamera.transform.position;
        cameraForwardOrientation = this.RelevantCamera.transform.rotation * Vector3.forward;
        // Ray from Camera to facing direction has to build new every Frame
        rayFromCamera = new Ray(cameraPosition, cameraForwardOrientation);
        // Check if ray hits something
        if (Physics.Raycast(rayFromCamera, out rayHitInfo))
        {
            if (CrosshairController.Verbose) Debug.Log("Hit! Collider is " + rayHitInfo.collider);
            if (rayHitInfo.collider != null && rayHitInfo.collider.transform.gameObject == fixCrossRef)
            {
                if (CrosshairController.Verbose) Debug.Log("Fixated.");
                if (!Fixated)
                {
                    this.timeStamp = 0f;
                }
                this.Fixated = true;
                this.colorSwitch.switchColor(true);
                this.timeStamp += Time.deltaTime;
                if (this.timeStamp >= CrosshairController.timeThreshold)
                {
                    if (CrosshairController.Verbose) Debug.Log("fixation lasted " + CrosshairController.timeThreshold + "sec (" + timeStamp + ")...");
                    this.fixationDone = true;
                    this.timeStamp = 0f;
                    fixCrossRef = rayHitInfo.collider.transform.gameObject;
                }
            }
            else
            {
                if (CrosshairController.Verbose) Debug.Log("Far away");
                this.colorSwitch.switchColor(false);
                this.Fixated = false;
                this.fixationDone = false;
            }
        }
        else
        {
            this.Fixated = false;
            this.fixationDone = false;
            this.colorSwitch.switchColor(false);
        }
    }

    public bool FixationCompleted()
    {
        return this.fixationDone;
    }
}
