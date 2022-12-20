using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    bool isCollected;
    [SerializeField] private bool fuel;
    [SerializeField] private int _value;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CollectItem();
        }
    }
    private void CollectItem()
    {
        Debug.Log(_value);
        if(fuel)
        {
            if (isCollected)
                return;
            isCollected = true;
            GameManager.instance.SetFuel(_value);
        }    
        else
        {
            if (isCollected)
                return;
            isCollected = true;
            GameManager.instance.SetGold(_value);
        }
        Destroy(this.gameObject);
    }
}
