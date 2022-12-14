using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeshiftRaceCarController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float maxSpeed,horizontalSpeed,rotationMultiplier;
    public float speed;
    private const string HORIZONTAL = "Horizontal";
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 rot = Vector3.forward * Input.GetAxis(HORIZONTAL) * rotationMultiplier;
        transform.rotation = Quaternion.Euler(rot);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis(HORIZONTAL)*horizontalSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void FixedUpdate()
    {
        
    }
}
