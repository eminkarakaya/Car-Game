using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] private int _gold;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.gameObject.tag == "Player")
        {
            CollectItem();
        }
    }
    private void CollectItem()
    {
        GameManager.instance.SetGold(_gold);
        Destroy(this.gameObject);
    }
        
}
