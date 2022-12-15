using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CarData")]
public class CarData : ScriptableObject
{
    public bool isLocked;
    public string carName;
    public float maxSpeed, boostedMaxSpeed, accelerationSpeed, decelerationSpeed, horizontalSpeed, boostDuration;
}
