using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour
{
    private Vector3 currentVelocity;
    //private bool isBraking = false;
    private float currentTurnInput;
    private float TargetTurnInput;
    public Rigidbody carBody;
    private float accelerationInput;
    public float carHorsePower = 400f;
    public float maxTurnAngle = 20f;
    public Rigidbody carRb;
    public float jumpAmount;
    public GameObject speedUI;
    private Text speedTxt;


    [Header("Wheel Colliders")]
    public WheelCollider wc_FrontLeft;
    public WheelCollider wc_FrontRight;
    public WheelCollider wc_BackLeft;
    public WheelCollider wc_BackRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity= carBody.velocity;

        accelerationInput = Input.GetAxis("Vertical");
        TargetTurnInput = Input.GetAxis("Horizontal");

        speedTxt.text = (carRb.velocity.magnitude * 3.6f).ToString("0") + ("km/h");
    }

    private void FixedUpdate()
    {
        Debug.Log("The Speed = " + currentVelocity + "|| Accerelation Input = " + accelerationInput);
        Vector3 combineInput = (transform.forward * -1) * accelerationInput;
        float DotProduct = Vector3.Dot(currentVelocity.normalized, combineInput);

        if (DotProduct < 0)
        {
            // Brake
            wc_FrontLeft.brakeTorque = 1000f;
            wc_BackLeft.brakeTorque = 1000f;
            wc_FrontRight.brakeTorque = 1000f;
            wc_BackLeft.brakeTorque = 1000f;
            //no aceel
            wc_FrontLeft.motorTorque = 0;
            wc_BackLeft.motorTorque = 0;
            wc_FrontRight.motorTorque = 0;
            wc_BackLeft.motorTorque = 0;
        }
        else
        {
            // No brake
            wc_FrontLeft.brakeTorque = 0;
            wc_BackRight.brakeTorque = 0;
            wc_BackLeft.brakeTorque = 0;
            wc_FrontRight.brakeTorque = 0;


            wc_BackLeft.motorTorque = accelerationInput * carHorsePower * -1;
            wc_BackRight.motorTorque = accelerationInput * carHorsePower * -1;
        }
        // code for handbrake

        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                wc_BackLeft.brakeTorque = 1000f;
                wc_BackRight.brakeTorque = 1000f;

                
            }
            else
            {
                wc_BackLeft.brakeTorque = 0;
                wc_BackRight.brakeTorque = 0;
            }
        }

        string KeyPressed;
        if (accelerationInput > 0)
        {
            KeyPressed = "W";
        }
        else if (accelerationInput < 0)
        {
            KeyPressed = "S";
        }
        else
        {
            KeyPressed = "No Key Pressed";
        }
        //applying turn
        currentTurnInput = ApproachTargetValueWithIncrement(currentTurnInput, TargetTurnInput, 0.07f);
        wc_FrontLeft.steerAngle = currentTurnInput * maxTurnAngle;
        wc_FrontRight.steerAngle = currentTurnInput * maxTurnAngle;


        Debug.Log("Input = " + KeyPressed + "||| Velocity = " + currentVelocity.normalized + "|| Dot Product = " + DotProduct);
        // jump function

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            carRb.AddForce(Vector3.up *jumpAmount,ForceMode.Impulse );
        }
  


    }
    private float ApproachTargetValueWithIncrement(float currentValue, float targetValue, float increment)
    {
        if (currentValue == targetValue) 
        {
            return currentValue;
        }
        else
        {
            if (currentValue < targetValue)
            {
                currentValue = currentValue + increment;
            }
            else
            {
                currentValue = currentValue - increment;
            }

        }
        return currentValue;
    }
}
