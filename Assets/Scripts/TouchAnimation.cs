using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TouchAnimation : MonoBehaviour
{
    [SerializeField] Transform start, finish;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    private void Start()
    {
        transform.position = start.position;
        transform.DOMove(finish.position, duration).SetEase(ease).SetLoops(-1,LoopType.Yoyo);
    }
}
