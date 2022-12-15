using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField] GameObject followObject;
    [SerializeField] Vector3 offset;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Follow();
    }
    void Follow()
    {
        transform.position = Vector3.Lerp(new Vector3(0, transform.position.y,transform.position.z), new Vector3(0,followObject.transform.position.y, followObject.transform.position.z) + offset, .75f);
    }
    public void WinAnim(Transform _transform)
    {
        //transform.DOMove( + offset, 2f);
    }
}