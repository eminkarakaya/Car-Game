using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeshiftRaceCarController : MonoBehaviour
{
    [SerializeField] CarData data;
    [SerializeField] private float maxSpeed,rotationMultiplier, speed;
    float tempMaxSpeed;
    private const string HORIZONTAL = "Horizontal";
    private void Start()
    {
        maxSpeed = data.maxSpeed;
    }
    private void Update()
    {
        if (!GameManager.instance.isStart)
            return;
        if (speed < maxSpeed)
        {
            speed += Time.deltaTime * data.accelerationSpeed;
        }
        else
        {
            if (speed > 0)
            {
                speed -= Time.deltaTime * data.decelerationSpeed;
            }
        }
        Vector3 rot = Vector3.forward * Input.GetAxis(HORIZONTAL) * rotationMultiplier;
        transform.rotation = Quaternion.Euler(rot);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis(HORIZONTAL)*data.horizontalSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Speed")
        {
            Boost();
        }
    }
    private void Boost()
    {
        speed = data.boostedMaxSpeed;
    }
}
