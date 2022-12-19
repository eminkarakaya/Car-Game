using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
   
    CarController car;
    private void Start()
    {
        car = GetComponent<CarController>();
        
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                car.SpecialFinish();

            }
            if (touch.phase == TouchPhase.Moved)
            {
                car.horizontalData = touch.deltaPosition.normalized.x;
            }
            else
            {
                car.horizontalData = 0;
            }
        }
    }
}
