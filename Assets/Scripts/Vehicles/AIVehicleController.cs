using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVehicleController : MonoBehaviour
{

    public GameObject targetObject;
    private float distanceToTarget;

    private float appliedMotorTorque;
    private float appliedBrakeTorque;
    private float appliedTurnAngle;

    [Header("Wheel Colliders and Rigidbody")]
    public WheelCollider wc_fl;
    public WheelCollider wc_fr;
    public WheelCollider wc_rl;
    public WheelCollider wc_rr;
    public Rigidbody aiCarBody;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(targetObject.transform.position,transform.position);
        //Calculate distance between the AI car and the target position
        // Keep in mind that transform.position in the line below refers to the position of this AI car
        if (distanceToTarget < 5)
        {
            appliedBrakeTorque = 1000f;
            appliedMotorTorque = 0f;
        }
        else  //if the target is not close and is far away. This is the  condition for accelerating forward or backward
        {
            //set applied brake torque  to 0  as soon as we enter this else condition
            appliedBrakeTorque = 0;

            //get the normalized direction towards the target
            Vector3 targetDirection = (targetObject.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, targetDirection);

            if (dotProduct > 0) // if the object is in front accelerate forward
            {
                appliedMotorTorque = 700;
            }
            else // if the object is not in fornt decidee if we need to reverse or turn around
            {
                if (distanceToTarget>20) // if the object ismore than 20m away, do not reverse  and turn around instead
                {
                    appliedMotorTorque = 700;
                }
                else //if the object is less than 20m away, reverse the car
                {
                    appliedMotorTorque = -700;
                }
            }
            // calaculate an angle between forward, targetDirection, with the Up-Axis as the pivot axis
            float targetAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
            
            //convert the angle from degrees to radians
            float targetAngleRadians = targetAngle * Mathf.Rad2Deg;

            //calculate the Sin to get a value between -1 and 1 
            appliedTurnAngle= Mathf.Sin(targetAngleRadians)* 25f;
        }
    }

    private void FixedUpdate()
    {
        wc_fl.motorTorque = appliedMotorTorque;
        wc_fl.brakeTorque = appliedBrakeTorque;
        wc_fl.steerAngle = appliedTurnAngle;

        wc_fr.motorTorque = appliedMotorTorque;
        wc_fr.brakeTorque = appliedBrakeTorque;
        wc_fr.steerAngle = appliedTurnAngle;
    }

}
