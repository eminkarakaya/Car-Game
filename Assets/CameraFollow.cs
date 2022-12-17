using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    LevelController levelController;
    [SerializeField] GameObject followObject;
    [SerializeField] Vector3 offset;
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        followObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        Follow();
    }
    void Follow()
    {
        if(!CarController.instance.isFinish)
            transform.position = Vector3.Lerp(new Vector3(0, transform.position.y,transform.position.z), new Vector3(0,followObject.transform.position.y, followObject.transform.position.z) + offset, 1f);
    }
}