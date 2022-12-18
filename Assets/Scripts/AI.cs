using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    RaycastHit hit;
    [SerializeField] private Vector2 moveFrequencyMinMax;
    [SerializeField] private Vector2 moveDurMinMax;
    CarController car;
    float duration;
    private void Start()
    {
        duration = Random.Range(moveFrequencyMinMax.x, moveFrequencyMinMax.y);
        car = GetComponent<CarController>();
    }
    private void Update()
    {
        if(duration > 0)
            duration -= Time.deltaTime;
        if(duration < 0)
        {
            duration = 0;
            StartCoroutine(Move()); 
        }
    }
    float Direction()
    {
        var right = 0f;
        var left = 0f;
        if(Physics.Raycast(transform.position,Vector3.right,out hit,100,layer))
        {
            right = Mathf.Abs(transform.position.x - hit.transform.position.x);
        }
        if(Physics.Raycast(transform.position,Vector3.left,out hit,100,layer))
        {
            left = Mathf.Abs(transform.position.x - hit.transform.position.x);
        }
        if (right > left)
            return 1;
        else
            return -1;

    }
    IEnumerator Move()
    {
        var temp = Random.Range(moveDurMinMax.x, moveDurMinMax.y);
        
        var dir = Direction();
        while(temp > 0)
        {
            temp -= Time.deltaTime;
            car.horizontalData = dir;
            yield return null;
        }
        duration = Random.Range(moveFrequencyMinMax.x, moveFrequencyMinMax.y);
        car.horizontalData = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            StopAllCoroutines();
            StartCoroutine(Move());
        }
    }
}
