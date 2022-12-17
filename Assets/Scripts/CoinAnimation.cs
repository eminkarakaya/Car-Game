using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinAnimation : MonoBehaviour
{
    Vector3 rot = new Vector3(0, 180, 0);
    [SerializeField] private Ease ease;
    void Start()
    {
        
        transform.DORotate(rot, 3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        
        transform.DOMoveY(transform.position.y + 1, 1f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
