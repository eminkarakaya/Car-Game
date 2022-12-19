using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    CarController car;
    float data;
    private void Start()
    {
        car = GetComponent<CarController>();
        
    }
 
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                car.SpecialFinish();
            }
            if (touch.phase == TouchPhase.Moved )
            {
                data = touch.deltaPosition.normalized.x;
                
                //if (data > 1)
                //    data = 1;
                //if (data < -1)
                //    data = -1;
                car.horizontalData = data;
            }
            else
            {
                car.horizontalData = 0;
            }
        }
    }
}
