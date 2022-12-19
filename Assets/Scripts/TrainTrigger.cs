using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    [SerializeField] Train train;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            train.isTrigger = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
