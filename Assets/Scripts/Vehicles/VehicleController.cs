using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour
{
    private Vector3 currentVelocity;
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
    private float checkGround;

    public float nitrousActiveDuration = 4f;
    public float nitrousRechargeTime = 10f;
    public float nitrousRechargeDelay = 3f;
    public float nitrousTorque = 500f;
    private bool isNitrousActive;

    private float currentNitrousCapacity = 1f;
    private float currentNitrousDelay = 0;
    private float currentNitrousTorque = 0;


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

        isNitrousActive = Input.GetKeyDown(KeyCode.N);

        //checking to see if the nitrous isnt empty
        if (isNitrousActive == true)
        {
            //checking if nitrous isnt empty
            if (currentNitrousCapacity > 0)
            {
                currentNitrousCapacity -= (Time.deltaTime / nitrousActiveDuration);
                currentNitrousTorque = nitrousTorque;
            }
            //the following executtes if Nitrouswas held or pressed when the Nitrous capacity was already empty
            else
            {

            }
        }
        // when the button is not pressed, start refilling
        else
        {
            //before refilling check to make sure you are not already full
            if (currentNitrousCapacity < 1)
            {
                currentNitrousCapacity += (Time.deltaTime / nitrousRechargeTime);
                currentNitrousTorque = 0;
            }
        }
        //Debug.Log(" current Nitrous Capacity = " + currentNitrousCapacity);


    }

    private void FixedUpdate()
    {
        //Debug.Log("The Speed = " + currentVelocity + "|| Accerelation Input = " + accelerationInput);
        Vector3 combineInput = (transform.forward) * accelerationInput;
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


            wc_BackLeft.motorTorque = accelerationInput * carHorsePower;
            wc_BackRight.motorTorque = accelerationInput * carHorsePower;
        }
        // code for handbrake

        {
            // jump function
            if (Input.GetKeyDown(KeyCode.Space))
            {
                carRb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }
        }
        //applying turn
        currentTurnInput = ApproachTargetValueWithIncrement(currentTurnInput, TargetTurnInput, 0.07f);
        wc_FrontLeft.steerAngle = currentTurnInput * maxTurnAngle;
        wc_FrontRight.steerAngle = currentTurnInput * maxTurnAngle;



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
