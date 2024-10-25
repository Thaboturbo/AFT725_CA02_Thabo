using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMeshFollow : MonoBehaviour
{
    [Header("Wheel Meshes")]
    public Transform wm_FrontLeft;
    public Transform wm_FrontRight;
    public Transform wm_BackLeft;
    public Transform wm_BackRight;

    [Header("Wheel Colliders")]
    public WheelCollider wc_FrontLeft;
    public WheelCollider wc_FrontRight;
    public WheelCollider wc_BackLeft;
    public WheelCollider wc_BackRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        updatewheelOrientation(wc_FrontRight, wm_FrontRight);
        updatewheelOrientation(wc_FrontLeft, wm_FrontLeft);
        updatewheelOrientation(wc_BackRight, wm_BackRight);
        updatewheelOrientation(wc_BackLeft, wm_BackLeft);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void updatewheelOrientation(WheelCollider wc, Transform wm)
    {
        Vector3 tempLocation;
        Quaternion tempRotation;

        wc.GetWorldPose(out tempLocation, out tempRotation);

        wm.position = tempLocation;
        wm.rotation = tempRotation;
    }

}
