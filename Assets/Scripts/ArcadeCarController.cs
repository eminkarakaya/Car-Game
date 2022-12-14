using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float forwardAccel = 8f, reverseAccel = 4f,
        maxSpeed = 50f,gravityForce = 10f, turnStrength = 180f,
        dragOnGround = 3f;
    private float speedInput, turnInput;
    [SerializeField] private bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField] private float rayLenght;
    [SerializeField] private Transform leftWheel, rightWheel;
    [SerializeField] private float maxWheelTurn;
    private void Start()
    {
        rb.transform.SetParent(null);
    }
    private void Update()
    {
        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000;
        }
        else if(Input.GetAxis("Vertical")<0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000;
        }
        turnInput = Input.GetAxis("Horizontal");
        if(isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0));
        }
        leftWheel.localRotation = Quaternion.Euler(leftWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftWheel.localRotation.eulerAngles.z);
        rightWheel.localRotation = Quaternion.Euler(rightWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightWheel.localRotation.eulerAngles.z);
        transform.position = rb.transform.position;
    }
    private void FixedUpdate()
    {
        isGrounded = false;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit, rayLenght,groundLayer))
        {
            isGrounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if(isGrounded)
        {
            rb.drag = dragOnGround;
            if(Mathf.Abs(speedInput)>0)
            {
                rb.AddForce(transform.forward * speedInput);
            }

        }
        else
        {
            rb.drag = .1f;
            rb.AddForce(Vector3.down * gravityForce * 100);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * rayLenght);
    }
}
