using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    LevelController levelController;
    [SerializeField] GameObject followObject;
    [SerializeField] private Vector3 rot;
    [SerializeField] Vector3 offset;
    [SerializeField] private float animationDur;
    bool isStart;
    private void OnEnable()
    {
        CarController.OnFall += CameraShake;
        LevelController.OnStart += StartAnim;
    }
    private void OnDisable()
    {
        CarController.OnFall -= CameraShake;        
        LevelController.OnStart -= StartAnim;
    }   
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        followObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        if(isStart)
            Follow();
    }
    void Follow()
    {
        if(!CarController.instance.isFinish)
            transform.position = Vector3.Lerp(transform.position, followObject.transform.position + offset, .1f);
    }
    void CameraShake()
    {
        Debug.Log("shaked");
        //transform.DOPunchRotation(followObject.transform.position,1f,4,.00001f);
    }
    void StartAnim()
    {
        transform.SetParent(followObject.transform);
        transform.DOLocalMove(offset, .5f);
        transform.DOLocalRotate(rot, .5f).OnComplete(() =>
        {
            isStart = true;
            transform.SetParent(null);
        });
    }
}