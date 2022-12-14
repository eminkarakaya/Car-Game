using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider collider;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float currentSteerAngle;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float currentBreakForce;
    [SerializeField] private bool isBreaking;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        
        //currentBreakForce = isBreaking ? breakForce : 0f;

        if (isBreaking)
        {
            currentBreakForce = breakForce;
        }
        else
            currentBreakForce = 0;
        ApplyBreaking();
    }
    private void ApplyBreaking()
    {
        //Debug.Log(frontLeftWheelCollider.brakeTorque);
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
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
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransfrom)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransfrom.rotation = rot;
        wheelTransfrom.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Speed")
        {
            rb.velocity += transform.forward * speedMultiplier;
        }
    }
    private void OnDrawGizmos()
    {
        //rb.centerOfMass = collider.center;
        Gizmos.DrawSphere(rb.worldCenterOfMass, 1);
    }
}
