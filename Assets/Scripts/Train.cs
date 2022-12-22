using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public bool isTrigger;
    [SerializeField] private float _speed;
    private void Update()
    {
        if(isTrigger)
            transform.Translate(-transform.right * _speed * Time.deltaTime);
        
    }
}
