using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeshiftRaceCarController : MonoBehaviour
{
    bool boost;
    Rigidbody rb;
    [SerializeField] private float maxSpeed,horizontalSpeed,rotationMultiplier,boostedMaxSpeed, speed;
    [SerializeField] private float accelerationSpeed, decelerationSpeed;
    float tempMaxSpeed,boostDuration;
    private const string HORIZONTAL = "Horizontal";
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (speed < maxSpeed)
        {
            speed += Time.deltaTime * accelerationSpeed;
        }
        else
        {
            if (speed > 0)
            {
                speed -= Time.deltaTime * decelerationSpeed;
            }
        }
        Vector3 rot = Vector3.forward * Input.GetAxis(HORIZONTAL) * rotationMultiplier;
        transform.rotation = Quaternion.Euler(rot);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis(HORIZONTAL)*horizontalSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Speed")
        {
            StopAllCoroutines();
            StartCoroutine(Boost());
        }
    }
    //private void Boost()
    //{
    //    tempMaxSpeed = maxSpeed;
    //    maxSpeed = boostedMaxSpeed;
    //}
    private IEnumerator Boost()
    {
        tempMaxSpeed = maxSpeed;
        maxSpeed = boostedMaxSpeed;
        speed = boostedMaxSpeed;
        yield return new WaitForSeconds(boostDuration);
        maxSpeed = tempMaxSpeed;
    }
}
