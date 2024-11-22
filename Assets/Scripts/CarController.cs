using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    public float speed = 10f; // Forward speed
    public float turnSpeed = 100f; // Turn Speed
    public float brakeForce = 50f; // Force applied when braking
   

    private Rigidbody rb;

     void Start()
     {
        rb = GetComponent<Rigidbody>();
     }

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        {
            // Get input for forward and backword movement
            float moveVertical = Input.GetAxis("Vertical"); // W/S for froward/Backword
            float moveHorizontal = Input.GetAxis("Horizontal"); //A/D move left/right

            // move the car forward/backword 
            Vector3 movement = transform.forward * moveVertical * speed * Time.deltaTime;
             rb.MovePosition(rb.position + movement);


            // Rotate the car
            float turn = moveHorizontal * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);


            // Apply braking if the player presses the "S" key
            if (Input.GetKey(KeyCode.S))
            {
                ApplyBrakes();
            }
        }
    }



    void ApplyBrakes()
    {
        // Reduce the car's velocity in the forward direction
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 brakeVelocity = currentVelocity.normalized * -brakeForce * Time.deltaTime;

        // Clamp the brake velocity to ensure we don't reverse the car
        if (currentVelocity.magnitude > 0)
        {
            rb.linearVelocity += brakeVelocity;

            // Prevent the car from reversing when braking
            if (Vector3.Dot(currentVelocity, rb.linearVelocity) < 0)
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
       
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

        // Add wheel rotation based on speed
        wheelTransform.Rotate(Vector3.right, wheelCollider.rpm * 6 * Time.deltaTime);
    }
}
